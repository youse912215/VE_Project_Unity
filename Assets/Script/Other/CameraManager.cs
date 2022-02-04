using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.CommonFunction;
using static ShowMenu;
using static TowerDefenceButtonManager;
using static PrepareVirus;
using static VirusMaterialData;

public class CameraManager : MonoBehaviour
{
    private bool isSetButton;
    private bool isPerChange; //���_�ύX������
    private GameObject pMenuButton;
    public static GameObject pVirusButton;
    public static bool isActiveButton;

    private Vector3 pos;
    private Vector3 rot;

    public Canvas canvas0;
    public Canvas canvas1;

    [SerializeField] private GameObject subCam1;
    [SerializeField] private GameObject subCam2;
    GameObject obj;
    ActVirus actV;

    float wheel;
    bool isWheel;
    public static int currentSetNum;
    [SerializeField] private GameObject wheelUI;

    [SerializeField] private GameObject water;
    [SerializeField] private Material nonActiveMat;
    [SerializeField] private Material activeMat;
    Renderer waterRenderer;

    [SerializeField] private Text currentOwnedText;

    private const float WHEEL_INTERVAL = 0.3f; //�z�C�[���̊Ԋu����
    private readonly Vector2 CLICK_POS = new Vector2(1600.0f, 350.0f); //�N���b�N�ł���ʒu
    private const float MOUSE_DIFF_POS = 55.0f; //�}�E�X�̍������W

    public int owned = 0;

    // Start is called before the first frame update
    void Start()
    {
        pMenuButton = GetHierarchyObject("ParentMenuButton");
        pVirusButton = GetHierarchyObject("ParentVirusButton");

        transform.position = CAM_POS;
        pos = CAM_POS;
        rot = CAM_ROT;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
        isSetButton = false;
        isPerChange = false;
        isActiveButton = true;

        //subCam = GameObject.Find("SubCamera");
        actV = GetOtherScriptObject<ActVirus>(obj);

        wheel = 0.0f;
        isWheel = false;
        //currentSetNum = 0;
        //SetVirusButtonPosition(ACTIVE_POS);
        //wheelUI = GameObject.Find("WheelUI");
        wheelUI.SetActive(false);

        waterRenderer = water.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /* �g�p�\�E�C���X�����X�V */
        owned = vCreationCount[virusSetList[currentSetNum]]
            - vSetCount[virusSetList[currentSetNum]]; //���݂ۗ̕L�E�C���X��UI���X�V
        FixedOutOfRange(); //�͈͊O�̐��l���C��
        currentOwnedText.text = owned.ToString(); //���݂ۗ̕L�E�C���X��UI���X�V

        /* �z�C�[���X�V���� */
        wheel = Input.GetAxis("Mouse ScrollWheel"); //�z�C�[�����擾
        ChangeWheelUIActivity(); //�z�C�[��UI�̃A�N�e�B�u��Ԃ̕ύX
        ChangePerspective(); //���_�ύX

        /* �{�^������������ */
        if (isSetButton) return;
        StartCoroutine("InitSetButton"); //�{�^���̏��������蓖��
    }

    /// <summary>
    /// �͈͊O�̐��l���C��
    /// </summary>
    void FixedOutOfRange()
    {
        if (owned > -1) return;
        owned = 0;
    }

    /// <summary>
    /// �z�C�[��UI�̃A�N�e�B�u��Ԃ̕ύX
    /// </summary>
    private void ChangeWheelUIActivity()
    {
        if (currentOwnedText.enabled && WheelPos())
        {
            wheelUI.SetActive(true); //�z�C�[�����A�N�e�B�u��
            wheelUI.transform.position =
                new Vector3(mousePos.x + MOUSE_DIFF_POS, mousePos.y + MOUSE_DIFF_POS, 0.0f); //�}�E�X�̍������W�����Z
            waterRenderer.material = activeMat; //�}�e���A�����A�N�e�B�u��
            if (wheel != 0.0f) StartCoroutine("ChangeVirusSets"); //�E�C���XUI��ؑ�
        }
        else
        {
            wheelUI.SetActive(false); //�z�C�[�����A�N�e�B�u��
            waterRenderer.material = nonActiveMat; //�}�e���A�����A�N�e�B�u��
        }
    }

    /// <summary>
    /// ���_��ύX����
    /// </summary>
    private void ChangePerspective()
    {
        if (!isPerChange) return; //���_�ύX����Ă��Ȃ��Ƃ��A�������X�L�b�v

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot); //���_��ύX
        isPerChange = false; //�t���O��false
    }

    /// <summary>
    /// Pushing change button
    /// </summary>
    public void PushChangeButton()
    {
        if (actV.isGrabbedVirus) return;

        pos = ChangeTransformCamera(pos, CAM_POS, CAM_P_POS); //�ʒu��ύX
        rot = ChangeTransformCamera(rot, CAM_ROT, CAM_P_ROT); //�p�x��ύX
        VirusMenuSetActive(); //���j���[�̏�Ԃ�ύX
        isPerChange = true; //�t���O��true
    }

    /// <summary>
    /// �E�C���X���j���[�̃A�N�e�B�u��Ԃ��Ǘ�
    /// </summary>
    private void VirusMenuSetActive()
    {
        if(isActiveButton){
            //��A�N�e�B�u��Ԃ�
            SetVirusButtonPosition(NON_ACTIVE_POS);
            isActiveButton = false;
            subCam1.SetActive(false);
            subCam2.SetActive(false);
            actV.comCanvas.SetActive(false);
            currentOwnedText.enabled = false;        
        }
        else
        {
            //�A�N�e�B�u��Ԃ�
            SetVirusButtonPosition(ACTIVE_POS);
            isActiveButton = true;
            subCam1.SetActive(true);
            subCam2.SetActive(true);
            actV.comCanvas.SetActive(true);
            currentOwnedText.enabled = true;
        }
    }

    /// <summary>
    /// �p�x��ύX����
    /// </summary>
    /// <param name="y">�l</param>
    /// <returns>�p�x</returns>
    private Vector3 ChangeTransformCamera(Vector3 y, Vector3 x1, Vector3 x2)
    {
        y = y == x2 ? x1 : x2;
        return y;
    }

    /// <summary>
    /// �{�^���̏��������蓖��
    /// </summary>
    /// <returns></returns>
    IEnumerator InitSetButton()
    {
        changeButton.onClick.AddListener(PushChangeButton);
        isSetButton = true;
        yield return null; //�֐����甲����
    }

    /// <summary>
    /// �E�C���XUI�ؑ�
    /// �ؑ֎��̃^�C�~���O�𒲐�����R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeVirusSets()
    {
        isWheel = true;
        yield return new WaitForSeconds(WHEEL_INTERVAL);

        if (isWheel)
        {
            SetVirusButtonPosition(NON_ACTIVE_POS);
            currentSetNum = currentSetNum == 2 ? 0 : ++currentSetNum;
            SetVirusButtonPosition(ACTIVE_POS);
        }
        isWheel = false;
    }

    /// <summary>
    /// �E�C���X�{�^���̈ʒu���Z�b�g
    /// </summary>
    /// <param name="pos"></param>
    public static void SetVirusButtonPosition(Vector3 pos)
    {
        pVirusButton.transform.GetChild(virusSetList[currentSetNum]).transform.localPosition = pos;
    }

    /// <summary>
    /// �z�C�[���̍��W�ƃN���b�N�\���W�̈ʒu�֌W��Ԃ�
    /// </summary>
    /// <returns></returns>
    private bool WheelPos()
    {
        return mousePos.x >= CLICK_POS.x && mousePos.y <= CLICK_POS.y;
    }
}
