using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;
using static Call.CommonFunction;
using static TowerDefenceButtonManager;

public class CameraManager : MonoBehaviour
{
    private bool isSetButton;
    private bool isPerChange; //���_�ύX������
    private GameObject pMenuButton;
    private GameObject pVirusButton;
    private bool isActiveButton;

    private Vector3 pos;
    private Vector3 rot;

    public Canvas canvas0;
    public Canvas canvas1;

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
    }

    // Update is called once per frame
    void Update()
    {
        //canvas0.transform.rotation = Camera.main.transform.rotation; //HP��^�ォ��ł�������悤�ɂ���
        //canvas1.transform.rotation = Camera.main.transform.rotation; //HP��^�ォ��ł�������悤�ɂ���

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
        pos = ChangeTransformCamera(pos, CAM_POS, CAM_P_POS); //�ʒu��ύX
        rot = ChangeTransformCamera(rot, CAM_ROT, CAM_P_ROT); //�p�x��ύX
        VirusMenuSetActive(pVirusButton); //���j���[�̏�Ԃ�ύX
        isPerChange = true; //�t���O��true
    }

    /// <summary>
    /// �E�C���X���j���[�̃A�N�e�B�u��Ԃ��Ǘ�
    /// </summary>
    /// <param name="obj">�Q�[���I�u�W�F�N�g</param>
    private void VirusMenuSetActive(GameObject obj)
    {
        if(isActiveButton){
            //��A�N�e�B�u��Ԃ�
            obj.transform.localPosition = NON_ACTIVE_POS;
            isActiveButton = false;
        }
        else
        {
            //�A�N�e�B�u��Ԃ�
            obj.transform.localPosition = ACTIVE_POS;
            isActiveButton = true;
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
}
