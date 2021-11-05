using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue.MENU_TYPE;
using static Call.CommonFunction;
using static Call.VirusData;
using static ShowMenu;

public class SetProcess : MonoBehaviour
{
    GameObject obj;
    ActVirus script;

    private void Start()
    {
        script = GetActVirusScript<ActVirus>(obj);
    }

    /// <summary>
    /// Pushing set button
    /// </summary>
    public void SetButtonPush()
    {
        SetVirus(); //�E�C���X��ݒu
        ReverseMenuFlag(BACK); //���j���[�����
        script.isOpenMenu = false; //���j���[�t���O��false
    }

    /// <summary>
    /// �E�C���X��ݒu
    /// </summary>
    private void SetVirus()
    {
        GetVirus((VIRUS_NUM)script.buttonMode, script.vParents); //�E�C���X���A�N�e�B�u�ɂ���
        script.isGrabbedVirus = false; //�{�^�������false
    }
}
