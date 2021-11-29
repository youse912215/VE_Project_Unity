using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.VirusData;
using static WarriorData;

public class EnemyHealth : MonoBehaviour
{
    /* private */ 
    //最大HPと現在のHP。
    private float maxHp = 2000;
    private float currentHp;
    
    [SerializeField] private Slider slider; //スライダー
    [SerializeField] private ParticleSystem impactPs; //衝撃波のパーティクルシステム
    [SerializeField] private ParticleSystem bloodPs; //血しぶきのパーティクルシステム
    private ParticleSystem impactEffect; //衝撃波のエフェクト
    private ParticleSystem bloodEffect; //衝撃波のエフェクト

    /* public */ 
    public uint takenDamage; //被ダメージ
    public float totalDamage; //合計ダメージ
    public bool isInfection; //感染したかどうか
    public bool isDead; //死んだかどうか

    void Start()
    {   
        slider.value = 1; //Sliderを満タン
        currentHp = maxHp; //現在のHPに最大HPを代入
        takenDamage = 0b0000;
        totalDamage = 0.0f;
        impactEffect = Instantiate(impactPs);
        impactEffect.Stop();
        bloodEffect = Instantiate(bloodPs);
        bloodEffect.Stop();
    }

    void Update()
    {
        if (transform.position.z <= TARGET_POS)
        {
            ColonyHealth.currentHp -= 0.5f;
            impactEffect.transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 250.0f,
                transform.position.z - 170.0f);
            impactEffect.Play();
            //GetComponent<AudioSource>().Play();
        }

        if (!isInfection) return;

        currentHp -= totalDamage;
        slider.value = currentHp / maxHp;

        if (currentHp > 0.0f) return;
        deadCount++;
        impactEffect.Stop();
        bloodEffect.transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 250.0f,
                transform.position.z);
        bloodEffect.Play();
        
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