using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static WarriorData;
using static RAND.CreateRandom;

public class ActEnemy : MonoBehaviour
{
    private GameObject ePrefab;

    private float spawnTime;
    private const int SPAWN_RAND = 5;

    private WarriorParents[] eParents = new WarriorParents[E_CATEGORY];
    private WarriorChildren[,] eChildren = new WarriorChildren[E_CATEGORY, ALL_ENEMEY_MAX * E_CATEGORY];
    public GameObject[,] eObject = new GameObject[E_CATEGORY, ALL_ENEMEY_MAX * E_CATEGORY];

    private int type;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < E_CATEGORY; ++i)
            InitTransform(eParents, eChildren, i); //敵の初期化
        spawnTime = 0;
        
        type = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime++; //出現時間経過
        if (spawnTime < SPAWN_INTERVAL) return; //出現間隔未満なら、処理をスキップ
        SpawnEnemy(); //敵を出現させる
    }

    private void CreateEnemy(int n)
    {
        //合計生存数がALL_ENEMEY_MAX * E_CATEGORY以上なら、スキップを処理する
        if (CulcEnemyCount(eParents) >= ALL_ENEMEY_MAX * E_CATEGORY) return;

        eChildren[n, eParents[n].survivalCount].isActivity = true; //生存状態をtrue     
        ePrefab = GameObject.Find(ENEMY_HEAD_NAME + n.ToString()); //プレファブを取得
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //クローンを生成

        var mE = eObject[n, eParents[n].survivalCount].GetComponent<MoveEnemy>();

        mE.startPos = GetPattern(); //出現パターンを取得
        eObject[n, eParents[n].survivalCount].transform.position = GetSpawnPos(mE.startPos); //スポーン位置を取得
        eObject[n, eParents[n].survivalCount].SetActive(true); //ゲームオブジェクトをアクティブにする
        mE.isStart = true;
        eParents[n].survivalCount++; //最後に一体追加
    }

    /// <summary>
    /// 敵0か1をランダムで生成
    /// </summary>
    /// <returns></returns>
    private int GetSpawnRandom()
    {
        type = rand % SPAWN_RAND;
        type = (int)Integerization(type);
        type = (type == 0) ? 1 : 0;
        return type;
    }

    /// <summary>
    /// 敵を出現させる
    /// </summary>
    private void SpawnEnemy()
    {
        CreateEnemy(GetSpawnRandom());
        spawnTime = 0.0f;
    }

    /// <summary>
    /// 出現位置のパターンを取得
    /// </summary>
    /// <returns></returns>
    private int GetPattern()
    {
        var pattarn = 0;

        switch(Integerization(rand) % 3.0f)
        {
            case 0.0f: pattarn = -1;
                break;
            case 1.0f: pattarn = 0;
                break;
            case 2.0f: pattarn = 1;
                break;
            default: break;
        }

        return pattarn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sPos">出現開始位置</param>
    /// <returns></returns>
    private Vector3 GetSpawnPos(int sPos)
    {
        SPAWN_POS.x = rand / 2.0f + 4000.0f * sPos; //-500 ~ 500の範囲
        
        switch (sPos)
        {
            case -1:
            case 1: SPAWN_POS.z = SPAWN_B;
                break;
            case 0: SPAWN_POS.z = SPAWN_A;
                break;
        }
        return SPAWN_POS;
    }
}
