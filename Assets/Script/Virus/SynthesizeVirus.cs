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
    private VIRUS_NUM currentCode; //���ݑI�����Ă���E�C���X�R�[�h

    private bool isCreate; //�E�C���X�����邩

    // Start is called before the first frame update
    void Start()
    {
        InitSetButton(); //�{�^���̏���������

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
       currentCode = CODE_CLD; //CLD�I��
       isCreate = CheckMaterialCount(V1_PROTEIN, V2_PROTEIN, V3_PROTEIN, BLUE_POTION, CODE_CLD); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// Pushing inf button
    /// </summary>
    public void PushInfButton()
    {
        isCreate = false;
        currentCode = CODE_INF; //INF�I��
        isCreate = CheckMaterialCount(ERYTHROCYTE_AGGLUTININ, NEURAMINIDASE, ENVELOPE, RED_POTION, CODE_INF); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// pushing c19 button
    /// </summary>
    public void Push19Button()
    {
        isCreate = false;
        currentCode = CODE_19; //19�I��
        isCreate = CheckMaterialCount(S_PROTEIN, N_PROTEIN, ENVELOPE, RED_POTION, CODE_19); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// pushing nov button
    /// </summary>
    public void PushNovButton()
    {
        isCreate = false;
        currentCode = CODE_NOV; //NOV�I��
        isCreate = CheckMaterialCount(PROTEASE, V1_PROTEIN, V2_PROTEIN, BLUE_POTION, CODE_NOV); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// pushing ehf button
    /// </summary>
    public void PushEhfButton()
    {
        isCreate = false;
        currentCode = CODE_EHF; //EHF�I��
        isCreate = CheckMaterialCount(V24_PROTEIN, V40_PROTEIN, NUCLEOCCAPSID, YELLOW_POTION, CODE_EHF); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// pushing ev button
    /// </summary>
    public void PushEvButton()
    {
        isCreate = false;
        currentCode = CODE_EV; //EV�I��
        isCreate = CheckMaterialCount(FEN1_PROTEIN, POLYMERASE, ENVELOPE, BLUE_POTION, CODE_EV); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// pushing bd button
    /// </summary>
    public void PushBdButton()
    {
        isCreate = false;
        currentCode = CODE_BD; //BD�I��
        isCreate = CheckMaterialCount(V3_PROTEIN, F1_ANTIGEN, V_ANTIGEN, RED_POTION, CODE_BD); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// pushing ult button
    /// </summary>
    public void PushUltButton()
    {
        isCreate = false;
        currentCode = CODE_ULT; //ULT�I��
        isCreate = CheckMaterialCount(ULX_PROTEIN, ERYTHROCYTE_AGGLUTININ, ENVELOPE, BLOOD_POTION, CODE_ULT); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// �{�^���̏��������蓖��
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
