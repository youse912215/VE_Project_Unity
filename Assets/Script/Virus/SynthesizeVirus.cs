using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static DebugManager;
using static VirusMaterialData;
using static VirusMaterialData.MATERIAL_CODE;
using static SimulationButtonManager;

public class SynthesizeVirus : MonoBehaviour
{
    private VIRUS_NUM currentCode; //現在選択しているウイルスコード

    private bool isCreate; //ウイルスを作れるか

    // Start is called before the first frame update
    void Start()
    {
        InitSetButton(); //ボタンの初期化処理

        currentCode = CODE_NONE;
        vMatOwned[0] = 2;
        vMatOwned[1] = 3;
        vMatOwned[2] = 1;
        vMatOwned[17] = 0;
        isCreate = false;
    }

    // Update is called once per frame
    void Update()
    {
        DebugFunc(currentCode);
        
    }

    /// <summary>
    /// Pushing cld button
    /// </summary>
    public void PushCldButton()
    {
       isCreate = false;
       currentCode = CODE_CLD; //CLD選択中
       isCreate = CheckMaterialCount(V1_PROTEIN, V2_PROTEIN, V3_PROTEIN, BLUE_POTION, CODE_CLD); //必要個数に達しているか確認
    }

    /// <summary>
    /// Pushing inf button
    /// </summary>
    public void PushInfButton()
    {
        isCreate = false;
        currentCode = CODE_INF; //INF選択中
        isCreate = CheckMaterialCount(ERYTHROCYTE_AGGLUTININ, NEURAMINIDASE, ENVELOPE, RED_POTION, CODE_INF); //必要個数に達しているか確認
    }

    /// <summary>
    /// pushing c19 button
    /// </summary>
    public void Push19Button()
    {
        isCreate = false;
        currentCode = CODE_19; //19選択中
        isCreate = CheckMaterialCount(S_PROTEIN, N_PROTEIN, ENVELOPE, RED_POTION, CODE_19); //必要個数に達しているか確認
    }

    /// <summary>
    /// pushing nov button
    /// </summary>
    public void PushNovButton()
    {
        isCreate = false;
        currentCode = CODE_NOV; //NOV選択中
        isCreate = CheckMaterialCount(PROTEASE, V1_PROTEIN, V2_PROTEIN, BLUE_POTION, CODE_NOV); //必要個数に達しているか確認
    }

    /// <summary>
    /// pushing ehf button
    /// </summary>
    public void PushEhfButton()
    {
        isCreate = false;
        currentCode = CODE_EHF; //EHF選択中
        isCreate = CheckMaterialCount(V24_PROTEIN, V40_PROTEIN, NUCLEOCCAPSID, YELLOW_POTION, CODE_EHF); //必要個数に達しているか確認
    }

    /// <summary>
    /// pushing ev button
    /// </summary>
    public void PushEvButton()
    {
        isCreate = false;
        currentCode = CODE_EV; //EV選択中
        isCreate = CheckMaterialCount(FEN1_PROTEIN, POLYMERASE, ENVELOPE, BLUE_POTION, CODE_EV); //必要個数に達しているか確認
    }

    /// <summary>
    /// pushing bd button
    /// </summary>
    public void PushBdButton()
    {
        isCreate = false;
        currentCode = CODE_BD; //BD選択中
        isCreate = CheckMaterialCount(V3_PROTEIN, F1_ANTIGEN, V_ANTIGEN, RED_POTION, CODE_BD); //必要個数に達しているか確認
    }

    /// <summary>
    /// pushing ult button
    /// </summary>
    public void PushUltButton()
    {
        isCreate = false;
        currentCode = CODE_ULT; //ULT選択中
        isCreate = CheckMaterialCount(ULX_PROTEIN, ERYTHROCYTE_AGGLUTININ, ENVELOPE, BLOOD_POTION, CODE_ULT); //必要個数に達しているか確認
    }

    /// <summary>
    /// ボタンの初期化割り当て
    /// </summary>
    private void InitSetButton()
    {
        buttons[0].onClick.AddListener(PushCldButton);
        buttons[1].onClick.AddListener(PushInfButton);
        buttons[2].onClick.AddListener(Push19Button);
        buttons[3].onClick.AddListener(PushNovButton);
        buttons[4].onClick.AddListener(PushEhfButton);
        buttons[5].onClick.AddListener(PushEvButton);
        buttons[6].onClick.AddListener(PushBdButton);
        buttons[7].onClick.AddListener(PushUltButton);
    }
}
