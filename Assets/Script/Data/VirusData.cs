/* ウイルスデータ */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class VirusData : MonoBehaviour
    {
        //親ウイルス構造体
        public struct VirusParents
        {
            public GameObject[] obj; //親オブジェクト
            public string tag; //タグ名
            public int setCount; //現在設置した個数
            public int creationCount; //作成した個数
        }

        //子ウイルス構造体
        public struct VirusChildren
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
            CODE_NOV,
            CODE_EHF,
            CODE_EV,
            CODE_BD,
            CODE_ULT,
            CODE_NONE,
        }

        //位置
        public static readonly Vector3[] pos =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
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
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
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
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        }; 

        //3力
        public static readonly Vector3[] force =
        {
            new Vector3(1, 1.0f, 1), //code.cld
            new Vector3(3, 2, 1), //code.inf
            new Vector3(2, 3, 3), //code.19
            new Vector3(1, 3, 1), //code.cld
            new Vector3(3, 1.25f, 1), //code.inf
            new Vector3(2, 1.5f, 3), //code.19
            new Vector3(5, 2, 1), //code.cld
            new Vector3(5, 5, 5), //code.ult

        };

        public static readonly Color[] COLOR_SET =
        {
            new Color(84, 7, 84),
            new Color(0, 7, 84),
            new Color(0, 120, 84),
            new Color(255,255,255),
            new Color(255,255,255),
            new Color(255,255,255),
            new Color(255,255,255),
            new Color(255,255,255),
        };

        public const int V_CATEGORY = 8; //ウイルスの種類
        public const int OWNED = 5; //ウイルスの最大保有数

        public static bool[] isLimitCapacity = new bool[V_CATEGORY]; //限界容量
        public static string VirusHeadName = "Virus"; //ウイルス頭名
        public static string[] VirusTagName = {"Cold", "Inf", "19", "Nov", "Ehf", "Ev", "Bd", "Ult"};
        public static readonly Vector3 V_SIZE = new Vector3(10.0f, 10.0f, 10.0f);
        public static Vector3 vRange = new Vector3(50.0f, 50.0f, 50.0f);

        public const float CONSTANT_FORCE = 5.0f;

        /// <summary>
        /// ウイルス番号に対応する、値をセットする
        /// </summary>
        /// <param name="vC">ウイルス構造体</param>
        /// <param name="n">ウイルス名</param>
        public static void InitValue(VirusParents[] vP, VirusChildren[,] vC, VIRUS_NUM n)
        {
            int i = (int)n; //番号取得
            vP[i].setCount = 0; //現在数取得

            for (int a = 0; a < OWNED; a++) {
                vC[i, a].pos = pos[i]; //位置取得
                vC[i, a].rot = rot[i]; //角度取得
                vC[i, a].scl = scl[i]; //スケール取得
                vC[i, a].force = force[i]; //3力取得
                vC[i, a].isActivity = false; //生存状態取得
            }
        }
        
        /// <summary>
        /// ウイルスをアクティブにする
        /// </summary>
        /// <param name="n">ウイルス名</param>
        public static void GetVirus(VirusParents[] vP, int n)
        {
            if (vP[n].setCount == vP[n].creationCount) isLimitCapacity[n] = true; //限界容量に到達
        }

        /// <summary>
        /// ウイルスを生成し、生存情報を取得する
        /// </summary>
        /// <param name="vC">ウイルス構造体</param>
        /// <param name="n">ウイルス名</param>
        public static void GenerationVirus(VirusParents[] vP,　VirusChildren[,] vC, int n)
        {
            int j = vP[n].setCount; //ウイルス内識別番号
            vC[n, j].isActivity = true; //生存状態をアクティブ
        }

        /// <summary>
        /// 設置指定したウイルスの座標を保存する
        /// </summary>
        /// <param name="vC">ウイルス構造体配列</param>
        /// <param name="vNum">ウイルス番号</param>
        /// <param name="sNum">設置番号</param>
        /// <param name="obj">ゲームオブジェクト</param>
        /// <param name="wPos">ワールド座標</param>
        public static void SaveVirusPosition(VirusChildren[,] vC, int vNum, int sNum,
            GameObject[,] obj, Vector3 wPos)
        {
            int i = vNum; //ウイルス番号
            int j = sNum; //ウイルス内識別番号
            obj[i, j].transform.position = wPos; //ウイルスオブジェクトに、現在のマウスのワールド座標を保存
            vC[i, j].pos = wPos;  //ウイルス構造体に、現在のマウスのワールド座標を保存
            vC[i, j].isActivity = false; //生存状態をインアクティブ
        }

        /// <summary>
        /// 範囲外処理
        /// </summary>
        /// <param name="n">ウイルス名</param>
        /// <returns>限界容量の状態</returns>
        public static bool ProcessOutOfRange(int n)
        {
            return isLimitCapacity[n];
        }
    }
}