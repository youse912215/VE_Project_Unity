using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
using static Call.ConstantValue;

public class MoveEnemy : MonoBehaviour
{
    private float speed; //���x

    private const float INIT_ACCELE = 0.1f; //�����x�̏����l
    private const int MIN_RAND = 10; //�����ŏ��l
    private const int MAX_RAND = 50; //�����ő�l

    // Start is called before the first frame update
    void Start()
    {
        speed = GetMoveSpeed(); //�ړ����x���擾
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= TARGET_POS) return;

        transform.position += new Vector3(0, 0, speed); //�ړ�����
    }

    /// <summary>
    /// �ړ����x���擾����
    /// </summary>
    /// <returns></returns>
    private float GetMoveSpeed()
    {
        //�����l�Ɖ����x�Ƒ��x�Ōv�Z
        float r = (float)Random.Range(MIN_RAND, MAX_RAND) * INIT_ACCELE * MOVE_SPEED;
        return r;
    }
}
