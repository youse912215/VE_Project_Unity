using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class ConstantValue : MonoBehaviour
    {
        public const float HALF_CIRCLE = 90.0f;
        public const float QUARTER_CIRCLE = HALF_CIRCLE / 2.0f;

        //�}�E�X�Ƃ̍������W
        public const float DIFF_X = 100.0f;
        public const float DIFF_Y = -75.0f;

        //�J����
        public const float CAM_DISTANCE = 4850.0f;
        /* �^�� */
        public static readonly Vector3 CAM_POS = new Vector3(300.0f, 2050.0f, 2000.0f); //�ʒu
        public static Vector3 CAM_ROT = new Vector3(90.0f, 0.0f, 0.0f); //�p�x
        /* �v���C���[���_ */
        public static readonly Vector3 CAM_P_POS = new Vector3(167.0f ,-2106.0f, -20.0f); //�ʒu
        public static Vector3 CAM_P_ROT = new Vector3(30.0f, 0.0f, 0.0f); //�p�x

        //���j���[UI
        public static readonly Vector3 INIT_MENU_POS = new Vector3(-9999.0f, -9999.0f, 0.0f);
        public static readonly Vector3 BACK_SET_POS = new Vector3(0.0f, 0.0f, 0.0f);
        public static readonly Vector3 BACK_AFTER_POS = new Vector3(0.0f, -30.0f, 0.0f);

        //�E�C���XUI
        public static readonly Vector3 NON_ACTIVE_POS = new Vector3(0.0f, -100.0f, 0.0f);
        public static readonly Vector3 ACTIVE_POS = new Vector3(0.0f, 0.0f, 0.0f);

        //���j���[�^�C�v
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


