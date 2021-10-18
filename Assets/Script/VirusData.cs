/* ウイルスデータ */
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
            public bool isActivity; //生存状態
        }

        //ウイルス番号
        public enum VIRUS_NUM
        {
            CODE_CLD,
            CODE_INF,
            CODE_19,
        }

        //位置
        public static readonly Vector3[] pos =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        };

        //位置
        public static readonly Quaternion[] rot =
        {
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
        };

        //スケール
        public static readonly Vector3[] scl =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        }; 

        //3力
        public static readonly Vector3[] force =
            {
            new Vector3(1, 1, 1), //code.cld
            new Vector3(3, 2, 1), //code.inf
            new Vector3(2, 3, 3), //code.19

            };

        public const int CATEGORY = 3;
        public const int OWNED = 5;

        public static int[] owned = new int[CATEGORY];

        /// <summary>
        /// ウイルス番号に対応する、値をセットする
        /// </summary>
        /// <param name="v">ウイルス型の変数</param>
        /// <param name="n">ウイルス名</param>
        public static void InitValue(Virus[,] v, VIRUS_NUM n)
        {
            int i = (int)n; //番号取得
            owned[i] = 0; //保有数取得

            for (int a = 0; a < OWNED; a++) v[i, a].pos = pos[i]; //位置取得
            for (int a = 0; a < OWNED; a++) v[i, a].rot = rot[i]; //角度取得
            for (int a = 0; a < OWNED; a++) v[i, a].scl = scl[i]; //スケール取得
            for (int a = 0; a < OWNED; a++) v[i, a].force = force[i]; //3力取得
            for (int a = 0; a < OWNED; a++) v[i, a].isActivity = false; //生存状態取得
        }
        
        /// <summary>
        /// ウイルスをアクティブにする
        /// </summary>
        /// <param name="v">ウイルス型の変数</param>
        /// <param name="n">ウイルス名</param>
        public static void GetVirus(VIRUS_NUM n)
        {
            int i = (int)n;
            if (owned[i] != 0) owned[i]++;
        }

        public static void ReleaseVirus(VIRUS_NUM n)
        {
            int i = (int)n;
            if (owned[i] != 0) owned[i]--;
        }

        public static void SetVirus(Virus[,] v, VIRUS_NUM n, GameObject[,] obj)
        {
            int i = (int)n; //ウイルス番号
            int j = owned[i]; //ウイルス内識別番号
            v[i, j].isActivity = true;
        }

        public static void SaveVirusPosition(Virus[,] v, VIRUS_NUM n, GameObject[,] obj, Vector3 wPos)
        {
            int i = (int)n; //ウイルス番号
            int j = owned[i]; //ウイルス内識別番号
            obj[i, j].transform.position = wPos; //現在のマウスのワールド座標を保存
            v[i, j].isActivity = false;
        }

        public static bool ProcessOutOfRange(VIRUS_NUM n)
        {
            int i = (int)n;
            if (owned[i] >= OWNED - 1) return true;
            return false;
        }

        //public static int AllocationNumber(VIRUS_NUM n)
        //{
        //    int i = (int)n;
        //    return OWNED - owned[i] - 1;
        //}
    }
}