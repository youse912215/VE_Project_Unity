using UnityEngine;
using System.Collections.Generic;

public class WarriorData : MonoBehaviour
{
    public struct WarriorParents
    {
        public GameObject[] obj;
        public string tag;
        public int survivalCount;
    }

    public struct WarriorChildren
    {
        public Vector3 pos;
        public Vector3 rot;
        public Vector3 scl;

        public float ad; //•¨—UŒ‚—Í
        public bool isActivity; //¶‘¶ó‘Ô
    }

    public static readonly float[] adList = {5.0f, 10.0f, 15.0f};
    public static readonly string[] EnemyTags = {"meleeEnemy", "protectiveEnemy", "rangedEnemy"};
    public static readonly Vector3 E_SIZE = new Vector3(10.0f, 10.0f, 10.0f);
    public static Vector3 SPAWN_POS = new Vector3(40.0f, -2800.0f, 6000.0f);
    public static int deadCount; //€–S”

    public const int ALL_ENEMEY_MAX = 50; //Å‘å”
    public const int E_CATEGORY = 2; //ƒJƒeƒSƒŠ[”
    public const string ENEMY_HEAD_NAME = "Enemy"; //“G‚Ì–¼‘O
    public const float SPAWN_INTERVAL = 10.0f; //oŒ»ŠÔŠu
    public const float MOVE_SPEED = -0.5f; //ˆÚ“®‘¬“x
    public const float SPAWN_A = 6000.0f; //oŒ»’n“_A
    public const float SPAWN_B = 4000.0f; //oŒ»’n“_B

    //“G0
    public static readonly List<float> PENETRATION_DEFENCE_LIST0 =
        new List<float> {
            0.0f, 0.25f, 0.50f, 0.75f, 1.0f,
            1.5f, 2.0f, 2.5f, 3.0f, 3.5f
        }; //ŠÑ’Ê–hŒäƒŠƒXƒg0
    //“G1
    public static readonly List<float> PENETRATION_DEFENCE_LIST1 =
        new List<float> {
            1.25f, 1.50f, 1.75f, 2.00f, 2.25f,
            2.50f, 3.0f, 3.75f, 4.5f, 5.0f
        }; //ŠÑ’Ê–hŒäƒŠƒXƒg1

    public static readonly List<int> ENEMY_COUNTS_PER_WAVE =
        new List<int> {
            10, 15, 20, 25, 30, 40, 50  
        };

    /// <summary>
    /// “G‚Ì”‚ğŒvZ‚·‚é
    /// </summary>
    /// <param name="wP"></param>
    /// <returns></returns>
    public static int CulcEnemyCount(WarriorParents[] wP)
    {
        int total = 0;
        for (int i = 0; i < E_CATEGORY; ++i)
            total += wP[i].survivalCount;
        return total;
    }

    /// <summary>
    /// “G‚Ì‰Šú‰»
    /// </summary>
    /// <param name="wP"></param>
    /// <param name="wC"></param>
    /// <param name="n"></param>
    public static void InitTransform(WarriorParents[] wP, WarriorChildren[,] wC, int n)
    {
        for (int i = 0; i < n; ++i)
        {
            //wP[n].tag = EnemyTags[n];
            wP[n].survivalCount = 0;
        }

        for (int i = 0; i < ALL_ENEMEY_MAX * E_CATEGORY; ++i)
        {
            wC[n, i].pos = Vector3.zero;
            wC[n, i].rot = Vector3.zero;
            wC[n, i].scl = Vector3.zero;
            wC[n, i].ad = adList[n];
            wC[n, i].isActivity = false;
        }
    }
}
