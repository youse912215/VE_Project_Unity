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
        ReverseMenuFlag(BACK); //メニューを閉じる
        actV.isOpenMenu = false; //メニューフラグをfalse
        
        if (menuMode) actV.SetUIActivity(true);

        if (menuMode) return; //ボタン状態がtrueのとき、処理をスキップ
        actV.vChildren[actV.column, actV.row].isActivity = true; //再びアクティブ状態に
        actV.isGrabbedVirus = true; //掴んでいない状態
        menuMode = true; //メニューモードをtrue
    }
}
