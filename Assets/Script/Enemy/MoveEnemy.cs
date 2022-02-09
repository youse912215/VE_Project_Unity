using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;
using static Call.ConstantValue;
using static Call.CommonFunction;
using static RAND.CreateRandom;
using static EnemyCollision;
using static CanvasManager;

public class MoveEnemy : MonoBehaviour
{
    private float speed; //速度
    public bool isStart;
    public int startPos;

    private const float INIT_ACCELE = 0.2f; //加速度の初期値
    private const float MIN_SPEED = 5.0f; //移動速度最小値
    private const float MAX_RAND = 40.0f; //乱数最大値

    private ParticleSystem[] ps = new ParticleSystem[3];
    private Vector3 movement;

    private const float COOL_DOWN = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = GetMoveSpeed(); //移動速度を取得
        InitTransform();
        ps = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < ps.Length; ++i) ps[i].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //キャンバスモードがTowerDefense以外なら、処理をスキップ
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        if(!isStart) return;
        if (transform.position.z <= TARGET_POS) return;

        transform.position += (movement);
    }

    /// <summary>
    /// 移動速度を取得する
    /// </summary>
    /// <returns></returns>
    private float GetMoveSpeed()
    {
        //乱数値と加速度と速度で計算
        return (Integerization(rand) % MAX_RAND + MIN_SPEED) * INIT_ACCELE * MOVE_SPEED;
    }

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;

    }

    void InitTransform()
    {
        if (startPos == 0)
        {
            movement = new Vector3(0, 0, speed);
        }
        else
        {
            movement = new Vector3(speed * 0.6f * startPos, 0, speed * 0.6f);
            this.gameObject.transform.rotation =
                Quaternion.Euler(new Vector3(0.0f, QUARTER_CIRCLE * startPos, 0.0f));
        }
    }
}
