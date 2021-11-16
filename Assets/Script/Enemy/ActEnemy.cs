using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
public class ActEnemy : MonoBehaviour
{
    private GameObject ePrefab;

    float rand;

    private WarriorParents[] eParents = new WarriorParents[E_CATEGORY];
    private WarriorChildren[,] eChildren = new WarriorChildren[E_CATEGORY, ALL_ENEMEY_MAX];
    private GameObject[,] eObject = new GameObject[E_CATEGORY, ALL_ENEMEY_MAX];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < E_CATEGORY; ++i)
            InitTransform(eParents, eChildren, i); //敵の初期化

        rand = 0;
        Random.InitState(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) SpawnEnemy(0);
    }

    private void SpawnEnemy(int n)
    {
        //合計生存数がALL_ENEMEY_MAX * E_CATEGORY以上なら、スキップを処理する
        if (CulcEnemyCount(eParents) >= ALL_ENEMEY_MAX * E_CATEGORY) return;

        eChildren[n, eParents[n].survivalCount].isActivity = true; //生存状態をtrue     
        ePrefab = GameObject.Find(EnemyHeadName + n.ToString()); //プレファブを取得
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //クローンを生成
        rand = (float)Random.Range(-400, 600);
        SPAWN_POS.x = rand;
        eObject[n, eParents[n].survivalCount].transform.position = SPAWN_POS; //スポーン位置を取得
        //eObject[n, eParents[n].survivalCount].transform.localScale = E_SIZE; //サイズを取得
        eObject[n, eParents[n].survivalCount].SetActive(true); //ゲームオブジェクトをアクティブにする
        eParents[n].survivalCount++; //最後に一体追加
    }
}
