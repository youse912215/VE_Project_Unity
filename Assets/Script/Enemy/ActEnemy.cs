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
        ePrefab = GameObject.Find(ENEMY_HEAD_NAME + n.ToString()); //�v���t�@�u���擾
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //�N���[���𐶐�

        var mE = eObject[n, eParents[n].survivalCount].GetComponent<MoveEnemy>();

        mE.startPos = GetPattern(); //�o���p�^�[�����擾
        eObject[n, eParents[n].survivalCount].transform.position = GetSpawnPos(mE.startPos); //�X�|�[���ʒu���擾
        eObject[n, eParents[n].survivalCount].SetActive(true); //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ���
        mE.isStart = true;
        eParents[n].survivalCount++; //�Ō�Ɉ�̒ǉ�
    }

    /// <summary>
    /// �G0��1�������_���Ő���
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
    /// �G���o��������
    /// </summary>
    private void SpawnEnemy()
    {
        CreateEnemy(GetSpawnRandom());
        spawnTime = 0.0f;
    }

    /// <summary>
    /// �o���ʒu�̃p�^�[�����擾
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
    /// <param name="sPos">�o���J�n�ʒu</param>
    /// <returns></returns>
    private Vector3 GetSpawnPos(int sPos)
    {
        SPAWN_POS.x = rand / 2.0f + 4000.0f * sPos; //-500 ~ 500�͈̔�
        
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
