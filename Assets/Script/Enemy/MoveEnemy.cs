using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
using static Call.ConstantValue;
using static Call.CommonFunction;
using static RAND.CreateRandom;

public class MoveEnemy : MonoBehaviour
{
    private float speed; //速度
    public bool isStart;

    private const float INIT_ACCELE = 0.8f; //加速度の初期値
    private const int MIN_RAND = 10; //乱数最小値
    private const int MAX_RAND = 50; //乱数最大値

    private ParticleSystem[] ps = new ParticleSystem[3];

    // Start is called before the first frame update
    void Start()
    {
        speed = GetMoveSpeed(); //移動速度を取得
        ps = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < ps.Length; ++i) ps[i].Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isStart) return;
        if (transform.position.z <= TARGET_POS) return;

        transform.position += new Vector3(0, 0, speed); //移動する
    }

    /// <summary>
    /// 移動速度を取得する
    /// </summary>
    /// <returns></returns>
    private float GetMoveSpeed()
    {
        //乱数値と加速度と速度で計算
        return (Integerization(rand) % MAX_RAND) * INIT_ACCELE * MOVE_SPEED;
    }

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;

    }
}
