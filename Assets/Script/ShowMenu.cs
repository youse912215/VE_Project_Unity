using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;
using static Call.ConstantValue.MENU_TYPE;

public class ShowMenu : MonoBehaviour
{
    public static Vector3 mousePos; //マウス座標
    public static Camera cam; //カメラオブジェクト

    public static bool isOpenMenu;
    private const int MENU_TYPE = 5;
    public static bool[] isMenuFlag = new bool[MENU_TYPE];
    public static bool[] menuMode = new bool[MENU_TYPE];

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //メインカメラ取得
        transform.position = INIT_MENU_POS; //初期位置（画面外）
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenuFlag[(int)OPEN]) SetMenu(); //メニューをセット
        if (isMenuFlag[(int)BACK]) RemoveMenu(); //メニューをリムーブ
    }

    /// <summary>
    /// スクリーン上のマウス座標を返す
    /// </summary>
    public static Vector3 ReturnOnScreenMousePos()
    {
        mousePos = Input.mousePosition; //マウスの座標を取得
        mousePos.z = CAM_DISTANCE; //マウスz座標にカメラとの距離を代入
        return cam.ScreenToWorldPoint(mousePos); //スクリーン→ワールド変換
    }

    /// <summary>
    /// メニューを開く
    /// </summary>
    public static void OpenMenu()
    {
        ReverseMenuFlag(OPEN); //メニューを開く
    }

    /// <summary>
    /// メニューをセット
    /// </summary>
    private void SetMenu()
    {    
        transform.position = new Vector3(
            mousePos.x + DIFF_X,
            mousePos.y + DIFF_Y,
            transform.position.z
        ); //メニューの位置をマウスの右下に配置
        ReverseMenuFlag(OPEN); //メニューを閉じる
    }

    /// <summary>
    /// メニューをリムーブ
    /// </summary>
    private void RemoveMenu()
    {
        transform.position = INIT_MENU_POS; //初期位置（画面外）
        ReverseMenuFlag(BACK); //メニューを閉じる
    }

    /// <summary>
    /// メニューフラグを反転させる
    /// </summary>
    /// <param name="type">メニューの種類</param>
    public static void ReverseMenuFlag(MENU_TYPE type)
    {
        isMenuFlag[(int)type] = !isMenuFlag[(int)type] ? true : false;
    }
}
