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
    private VIRUS_NUM currentCode; //���ݑI�����Ă���E�C���X�R�[�h

    private bool initFlag;
    private bool[] isCreate = new bool[8]; //�E�C���X�����邩
    private int creationNum; //�쐬��

    // Start is called before the first frame update
    void Start()
    {
        currentCode = 0;
        vMatOwned[0] = 2;
        vMatOwned[1] = 3;
        vMatOwned[2] = 1;
        vMatOwned[17] = 1;

        creationNum = 0; //0�ŏ�����
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
        currentCode = CODE_CLD; //CLD�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(V1_PROTEIN, V2_PROTEIN, V3_PROTEIN, BLUE_POTION, CODE_CLD); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);

    }

    /// <summary>
    /// Pushing inf button
    /// </summary>
    public void PushInfButton()
    {
        currentCode = CODE_INF; //INF�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(ERYTHROCYTE_AGGLUTININ, NEURAMINIDASE, ENVELOPE, RED_POTION, CODE_INF); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing c19 button
    /// </summary>
    public void Push19Button()
    {
        currentCode = CODE_19; //19�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(S_PROTEIN, N_PROTEIN, ENVELOPE, RED_POTION, CODE_19); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing nov button
    /// </summary>
    public void PushNovButton()
    {
        currentCode = CODE_NOV; //NOV�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(PROTEASE, V1_PROTEIN, V2_PROTEIN, BLUE_POTION, CODE_NOV); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing ehf button
    /// </summary>
    public void PushEhfButton()
    {
        currentCode = CODE_EHF; //EHF�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(V24_PROTEIN, V40_PROTEIN, NUCLEOCCAPSID, YELLOW_POTION, CODE_EHF); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing ev button
    /// </summary>
    public void PushEvButton()
    {
        currentCode = CODE_EV; //EV�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(FEN1_PROTEIN, POLYMERASE, ENVELOPE, BLUE_POTION, CODE_EV); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing bd button
    /// </summary>
    public void PushBdButton()
    {
        currentCode = CODE_BD; //BD�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(V3_PROTEIN, F1_ANTIGEN, V_ANTIGEN, RED_POTION, CODE_BD); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    /// <summary>
    /// pushing ult button
    /// </summary>
    public void PushUltButton()
    {
        currentCode = CODE_ULT; //ULT�I��
        isCreate[(int)currentCode] =
            CheckMaterialCount(ULX_PROTEIN, ERYTHROCYTE_AGGLUTININ, ENVELOPE, BLOOD_POTION, CODE_ULT); //�K�v���ɒB���Ă��邩�m�F
        DebugFunc(currentCode);
    }

    public void PushAddButton()
    {
        �@creationNum++; //�쐬���𑝂₷
    }

    /// <summary>
    /// �{�^���̏��������蓖��
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
