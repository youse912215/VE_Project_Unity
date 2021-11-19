using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.VirusData;
using static WarriorData;

public class EnemyHealth : MonoBehaviour
{
    //最大HPと現在のHP。
    private float maxHp = 2000;
    public float currentHp;
    //Sliderを入れる
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private ParticleSystem ps;

    private ParticleSystem effect;

    public bool isInfection;
    public uint damage;
    public float total;
    public bool isDead;

    void Start()
    {   
        slider.value = 1; //Sliderを満タン
        currentHp = maxHp; //現在のHPに最大HPを代入
        damage = 0b0000;
        total = 0.0f;
        effect = Instantiate(ps);
        effect.Stop();
        
    }

    void Update()
    {
        if (transform.position.z <= TARGET_POS)
        {
            ColonyHealth.currentHp -= 0.5f;
            effect.transform.position = new Vector3(transform.position.x, transform.position.y + 250.0f, transform.position.z - 170.0f);
            effect.Play();
            //GetComponent<AudioSource>().Play();
        }

        if (!isInfection) return;

        currentHp -= total;
        slider.value = currentHp / maxHp;

        if (currentHp > 0.0f) return;
        deadCount++;
        effect.Stop();
        Destroy(gameObject);
    }

    /// <summary>
    /// 体力を計算する
    /// </summary>
    /// <param name="damage">ダメージ</param>
    /// <returns></returns>
    public int CulculationHealth(uint damage)
    {
        uint d0 = ((damage & 0b0001) >> 0) * (uint)force[0].x; //
        uint d1 = ((damage & 0b0010) >> 1) * (uint)force[1].x; //
        uint d2 = ((damage & 0b0100) >> 2) * (uint)force[2].x; //
        return (int)(d0 + d1 + d2);
    }
}