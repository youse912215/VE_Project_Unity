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
        //canvas0.transform.rotation = Camera.main.transform.rotation; //HPを真上からでも見えるようにする
        //canvas1.transform.rotation = Camera.main.transform.rotation; //HPを真上からでも見えるようにする

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

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot); //視点を変更
        isPerChange = false; //フラグをfalse
    }

    /// <summary>
    /// Pushing change button
    /// </summary>
    public void PushChangeButton()
    {
        pos = ChangeTransformCamera(pos, CAM_POS, CAM_P_POS); //位置を変更
        rot = ChangeTransformCamera(rot, CAM_ROT, CAM_P_ROT); //角度を変更
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
    /// <param name="y">値</param>
    /// <returns>角度</returns>
    private Vector3 ChangeTransformCamera(Vector3 y, Vector3 x1, Vector3 x2)
    {
        y = y == x2 ? x1 : x2;
        return y;
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
