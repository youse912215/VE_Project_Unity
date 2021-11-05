using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;

public class MoveProcess : MonoBehaviour
{
    GameObject obj;
    ActVirus actV;

    // Start is called before the first frame update
    void Start()
    {
        actV = GetActVirusScript<ActVirus>(obj);
    }

    /// <summary>
    /// pushing move button
    /// </summary>
    public void MoveButtonPush()
    {
        actV.vChildren[actV.column, actV.row].isActivity = true; //�w��̃E�C���X���A�N�e�B�u��Ԃ�
        ReverseMenuFlag(BACK); //���j���[�����
        actV.isOpenMenu = false; //���j���[�t���O��false
        menuMode = false; //���j���[���[�h��ݒu�O��
        actV.isGrabbedVirus = true; //�͂�ł����Ԃ�
    }
}
