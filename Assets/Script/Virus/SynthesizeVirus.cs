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
    public static VIRUS_NUM currentCode; //���ݑI�����Ă���E�C���X�R�[�h

    private bool initFlag;
    private bool isPushing; //�E�C���X�{�^������������
    private bool[] isCreate = new bool[8]; //�E�C���X�����邩
    private int[] creationArray = new int[9]; //�쐬���z��
    private List<int> tmpList = new List<int> { 0, 0, 0, 0 }; //��U�A�f�ނ�ۑ����郊�X�g

    private GameObject selectUI;
    private RectTransform selRect;

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

    //���݂̃E�C���X�ۗL�ʃe�L�X�g
    private Text[] vCount = new Text[V_CATEGORY];
    private Text[] vArray = new Text[1];
    private GameObject[] vCountText = new GameObject[V_CATEGORY];
    private GameObject[] vArrayText = new GameObject[1];

    //���݂̃E�C���X�쐬�f�ޕۗL�ʃe�L�X�g
    private Text[] vMCount = new Text[4];
    private GameObject[] vMCountText = new GameObject[4];

    private Text[] vPCount = new Text[V_CATEGORY];
    private GameObject[] vPCountText = new GameObject[V_CATEGORY];

    // Start is called before the first frame update
    void Start()
    {
        isPushing = false;

        currentCode = CODE_CLD; //���݂̃R�[�h�Ȃ�
        creationArray = Enumerable.Repeat(0, creationArray.Length).ToArray(); //0�Ŕz���������
        InitOwnedVirus(); //���݂ۗ̕L���̏�����

        InitText(V_CATEGORY, "vCount", vCountText, vCount); //�e�L�X�g�̏�����
        InitText(1, "cArray", vArrayText, vArray);
        InitText(tmpList.Count(), "vMCount", vMCountText, vMCount);
        InitText(V_CATEGORY, "vPCount", vPCountText, vPCount);

        InitUI(); //UI�̏�����

        PushVirusButton((int)currentCode);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!isPushing) return;
        UpdateCountText(); //���݂̃J�E���g�e�L�X�g���X�V
        isPushing = false; //�{�^��������off

        if (initFlag) return; //�������t���O��true�̂Ƃ��A�������X�L�b�v
        StartCoroutine("InitSetButton"); //�{�^���̏��������蓖��
    }

    /// <summary>
    /// Pushing virus button
    /// </summary>
    /// <param name="code">���݂̃R�[�h</param>
    public void PushVirusButton(int code)
    {
        isPushing = true;

        UpdateSelectPosition(code); //�I���ʒu�X�V

        if (creationArray[(int)currentCode] != 0)
            ResetOwnedVirusMaterial(matCompositList, tmpList, currentCode);

        creationArray[(int)currentCode] = 0; //�쐬�������Z�b�g
        currentCode = (VIRUS_NUM)code; //���݂̃R�[�h��ۑ�
        isCreate[(int)currentCode] = CheckMaterialCount(matCompositList, currentCode); //�K�v���ɒB���Ă��邩�m�F
    }

    /// <summary>
    /// Pushing creation button
    /// </summary>
    public void PushCreateButton()
    {
        isPushing = true;

        if (currentCode == CODE_NONE) return; //�R�[�h���Ȃ��Ƃ��A�������X�L�b�v

        if (creationArray[(int)currentCode] <= 0) return; //�쐬����0�ȉ��̂Ƃ��A�������X�L�b�v

        SaveCreationVirus(creationArray, currentCode); //�쐬�����E�C���X����ۑ�
        creationArray[(int)currentCode] = 0; //�쐬�������Z�b�g
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

        if (currentCode == CODE_NONE) return; //�R�[�h���Ȃ��Ƃ��A�������X�L�b�v

        if (creationArray[(int)currentCode] == 0)
            SaveOwnedVirusMaterial(matCompositList, tmpList, currentCode); //0�̂Ƃ��̑f�ސ���ۑ�

        if (!isCreate[(int)currentCode]) return; //�쐬�ł��Ȃ���Ԃ̂Ƃ��A�������X�L�b�v

        OrganizeVirusMaterialCount(sign); //�f�ޗʂ𐮗�����
    }

    /// <summary>
    /// Pushing subtraction button
    /// </summary>
    /// <param name="sign"></param>
    public void PushSubButton(int sign)
    {
        isPushing = true;

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

        initFlag = ReverseFlag(initFlag); //�������t���O�𔽓]
        yield return null; //�֐����甲����
    }

    /// <summary>
    /// �e�L�X�g�I�u�W�F�N�g�̏�����
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
    /// UI�̏�����
    /// </summary>
    private void InitUI()
    {
        selectUI = GameObject.Find("CurrentSelect");
        selRect = selectUI.GetComponent<RectTransform>();
        selRect.localPosition = SELECT_UI_POS; //�����ʒu�ɃZ�b�g
    }

    /// <summary>
    /// ���݂̃J�E���g�e�L�X�g�����X�V
    /// </summary>
    private void UpdateCountText()
    {
        if (currentCode == CODE_NONE) return;

        for (int i = 0; i < V_CATEGORY; ++i) vCount[i].text = vCreationCount[i].ToString(); //�E�C���X�ۗL���e�L�X�g
        vArray[0].text = creationArray[(int)currentCode].ToString(); //���v�쐬���e�L�X�g

        for (int i = 0; i < tmpList.Count(); ++i)
        {
            vMCount[i].text = vMatOwned[(int)matCompositList[(int)currentCode, i]].ToString(); //�f�ޕۗL���e�L�X�g

            //�f�ޕۗL�����A�K�v����葽���Ƃ�
            if (requiredMaterials[(int)currentCode, i] <= vMatOwned[(int)matCompositList[(int)currentCode, i]])
                vMCount[i].color = Color.black; //��
            else vMCount[i].color = Color.red; //����ȊO�͐�
        }

        for (int i = 0; i < V_CATEGORY; ++i) vPCount[i].text = vCreationCount[i].ToString(); //�E�C���X�ۗL���e�L�X�g
    }

    /// <summary>
    /// �I��UI�̈ʒu���擾
    /// </summary>
    /// <param name="code">�E�C���X�R�[�h</param>
    private void UpdateSelectPosition(int code)
    {
        selRect.localPosition = new Vector3(
            SELECT_UI_POS.x,
            SELECT_UI_POS.y - CulculationSelectPos(code),
            SELECT_UI_POS.z);
    }

    /// <summary>
    /// �I��UI�̈ʒu�̌v�Z
    /// </summary>
    /// <param name="code">�E�C���X�R�[�h</param>
    /// <returns></returns>
    private float CulculationSelectPos(int code)
    {
        return code == (int)CODE_ULT ? BUTTON_HEIGHT * 5.0f : (BUTTON_HEIGHT * code);
    }
}
