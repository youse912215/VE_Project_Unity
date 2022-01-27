using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;

public class BackProcess : MonoBehaviour
{
    GameObject obj;
    ActVirus actV;

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj);
    }

    /// <summary>
    /// Pushing back button
    /// </summary>
    public void BackButtonPush()
    {
        ReverseMenuFlag(BACK); //���j���[�����
        actV.isOpenMenu = false; //���j���[�t���O��false
        
        if (menuMode) actV.SetUIActivity(true);

        if (menuMode) return; //�{�^����Ԃ�true�̂Ƃ��A�������X�L�b�v
        actV.vChildren[actV.column, actV.row].isActivity = true; //�ĂуA�N�e�B�u��Ԃ�
        actV.isGrabbedVirus = true; //�͂�ł��Ȃ����
        menuMode = true; //���j���[���[�h��true
    }
}
