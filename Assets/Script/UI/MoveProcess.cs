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
        actV.vChildren[actV.column, actV.row].isActivity = true; //指定のウイルスをアクティブ状態に
        ReverseMenuFlag(BACK); //メニューを閉じる
        actV.isOpenMenu = false; //メニューフラグをfalse
        menuMode = false; //メニューモードを設置前に
        actV.isGrabbedVirus = true; //掴んでいる状態に
    }
}
