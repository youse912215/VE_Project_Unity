using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class ConstantValue : MonoBehaviour
    {
        public const float HALF_CIRCLE = 90.0f;
        public const float QUARTER_CIRCLE = HALF_CIRCLE / 2.0f;

        //マウスとの差分座標
        public const float DIFF_X = 100.0f;
        public const float DIFF_Y = -75.0f;

        //カメラ
        public const float CAM_DISTANCE = 4850.0f;
        /* 真上 */
        public static readonly Vector3 CAM_POS = new Vector3(300.0f, 2050.0f, 2000.0f); //位置
        public static Vector3 CAM_ROT = new Vector3(90.0f, 0.0f, 0.0f); //角度
        /* プレイヤー視点 */
        public static readonly Vector3 CAM_P_POS = new Vector3(167.0f ,-2106.0f, -20.0f); //位置
        public static Vector3 CAM_P_ROT = new Vector3(30.0f, 0.0f, 0.0f); //角度

        //メニューUI
        public static readonly Vector3 INIT_MENU_POS = new Vector3(-9999.0f, -9999.0f, 0.0f);
        public static readonly Vector3 BACK_SET_POS = new Vector3(0.0f, 0.0f, 0.0f);
        public static readonly Vector3 BACK_AFTER_POS = new Vector3(0.0f, -30.0f, 0.0f);

        //ウイルスUI
        public static readonly Vector3 NON_ACTIVE_POS = new Vector3(0.0f, -100.0f, 0.0f);
        public static readonly Vector3 ACTIVE_POS = new Vector3(0.0f, 0.0f, 0.0f);

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


