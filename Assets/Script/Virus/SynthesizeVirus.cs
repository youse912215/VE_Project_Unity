using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Call.CommonFunction;
using static Call.ConstantValue;
using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static SimulationButtonManager;
using static VirusMaterialData;
using static VirusMaterialData.MATERIAL_CODE;

public class SynthesizeVirus : MonoBehaviour
{
    public static VIRUS_NUM currentCode; //現在選択しているウイルスコード

    private bool initFlag;
    private bool[] isCreate = new bool[8]; //ウイルスを作れるか
    private int[] creationArray = new int[9]; //作成数配列
    private List<int> tmpList = new List<int> { 0, 0, 0, 0 }; //一旦、素材を保存するリスト

    private GameObject selectUI;
    private RectTransform selRect;

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

    private Text[] vCount = new Text[V_CATEGORY];
    private Text vArray;
    private GameObject[] countText = new GameObject[V_CATEGORY];
    private GameObject arrayText;

    // Start is called before the first frame update
    void Start()
    {
        currentCode = CODE_CLD; //現在のコードなし
        creationArray = Enumerable.Repeat(0, creationArray.Length).ToArray(); //0個で配列を初期化
        InitOwnedVirus(); //現在の保有数の初期化

        InitText(); //テキストの初期化
        InitUI(); //UIの初期化
    }

    // Update is called once per frame
    void Update()
    {
        ShowCurrentCounts();

        if (initFlag) return; //初期化フラグがtrueのとき、処理をスキップ
        StartCoroutine("InitSetButton"); //ボタンの初期化割り当て
    }

    /// <summary>
    /// Pushing virus button
    /// </summary>
    /// <param name="code">現在のコード</param>
    public void PushVirusButton(int code)
    {
        GetSelectPosition(code);
        creationArray[(int)currentCode] = 0; //作成数をリセット
        currentCode = (VIRUS_NUM)code; //現在のコードを保存
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //必要個数に達しているか確認
    }

    /// <summary>
    /// Pushing creation button
    /// </summary>
    public void PushCreateButton()
    {
        if (currentCode == CODE_NONE) return; //コードがないとき、処理をスキップ

        if (creationArray[(int)currentCode] <= 0) return; //作成数が0以下のとき、処理をスキップ

        SaveCreationVirus(creationArray, currentCode); //作成したウイルス数を保存
        creationArray[(int)currentCode] = 0; //作成数をリセット
    }

    /// <summary>
    /// Pushing addition button
    /// </summary>
    /// <param name="sign"></param>
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

    /// <summary>
    /// Pushing subtraction button
    /// </summary>
    /// <param name="sign"></param>
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
        for (int i = 0; i < V_CATEGORY; ++i)
            buttons[0].onClick.AddListener(() => { PushVirusButton(0); });
        buttons[1].onClick.AddListener(() => { PushVirusButton(1); });
        buttons[2].onClick.AddListener(() => { PushVirusButton(2); });
        buttons[3].onClick.AddListener(() => { PushVirusButton(3); });
        buttons[4].onClick.AddListener(() => { PushVirusButton(4); });
        buttons[5].onClick.AddListener(() => { PushVirusButton(5); });
        buttons[6].onClick.AddListener(() => { PushVirusButton(6); });
        buttons[7].onClick.AddListener(() => { PushVirusButton(7); });
        buttons[8].onClick.AddListener(PushCreateButton);
        buttons[9].onClick.AddListener(() => { PushSubButton(1); });
        buttons[10].onClick.AddListener(() => { PushAddButton(-1); });

        initFlag = ReverseFlag(initFlag); //初期化フラグを反転
        yield return null; //関数から抜ける
    }

    /// <summary>
    /// テキストオブジェクトの初期化
    /// </summary>
    private void InitText()
    {
        for (int i = 0; i < V_CATEGORY; ++i)
        {
            countText[i] = GameObject.Find("vCount" + i.ToString());
            vCount[i] = countText[i].GetComponent<Text>();
        }
        arrayText = GameObject.Find("cArray");
        vArray = arrayText.GetComponent<Text>();
    }

    /// <summary>
    /// UIの初期化
    /// </summary>
    private void InitUI()
    {
        selectUI = GameObject.Find("CurrentSelect");
        selRect = selectUI.GetComponent<RectTransform>();
        selRect.localPosition = SELECT_UI_POS; //初期位置にセット
    }

    /// <summary>
    /// 現在の作成数等を表示
    /// </summary>
    private void ShowCurrentCounts()
    {
        if (currentCode == CODE_NONE) return;
        vCount[0].text = "×" + vCreationCount[0].ToString();
        vCount[1].text = "×" + vCreationCount[1].ToString();
        vCount[2].text = "×" + vCreationCount[2].ToString();
        vCount[3].text = "×" + vCreationCount[3].ToString();
        vCount[4].text = "×" + vCreationCount[4].ToString();
        vCount[5].text = "×" + vCreationCount[5].ToString();
        vCount[6].text = "×" + vCreationCount[6].ToString();
        vCount[7].text = "×" + vCreationCount[7].ToString();
        vArray.text = creationArray[(int)currentCode].ToString();
    }

    /// <summary>
    /// 選択UIの位置を取得
    /// </summary>
    /// <param name="code">ウイルスコード</param>
    private void GetSelectPosition(int code)
    {
        selRect.localPosition = new Vector3(
            SELECT_UI_POS.x,
            SELECT_UI_POS.y - CulculationSelectPos(code),
            SELECT_UI_POS.z);
    }

    /// <summary>
    /// 選択UIの位置の計算
    /// </summary>
    /// <param name="code">ウイルスコード</param>
    /// <returns></returns>
    private float CulculationSelectPos(int code)
    {
        return code == (int)CODE_ULT ? BUTTON_HEIGHT * 5.0f : (BUTTON_HEIGHT * code);
    }

    /// <summary>
    /// Pushing create screen button
    /// </summary>
    private void PushCreateScreenButton()
    {

    }

    /// <summary>
    /// Pushing prepare screen button
    /// </summary>
    private void PushPrepareScreenButton()
    {

    }

    /// <summary>
    /// Pushing supplies screen button
    /// </summary>
    private void PushSuppliesScreenButton()
    {

    }
}
