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
    public static VIRUS_NUM currentCode; //���ݑI�����Ă���E�C���X�R�[�h

    private bool initFlag;
    private bool[] isCreate = new bool[8]; //�E�C���X�����邩
    private int[] creationArray = new int[9]; //�쐬���z��
    private List<int> tmpList = new List<int>{0, 0, 0, 0}; //��U�A�f�ނ�ۑ����郊�X�g

    //�f�ލ������X�g
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
        currentCode = CODE_NONE; //���݂̃R�[�h�Ȃ�
        creationArray = Enumerable.Repeat(0, creationArray.Length).ToArray(); //0�Ŕz���������
        InitOwnedVirus(); //���݂ۗ̕L���̏�����
    }

    // Update is called once per frame
    void Update()
    {
        debug();
        Debug.Log("���݂̃R�[�h::" + currentCode);

        if (initFlag) return; //�������t���O��true�̂Ƃ��A�������X�L�b�v
        StartCoroutine("InitSetButton"); //�{�^���̏��������蓖��
    }

    /// <summary>
    /// Pushing virus button
    /// </summary>
    /// <param name="code">���݂̃R�[�h</param>
    public void PushVirusButton(int code)
    {
        creationArray[(int)currentCode] = 0; //�쐬�������Z�b�g
        currentCode = (VIRUS_NUM)code; //���݂̃R�[�h��ۑ�
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //�K�v���ɒB���Ă��邩�m�F
    }

    public void PushCreateButton()
    {
        if (currentCode == CODE_NONE) return; //�R�[�h���Ȃ��Ƃ��A�������X�L�b�v

        if (creationArray[(int)currentCode] <= 0) return; //�쐬����0�ȉ��̂Ƃ��A�������X�L�b�v

        SaveCreationVirus(creationArray, currentCode); //�쐬�����E�C���X����ۑ�
        creationArray[(int)currentCode] = 0; //�쐬�������Z�b�g
    }

    public void PushAddButton(int sign)
    {
        //GameObject ob = buttons[0].transform.parent.gameObject;
        //ob.SetActive(false);

        if (currentCode == CODE_NONE) return; //�R�[�h���Ȃ��Ƃ��A�������X�L�b�v

        if (creationArray[(int)currentCode] == 0)
            SaveOwnedVirusMaterial(matCompositList, tmpList, currentCode); //0�̂Ƃ��̑f�ސ���ۑ�

        if (!isCreate[(int)currentCode]) return; //�쐬�ł��Ȃ���Ԃ̂Ƃ��A�������X�L�b�v

        OrganizeVirusMaterialCount(sign); //�f�ޗʂ𐮗�����
    }

    public void PushSubButton(int sign)
    {
        if (currentCode == CODE_NONE) return; //�R�[�h���Ȃ��Ƃ��A�������X�L�b�v

        if (creationArray[(int)currentCode] == 0)
            SaveOwnedVirusMaterial(matCompositList, tmpList, currentCode); //0�̂Ƃ��̑f�ސ���ۑ�

        if (creationArray[(int)currentCode] == 0) return; //�쐬����0�̂Ƃ��A�������X�L�b�v

        OrganizeVirusMaterialCount(sign); //�f�ޗʂ𐮗�����
    }

    /// <summary>
    /// �f�ޗʂ𐮗�����
    /// </summary>
    /// <param name="sign">����</param>
    private void OrganizeVirusMaterialCount(int sign)
    {
        creationArray[(int)currentCode] -= sign; //�쐬�������炷
        CulcOwnedVirus(matCompositList, currentCode, sign); //�쐬�Ɏg�p����f�ނ𑝂₷
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// �{�^���̏��������蓖��
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

        initFlag = ReverseFlag(initFlag); //�������t���O�𔽓]
        yield return null; //�֐����甲����
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
