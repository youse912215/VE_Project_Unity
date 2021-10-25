using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class ConstantValue : MonoBehaviour
    {
        //マウスとの差分座標
        public const float DIFF_X = 50.0f;
        public const float DIFF_Y = -75.0f;

        //カメラ
        public const float CAM_DISTANCE = 4500.0f;

        //メニューUI
        public static readonly Vector3 INIT_MENU_POS = new Vector3(-9999.0f, -9999.0f, 0.0f);

        //メニュータイプ
        public enum MENU_TYPE : int
        {
            OPEN,
            BACK,
        }
    }
}


