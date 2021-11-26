using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static DebugManager;
using static VirusMaterialData;
using static VirusMaterialData.MATERIAL_CODE;
using static SimulationButtonManager;
using System;

public class SynthesizeVirus : MonoBehaviour
{
    public static VIRUS_NUM currentCode; //現在選択しているウイルスコード

    private bool initFlag;
    private bool[] isCreate = new bool[8]; //ウイルスを作れるか
    private int[] creationArray = new int[9]; //作成数配列
    private List<int> tmpList = new List<int>{0, 0, 0, 0}; //一旦、素材を保存するリスト

    //素材合成リスト
    private MATERIAL_CODE[,] matCompositList =
    {
        { V1_PROTEIN, V2_PROTEIN, V3_PROTEIN, BLUE_POTION },
        { ERYTHROCYTE_AGGLUTININ, NEURAMINIDASE, ENVELOPE, RED_POTION },
        { S_PROTEIN, N_PROTEIN, ENVELOPE, RED_POTION },
        { PROTEASE, V1_PROTEIN, V2_PROTEIN, BLUE_POTION },
        { V24_PROTEIN, V40_PROTEIN, NUCLEOCCAPSID, YELLOW_POTION },
        { FEN1_PROTEIN, POLYMERASE, ENVELOPE, BLUE_POTION },
        { V3_PROTEIN, F1_ANTIGEN, V_ANTIGEN, RED_POTION },
        { ULX_PROTEIN, ERYTHROCYTE_AGGLUTININ, ENVELOPE, BLOOD_POTION },
    };

    public GameObject s = null;
    public GameObject s2 = null;

    // Start is called before the first frame update
    void Start()
    {
        currentCode = CODE_NONE; //現在のコードなし
        creationArray = Enumerable.Repeat(0, creationArray.Length).ToArray(); //0個で配列を初期化
        InitOwnedVirus(); //現在の保有数の初期化
    }

    // Update is called once per frame
    void Update()
    {
        debug();
        Debug.Log("現在のコード::" + currentCode);

        if (initFlag) return; //初期化フラグがtrueのとき、処理をスキップ
        StartCoroutine("InitSetButton"); //ボタンの初期化割り当て
    }

    /// <summary>
    /// Pushing virus button
    /// </summary>
    /// <param name="code">現在のコード</param>
    public void PushVirusButton(int code)
    {
        creationArray[(int)currentCode] = 0; //作成数をリセット
        currentCode = (VIRUS_NUM)code; //現在のコードを保存
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //必要個数に達しているか確認
    }

    public void PushCreateButton()
    {
        if (currentCode == CODE_NONE) return; //コードがないとき、処理をスキップ

        if (creationArray[(int)currentCode] <= 0) return; //作成数が0以下のとき、処理をスキップ

        SaveCreationVirus(creationArray, currentCode); //作成したウイルス数を保存
        creationArray[(int)currentCode] = 0; //作成数をリセット
    }

    public void PushAddButton(int sign)
    {
        //GameObject ob = buttons[0].transform.parent.gameObject;
        //ob.SetActive(false);

        if (currentCode == CODE_NONE) return; //コードがないとき、処理をスキップ

        if (creationArray[(int)currentCode] == 0)
            SaveOwnedVirusMaterial(matCompositList, tmpList, currentCode); //0個のときの素材数を保存

        if (!isCreate[(int)currentCode]) return; //作成できない状態のとき、処理をスキップ

        OrganizeVirusMaterialCount(sign); //素材量を整理する
    }

    public void PushSubButton(int sign)
    {
        if (currentCode == CODE_NONE) return; //コードがないとき、処理をスキップ

        if (creationArray[(int)currentCode] == 0)
            SaveOwnedVirusMaterial(matCompositList, tmpList, currentCode); //0個のときの素材数を保存

        if (creationArray[(int)currentCode] == 0) return; //作成数が0のとき、処理をスキップ

        OrganizeVirusMaterialCount(sign); //素材量を整理する
    }

    /// <summary>
    /// 素材量を整理する
    /// </summary>
    /// <param name="sign">符号</param>
    private void OrganizeVirusMaterialCount(int sign)
    {
        creationArray[(int)currentCode] -= sign; //作成数を減らす
        CulcOwnedVirus(matCompositList, currentCode, sign); //作成に使用する素材を増やす
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //必要個数に達しているか確認
    }

    /// <summary>
    /// ボタンの初期化割り当て
    /// </summary>
    IEnumerator InitSetButton()
    {
        buttons[0].onClick.AddListener( () => { PushVirusButton(0); } );
        buttons[1].onClick.AddListener( () => { PushVirusButton(1); } );
        buttons[2].onClick.AddListener( () => { PushVirusButton(2); } );
        buttons[3].onClick.AddListener( () => { PushVirusButton(3); } );
        buttons[4].onClick.AddListener( () => { PushVirusButton(4); } );
        buttons[5].onClick.AddListener( () => { PushVirusButton(5); } );
        buttons[6].onClick.AddListener( () => { PushVirusButton(6); } );
        buttons[7].onClick.AddListener( () => { PushVirusButton(7); } );
        buttons[8].onClick.AddListener(PushCreateButton);
        buttons[9].onClick.AddListener(() => { PushSubButton(1); } );
        buttons[10].onClick.AddListener(() => { PushAddButton(-1); } );

        initFlag = ReverseFlag(initFlag); //初期化フラグを反転
        yield return null; //関数から抜ける
    }

    private void debug()
    {
        Text vN = s.GetComponent<Text>();
        vN.text = "[0]::" + vCreationCount[0].ToString()
            + "[1]::" + vCreationCount[1].ToString()
            + "[2]::" + vCreationCount[2].ToString();
        Text vN2 = s2.GetComponent<Text>();
        vN2.text = "[0]::" + creationArray[0].ToString()
            + "[1]::" + creationArray[1].ToString()
            + "[2]::" + creationArray[2].ToString();
    }
}
