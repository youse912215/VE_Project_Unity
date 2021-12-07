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
        ePrefab = GameObject.Find(EnemyHeadName + n.ToString()); //プレファブを取得
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //クローンを生成
        SPAWN_POS.x = rand / 2.0f; //-500 ~ 500の範囲
        eObject[n, eParents[n].survivalCount].transform.position = SPAWN_POS; //スポーン位置を取得
        eObject[n, eParents[n].survivalCount].SetActive(true); //ゲームオブジェクトをアクティブにする

        var mE = eObject[n, eParents[n].survivalCount].GetComponent<MoveEnemy>();
        mE.isStart = true;
        eParents[n].survivalCount++; //最後に一体追加
    }

    private int GetSpawnRandom()
    {
        type = rand % SPAWN_RAND;
        type = (int)Integerization(type);
        type = (type == 0) ? 1 : 0;
        return type;
    }

    private void SpawnEnemy()
    {
        CreateEnemy(GetSpawnRandom());
        spawnTime = 0.0f;
    }
}
