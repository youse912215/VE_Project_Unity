using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;
using static Call.CommonFunction;
using static ShowMenu;
using static TowerDefenceButtonManager;

public class CameraManager : MonoBehaviour
{
    private bool isSetButton;
    private bool isPerChange; //���_�ύX������
    private GameObject pMenuButton;
    private GameObject pVirusButton;
    public static bool isActiveButton;

    private Vector3 pos;
    private Vector3 rot;

    public Canvas canvas0;
    public Canvas canvas1;

    GameObject subCam;
    GameObject obj;
    ActVirus actV;

    float wheel;
    bool isWheel;
    int currentSetNum;
    private GameObject wheelUI;

    [SerializeField] private GameObject water;
    [SerializeField] private Material nonActiveMat;
    [SerializeField] private Material activeMat;
    Renderer waterRenderer;

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

        subCam = GameObject.Find("SubCamera");
        actV = GetOtherScriptObject<ActVirus>(obj);

        wheel = 0.0f;
        isWheel = false;
        currentSetNum = 1;
        SetVirusButtonPosition(ACTIVE_POS);
        wheelUI = GameObject.Find("WheelUI");
        wheelUI.SetActive(false);

        waterRenderer = water.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //canvas0.transform.rotation = Camera.main.transform.rotation; //HP��^�ォ��ł�������悤�ɂ���
        //canvas1.transform.rotation = Camera.main.transform.rotation; //HP��^�ォ��ł�������悤�ɂ���

        wheel = Input.GetAxis("Mouse ScrollWheel");
        if (WheelPos())
        {
            wheelUI.SetActive(true);
            wheelUI.transform.position = new Vector3(mousePos.x + 55.0f, mousePos.y + 55.0f, 0.0f);
            waterRenderer.material = activeMat;
            if (wheel != 0.0f) StartCoroutine("sample");
        }
        else
        {
            wheelUI.SetActive(false);
            waterRenderer.material = nonActiveMat;
        }

        ChangePerspective();

        if (isSetButton) return;
        StartCoroutine("InitSetButton"); //�{�^���̏��������蓖��
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
            subCam.SetActive(false);
        }
        else
        {
            //�A�N�e�B�u��Ԃ�
            SetVirusButtonPosition(ACTIVE_POS);
            isActiveButton = true;
            subCam.SetActive(true);
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

    IEnumerator sample()
    {
        isWheel = true;
        yield return new WaitForSeconds(0.8f);

        if (isWheel)
        {
            SetVirusButtonPosition(NON_ACTIVE_POS);
            currentSetNum = currentSetNum == 2 ? 0 : ++currentSetNum;
            SetVirusButtonPosition(ACTIVE_POS);
        }
        isWheel = false;
    }

    private void SetVirusButtonPosition(Vector3 pos)
    {
        pVirusButton.transform.GetChild(currentSetNum).transform.localPosition = pos;
    }

    private bool WheelPos()
    {
        return mousePos.x >= 1600.0f && mousePos.y <= 350.0f;
    }
}
