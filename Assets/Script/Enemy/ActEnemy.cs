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
            InitTransform(eParents, eChildren, i); //�G�̏�����
        spawnTime = 0;
        
        type = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime++; //�o�����Ԍo��
        if (spawnTime < SPAWN_INTERVAL) return; //�o���Ԋu�����Ȃ�A�������X�L�b�v
        SpawnEnemy(); //�G���o��������
    }

    private void CreateEnemy(int n)
    {
        //���v��������ALL_ENEMEY_MAX * E_CATEGORY�ȏ�Ȃ�A�X�L�b�v����������
        if (CulcEnemyCount(eParents) >= ALL_ENEMEY_MAX * E_CATEGORY) return;

        eChildren[n, eParents[n].survivalCount].isActivity = true; //������Ԃ�true     
        ePrefab = GameObject.Find(EnemyHeadName + n.ToString()); //�v���t�@�u���擾
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //�N���[���𐶐�
        SPAWN_POS.x = rand / 2.0f; //-500 ~ 500�͈̔�
        eObject[n, eParents[n].survivalCount].transform.position = SPAWN_POS; //�X�|�[���ʒu���擾
        eObject[n, eParents[n].survivalCount].SetActive(true); //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ���

        var mE = eObject[n, eParents[n].survivalCount].GetComponent<MoveEnemy>();
        mE.isStart = true;
        eParents[n].survivalCount++; //�Ō�Ɉ�̒ǉ�
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
