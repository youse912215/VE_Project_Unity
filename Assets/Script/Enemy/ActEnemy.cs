using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
public class ActEnemy : MonoBehaviour
{
    private GameObject ePrefab;

    private float spawnTime;
    private const int SPAWN_RAND = 5;

    private WarriorParents[] eParents = new WarriorParents[E_CATEGORY];
    private WarriorChildren[,] eChildren = new WarriorChildren[E_CATEGORY, ALL_ENEMEY_MAX * E_CATEGORY];
    public GameObject[,] eObject = new GameObject[E_CATEGORY, ALL_ENEMEY_MAX * E_CATEGORY];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < E_CATEGORY; ++i)
            InitTransform(eParents, eChildren, i); //�G�̏�����
        Random.InitState(100);
        spawnTime = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime++; //�o�����Ԍo��

        //Debug.Log(CulcEnemyCount(eParents));

        if (spawnTime < SPAWN_INTERVAL) return;
        SpawnEnemy();
    }

    private void CreateEnemy(int n)
    {
        //���v��������ALL_ENEMEY_MAX * E_CATEGORY�ȏ�Ȃ�A�X�L�b�v����������
        if (CulcEnemyCount(eParents) >= ALL_ENEMEY_MAX * E_CATEGORY) return;

        eChildren[n, eParents[n].survivalCount].isActivity = true; //������Ԃ�true     
        ePrefab = GameObject.Find(EnemyHeadName + n.ToString()); //�v���t�@�u���擾
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //�N���[���𐶐�
        SPAWN_POS.x = (float)Random.Range(-400, 600);
        eObject[n, eParents[n].survivalCount].transform.position = SPAWN_POS; //�X�|�[���ʒu���擾
        eObject[n, eParents[n].survivalCount].SetActive(true); //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ���

        MoveEnemy mE;
        mE = eObject[n, eParents[n].survivalCount].GetComponent<MoveEnemy>();
        mE.isStart = true;
        Debug.Log(mE.isStart);

        eParents[n].survivalCount++; //�Ō�Ɉ�̒ǉ�
    }

    private int GetSpawnRandom()
    {
        int r = Random.Range(0, SPAWN_RAND);
        r = r == SPAWN_RAND - 1 ? 1 : 0;
        return r;
    }

    private void SpawnEnemy()
    {
        CreateEnemy(GetSpawnRandom());
        spawnTime = 0.0f;
    }
}
