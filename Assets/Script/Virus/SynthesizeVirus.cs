using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static DebugManager;
using static VirusMaterialData;
using static VirusMaterialData.MATERIAL_CODE;
using static SimulationButtonManager;

public class SynthesizeVirus : MonoBehaviour
{
    private VIRUS_NUM currentCode; //現在選択しているウイルスコード

    private bool initFlag;
    private bool[] isCreate = new bool[8]; //ウイルスを作れるか
    private int creationNum; //作成数

    // Start is called before the first frame update
    void Start()
    {
        currentCode = 0;
        vMatOwned[0] = 2;
        vMatOwned[1] = 3;
        vMatOwned[2] = 1;
        vMatOwned[17] = 1;

        creationNum = 0; //0個で初期化
    }

    // Update is called once per frame
    void Update()
    {
        DebugFunc(isCreate[(int)currentCode]);
        if (initFlag)return;
        StartCoroutine("InitSetButton");
    }

    /// <summary>
    /// Pushing cld button
    /// </summary>
    public void PushCldButton()
    {
        currentCode = CODE_CLD; //CLD選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(V1_PROTEIN, V2_PROTEIN, V3_PROTEIN, BLUE_POTION, CODE_CLD); //必要個数に達しているか確認
        DebugFunc(currentCode);

    }

    /// <summary>
    /// Pushing inf button
    /// </summary>
    public void PushInfButton()
    {
        currentCode = CODE_INF; //INF選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(ERYTHROCYTE_AGGLUTININ, NEURAMINIDASE, ENVELOPE, RED_POTION, CODE_INF); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing c19 button
    /// </summary>
    public void Push19Button()
    {
        currentCode = CODE_19; //19選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(S_PROTEIN, N_PROTEIN, ENVELOPE, RED_POTION, CODE_19); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing nov button
    /// </summary>
    public void PushNovButton()
    {
        currentCode = CODE_NOV; //NOV選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(PROTEASE, V1_PROTEIN, V2_PROTEIN, BLUE_POTION, CODE_NOV); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing ehf button
    /// </summary>
    public void PushEhfButton()
    {
        currentCode = CODE_EHF; //EHF選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(V24_PROTEIN, V40_PROTEIN, NUCLEOCCAPSID, YELLOW_POTION, CODE_EHF); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing ev button
    /// </summary>
    public void PushEvButton()
    {
        currentCode = CODE_EV; //EV選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(FEN1_PROTEIN, POLYMERASE, ENVELOPE, BLUE_POTION, CODE_EV); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing bd button
    /// </summary>
    public void PushBdButton()
    {
        currentCode = CODE_BD; //BD選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(V3_PROTEIN, F1_ANTIGEN, V_ANTIGEN, RED_POTION, CODE_BD); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing ult button
    /// </summary>
    public void PushUltButton()
    {
        currentCode = CODE_ULT; //ULT選択中
        isCreate[(int)currentCode] =
            CheckMaterialCount(ULX_PROTEIN, ERYTHROCYTE_AGGLUTININ, ENVELOPE, BLOOD_POTION, CODE_ULT); //必要個数に達しているか確認
        DebugFunc(currentCode);
    }

    public void PushAddButton()
    {
        　creationNum++; //作成数を増やす
    }

    /// <summary>
    /// ボタンの初期化割り当て
    /// </summary>
    IEnumerator InitSetButton()
    {
        buttons[0].onClick.AddListener(PushCldButton);
        buttons[1].onClick.AddListener(PushInfButton);
        buttons[2].onClick.AddListener(Push19Button);
        buttons[3].onClick.AddListener(PushNovButton);
        buttons[4].onClick.AddListener(PushEhfButton);
        buttons[5].onClick.AddListener(PushEvButton);
        buttons[6].onClick.AddListener(PushBdButton);
        buttons[7].onClick.AddListener(PushUltButton);
        initFlag = ReverseFlag(initFlag);
        yield return null;
    }
}
