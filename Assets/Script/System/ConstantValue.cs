using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class ConstantValue : MonoBehaviour
    {
        public static readonly Vector2 WINDOW_SIZE = new Vector2(1920, 1080); 

        public const float ONE_CIRCLE = 180.0f;
        public const float HALF_CIRCLE = ONE_CIRCLE / 2.0f;
        public const float QUARTER_CIRCLE = HALF_CIRCLE / 2.0f;

        public const float TARGET_POS = 400.0f;

        public static readonly Vector3 SELECT_UI_POS = new Vector3(-560, 250, 0); //選択UI
        public const float BUTTON_HEIGHT = 120.0f;

        public const int SET_LIST_COUNT = 3; //設置リスト数

        //炎
        public static readonly Vector3 FIRE_POS = new Vector3(154.0f, -2431.0f, 234.0f);
        public static readonly Vector3 FIRE_ROT = new Vector3(-90.0f, 0.0f, 0.0f);

        //マウスとの差分座標
        public const float DIFF_X = 100.0f;
        public const float DIFF_Y = -75.0f;

        //カメラ
        public const float CAM_DISTANCE = 3450.0f;
        /* 真上 */
        public static readonly Vector3 CAM_POS = new Vector3(232, 670, 1260); //位置
        public static Vector3 CAM_ROT = new Vector3(90.0f, 0.0f, 0.0f); //角度
        /* プレイヤー視点 */
        public static readonly Vector3 CAM_P_POS = new Vector3(167.0f ,-2106.0f, -109.0f); //位置
        public static Vector3 CAM_P_ROT = new Vector3(30.5f, 0.0f, 0.0f); //角度

        //メニューUI
        public static readonly Vector3 INIT_MENU_POS = new Vector3(-9999.0f, -9999.0f, 0.0f);
        public static readonly Vector3 BACK_SET_POS = new Vector3(0.0f, 0.0f, 0.0f);
        public static readonly Vector3 BACK_AFTER_POS = new Vector3(0.0f, -30.0f, 0.0f);

        //ウイルスUI
        public static readonly Vector3 NON_ACTIVE_POS = new Vector3(4000.0f, -350.0f, 0.0f);
        public static readonly Vector3 ACTIVE_POS = new Vector3(2140.0f, -390.0f, 0.0f);

        //メニュータイプ
        public enum MENU_TYPE : int
        {
            SET,
            DELETE,
            MOVE,
            DETAIL,
            BACK,
        }
    }
}


