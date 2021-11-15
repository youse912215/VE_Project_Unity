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

    private Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        pMenuButton = GetHierarchyObject("ParentMenuButton");
        pVirusButton = GetHierarchyObject("ParentVirusButton");

        transform.position = CAM_POS;
        rot = CAM_ROT;
        transform.rotation = Quaternion.Euler(rot);
        isSetButton = false;
        isPerChange = false;
        isActiveButton = true;
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.rotation = Quaternion.Euler(rot); //���_��ύX
        isPerChange = false; //�t���O��false
    }

    /// <summary>
    /// Pushing change button
    /// </summary>
    public void PushChangeButton()
    {
        rot.x = ChangeRot(rot.x); //�p�x��ύX
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
    /// <param name="x">�l</param>
    /// <returns>�p�x</returns>
    private float ChangeRot(float x)
    {
        x = x == HALF_CIRCLE ? QUARTER_CIRCLE : HALF_CIRCLE;
        return x;
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
