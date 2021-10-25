using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;

public class ShowMenu : MonoBehaviour
{
    public static Vector3 mousePos; //マウス座標
    public static Camera cam; //カメラオブジェクト

    public static bool isFeelVirus;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //メインカメラ取得
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            mousePos.x + DIFF_X,
            mousePos.y + DIFF_Y,
            transform.position.z
        ); //メニューの位置をマウスの右下に配置
    }

    /// <summary>
    /// カメラの位置を取得
    /// </summary>
    public static void GetCameraPos()
    {
        mousePos = Input.mousePosition; //マウスの座標を取得
        mousePos.z = CAM_DISTANCE; //マウスz座標にカメラとの距離を代入
    }

    /// <summary>
    /// スクリーン上のマウス座標を返す
    /// </summary>
    public static Vector3 ReturnOnScreenMousePos()
    {
        GetCameraPos(); //カメラ位置取得
        return cam.ScreenToWorldPoint(mousePos); //スクリーン→ワールド変換
    }
}
