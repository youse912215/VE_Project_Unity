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
            InitTransform(eParents, eChildren, i); //�G�̏�����

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
        //���v��������ALL_ENEMEY_MAX * E_CATEGORY�ȏ�Ȃ�A�X�L�b�v����������
        if (CulcEnemyCount(eParents) >= ALL_ENEMEY_MAX * E_CATEGORY) return;

        eChildren[n, eParents[n].survivalCount].isActivity = true; //������Ԃ�true     
        ePrefab = GameObject.Find(EnemyHeadName + n.ToString()); //�v���t�@�u���擾
        eObject[n, eParents[n].survivalCount] = Instantiate(ePrefab); //�N���[���𐶐�
        rand = (float)Random.Range(-400, 600);
        SPAWN_POS.x = rand;
        eObject[n, eParents[n].survivalCount].transform.position = SPAWN_POS; //�X�|�[���ʒu���擾
        //eObject[n, eParents[n].survivalCount].transform.localScale = E_SIZE; //�T�C�Y���擾
        eObject[n, eParents[n].survivalCount].SetActive(true); //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ���
        eParents[n].survivalCount++; //�Ō�Ɉ�̒ǉ�
    }
}
