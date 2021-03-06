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
        script = GetOtherScriptObject<ActVirus>(obj);
    }

    /// <summary>
    /// Pushing set button
    /// </summary>
    public void SetButtonPush()
    {
        SetVirus(); //ウイルスを設置
        ReverseMenuFlag(BACK); //メニューを閉じる
        script.isOpenMenu = false; //メニューフラグをfalse
    }

    /// <summary>
    /// ウイルスを設置
    /// </summary>
    private void SetVirus()
    {
        GetVirus(script.vParents, script.buttonMode); //ウイルスをアクティブにする
        script.isGrabbedVirus = false; //ボタン操作をfalse
        script.SetUIActivity(true);
    }
}
