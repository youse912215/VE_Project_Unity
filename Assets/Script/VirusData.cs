using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class VirusData : MonoBehaviour
    {
        public struct Virus
        {
            public Vector3 pos; //位置
            public Quaternion rot; //角度
            public Vector3 scl; //スケール

            public Vector3 force; //3力（殺傷力、感染力、突破力）

            public float durableValue; //耐久値
            public int owned; //保有数 
            public bool isActivity; //生存状態
        }

        public enum VIRUS_NUM
        {
            CODE_CLD,
            CODE_INF,
            CODE_19,
        }

        /* 位置 */
        public static readonly Vector3[] pos =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        };

        /* 位置 */
        public static readonly Quaternion[] rot =
        {
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
        };

        /* スケール */
        public static readonly Vector3[] scl =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        }; 

        /* 3力 */
        public static readonly Vector3[] force =
            {
            new Vector3(1, 1, 1), //code.cld
            new Vector3(3, 2, 1), //code.inf
            new Vector3(2, 3, 3), //code.19

            };

        public const int num = 3;
        private const int owned = 5;

        //ウイルス番号に対応する、値をセットする
        public static void setValue(Virus[] v, VIRUS_NUM n)
        {
            int num = (int)n;
            v[num].pos = pos[num]; //位置取得
            v[num].rot = rot[num]; //角度取得
            v[num].scl = scl[num]; //スケール取得
            v[num].force = force[num]; //3力取得
            v[num].owned = owned;
        }
    }
}
