using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
using static Call.ConstantValue;

public class MoveEnemy : MonoBehaviour
{
    private float speed; //���x
    public bool isStart;

    private const float INIT_ACCELE = 0.1f; //�����x�̏����l
    private const int MIN_RAND = 10; //�����ŏ��l
    private const int MAX_RAND = 50; //�����ő�l

    private ParticleSystem[] ps = new ParticleSystem[3];

    // Start is called before the first frame update
    void Start()
    {
        speed = GetMoveSpeed(); //�ړ����x���擾
        ps = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < ps.Length; ++i) ps[i].Stop();
        //isStart = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isStart) return;
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

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;

    }
}
