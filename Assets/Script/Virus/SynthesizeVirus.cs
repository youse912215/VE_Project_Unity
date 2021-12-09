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
    private bool isPushing; //ウイルスボタンを押したか
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

    //現在のウイルス保有量テキスト
    private Text[] vCount = new Text[V_CATEGORY];
    private Text[] vArray = new Text[1];
    private GameObject[] vCountText = new GameObject[V_CATEGORY];
    private GameObject[] vArrayText = new GameObject[1];

    //現在のウイルス作成素材保有量テキスト
    private Text[] vMCount = new Text[4];
    private GameObject[] vMCountText = new GameObject[4];

    private Text[] vPCount = new Text[V_CATEGORY];
    private GameObject[] vPCountText = new GameObject[V_CATEGORY];

    // Start is called before the first frame update
    void Start()
    {
        isPushing = false;

        currentCode = CODE_CLD; //現在のコードなし
        creationArray = Enumerable.Repeat(0, creationArray.Length).ToArray(); //0個で配列を初期化
        InitOwnedVirus(); //現在の保有数の初期化

        InitText(V_CATEGORY, "vCount", vCountText, vCount); //テキストの初期化
        InitText(1, "cArray", vArrayText, vArray);
        InitText(tmpList.Count(), "vMCount", vMCountText, vMCount);
        InitText(V_CATEGORY, "vPCount", vPCountText, vPCount);

        InitUI(); //UIの初期化

        PushVirusButton((int)currentCode);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!isPushing) return;
        UpdateCountText(); //現在のカウントテキストを更新
        isPushing = false; //ボタン処理をoff

        if (initFlag) return; //初期化フラグがtrueのとき、処理をスキップ
        StartCoroutine("InitSetButton"); //ボタンの初期化割り当て
    }

    /// <summary>
    /// Pushing virus button
    /// </summary>
    /// <param name="code">現在のコード</param>
    public void PushVirusButton(int code)
    {
        isPushing = true;

        UpdateSelectPosition(code); //選択位置更新

        if (creationArray[(int)currentCode] != 0)
            ResetOwnedVirusMaterial(matCompositList, tmpList, currentCode);

        creationArray[(int)currentCode] = 0; //作成数をリセット
        currentCode = (VIRUS_NUM)code; //現在のコードを保存
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //必要個数に達しているか確認
    }

    /// <summary>
    /// Pushing creation button
    /// </summary>
    public void PushCreateButton()
    {
        isPushing = true;

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
        isPushing = true;

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
        isPushing = true;

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
    private void InitText(int n, string name, GameObject[] obj, Text[] text)
    {
        for (int i = 0; i < n; ++i)
        {
            obj[i] = GameObject.Find(name + i.ToString());
            text[i] = obj[i].GetComponent<Text>();
        }
        //vArrayText = GameObject.Find("cArray");
        //vArray = vArrayText.GetComponent<Text>();
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
    /// 現在のカウントテキスト等を更新
    /// </summary>
    private void UpdateCountText()
    {
        if (currentCode == CODE_NONE) return;

        for (int i = 0; i < V_CATEGORY; ++i) vCount[i].text = vCreationCount[i].ToString(); //ウイルス保有数テキスト
        vArray[0].text = creationArray[(int)currentCode].ToString(); //合計作成数テキスト

        for (int i = 0; i < tmpList.Count(); ++i)
        {
            vMCount[i].text = vMatOwned[(int)matCompositList[(int)currentCode, i]].ToString(); //素材保有数テキスト

            //素材保有数が、必要数より多いとき
            if (requiredMaterials[(int)currentCode, i] <= vMatOwned[(int)matCompositList[(int)currentCode, i]])
                vMCount[i].color = Color.black; //黒
            else vMCount[i].color = Color.red; //それ以外は赤
        }

        for (int i = 0; i < V_CATEGORY; ++i) vPCount[i].text = vCreationCount[i].ToString(); //ウイルス保有数テキスト
    }

    /// <summary>
    /// 選択UIの位置を取得
    /// </summary>
    /// <param name="code">ウイルスコード</param>
    private void UpdateSelectPosition(int code)
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
}
