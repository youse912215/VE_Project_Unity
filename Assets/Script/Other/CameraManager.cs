using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;
using static Call.CommonFunction;
using static TowerDefenceButtonManager;

public class CameraManager : MonoBehaviour
{
    private bool isSetButton;
    private bool isPerChange; //視点変更したか
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
        StartCoroutine("InitSetButton"); //ボタンの初期化割り当て
    }

    /// <summary>
    /// 視点を変更する
    /// </summary>
    private void ChangePerspective()
    {
        if (!isPerChange) return; //視点変更されていないとき、処理をスキップ

        transform.rotation = Quaternion.Euler(rot); //視点を変更
        isPerChange = false; //フラグをfalse
    }

    /// <summary>
    /// Pushing change button
    /// </summary>
    public void PushChangeButton()
    {
        rot.x = ChangeRot(rot.x); //角度を変更
        VirusMenuSetActive(pVirusButton); //メニューの状態を変更
        isPerChange = true; //フラグをtrue
    }

    /// <summary>
    /// ウイルスメニューのアクティブ状態を管理
    /// </summary>
    /// <param name="obj">ゲームオブジェクト</param>
    private void VirusMenuSetActive(GameObject obj)
    {
        if(isActiveButton){
            //非アクティブ状態に
            obj.transform.localPosition = NON_ACTIVE_POS;
            isActiveButton = false;
        }
        else
        {
            //アクティブ状態に
            obj.transform.localPosition = ACTIVE_POS;
            isActiveButton = true;
        }
    }

    /// <summary>
    /// 角度を変更する
    /// </summary>
    /// <param name="x">値</param>
    /// <returns>角度</returns>
    private float ChangeRot(float x)
    {
        x = x == HALF_CIRCLE ? QUARTER_CIRCLE : HALF_CIRCLE;
        return x;
    }

    /// <summary>
    /// ボタンの初期化割り当て
    /// </summary>
    /// <returns></returns>
    IEnumerator InitSetButton()
    {
        changeButton.onClick.AddListener(PushChangeButton);
        isSetButton = true;
        yield return null; //関数から抜ける
    }
}
