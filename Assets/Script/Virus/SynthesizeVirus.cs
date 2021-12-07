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

    private Text[] vCount = new Text[V_CATEGORY];
    private Text vArray;
    private GameObject[] countText = new GameObject[V_CATEGORY];
    private GameObject arrayText;

    // Start is called before the first frame update
    void Start()
    {
        currentCode = CODE_CLD; //���݂̃R�[�h�Ȃ�
        creationArray = Enumerable.Repeat(0, creationArray.Length).ToArray(); //0�Ŕz���������
        InitOwnedVirus(); //���݂ۗ̕L���̏�����

        InitText(); //�e�L�X�g�̏�����
        InitUI(); //UI�̏�����
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("tmp" + tmpList[0] + "::" + tmpList[1] + "::" + tmpList[2] + "::" + tmpList[3]);
        Debug.Log("0::" + vMatOwned[0]);
        Debug.Log("1::" + vMatOwned[1]);
        Debug.Log("2::" + vMatOwned[2]);
        Debug.Log("17::" + vMatOwned[17]);

        ShowCurrentCounts();

        if (initFlag) return; //�������t���O��true�̂Ƃ��A�������X�L�b�v
        StartCoroutine("InitSetButton"); //�{�^���̏��������蓖��
    }

    /// <summary>
    /// Pushing virus button
    /// </summary>
    /// <param name="code">���݂̃R�[�h</param>
    public void PushVirusButton(int code)
    {
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
    /// UI�̏�����
    /// </summary>
    private void InitUI()
    {
        selectUI = GameObject.Find("CurrentSelect");
        selRect = selectUI.GetComponent<RectTransform>();
        selRect.localPosition = SELECT_UI_POS; //�����ʒu�ɃZ�b�g
    }

    /// <summary>
    /// ���݂̍쐬������\��
    /// </summary>
    private void ShowCurrentCounts()
    {
        if (currentCode == CODE_NONE) return;
        vCount[0].text = vCreationCount[0].ToString();
        vCount[1].text = vCreationCount[1].ToString();
        vCount[2].text = vCreationCount[2].ToString();
        vCount[3].text = vCreationCount[3].ToString();
        vCount[4].text = vCreationCount[4].ToString();
        vCount[5].text = vCreationCount[5].ToString();
        vCount[6].text = vCreationCount[6].ToString();
        vCount[7].text = vCreationCount[7].ToString();
        vArray.text = creationArray[(int)currentCode].ToString();
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
