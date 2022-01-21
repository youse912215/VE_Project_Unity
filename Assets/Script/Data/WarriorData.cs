using UnityEngine;

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

        public float ad; //物理攻撃力
        public bool isActivity; //生存状態
    }

    public static readonly float[] adList = {5.0f, 10.0f, 15.0f};
    public static readonly string[] EnemyTags = {"meleeEnemy", "protectiveEnemy", "rangedEnemy"};
    public static readonly Vector3 E_SIZE = new Vector3(10.0f, 10.0f, 10.0f);
    public static Vector3 SPAWN_POS = new Vector3(40.0f, -2800.0f, 6000.0f);
    public static int deadCount; //死亡数

    public const int ALL_ENEMEY_MAX = 10; //最大数
    public const int E_CATEGORY = 2; //カテゴリー数
    public const string ENEMY_HEAD_NAME = "Enemy"; //敵の名前
    public const float SPAWN_INTERVAL = 10.0f; //出現間隔
    public const float MOVE_SPEED = -0.5f; //移動速度
    public const float SPAWN_A = 6000.0f; //出現地点A
    public const float SPAWN_B = 4000.0f; //出現地点B

    /// <summary>
    /// 敵の数を計算する
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
    /// 敵の初期化
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
