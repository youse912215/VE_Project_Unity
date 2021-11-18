using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
using static Call.ConstantValue;

public class MoveEnemy : MonoBehaviour
{
    private float speed; //速度

    private const float INIT_ACCELE = 0.1f; //加速度の初期値
    private const int MIN_RAND = 10; //乱数最小値
    private const int MAX_RAND = 50; //乱数最大値

    // Start is called before the first frame update
    void Start()
    {
        speed = GetMoveSpeed(); //移動速度を取得
    }

    // Update is called once per frame
    void Update()
    {
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
        float r = (float)Random.Range(MIN_RAND, MAX_RAND) * INIT_ACCELE * MOVE_SPEED;
        return r;
    }
}
