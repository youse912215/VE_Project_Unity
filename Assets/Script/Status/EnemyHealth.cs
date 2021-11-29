using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.VirusData;
using static WarriorData;

public class EnemyHealth : MonoBehaviour
{
    /* private */ 
    //�ő�HP�ƌ��݂�HP�B
    private float maxHp = 2000;
    private float currentHp;
    
    [SerializeField] private Slider slider; //�X���C�_�[
    [SerializeField] private ParticleSystem impactPs; //�Ռ��g�̃p�[�e�B�N���V�X�e��
    [SerializeField] private ParticleSystem bloodPs; //�����Ԃ��̃p�[�e�B�N���V�X�e��
    private ParticleSystem impactEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem bloodEffect; //�Ռ��g�̃G�t�F�N�g

    /* public */ 
    public uint takenDamage; //��_���[�W
    public float totalDamage; //���v�_���[�W
    public bool isInfection; //�����������ǂ���
    public bool isDead; //���񂾂��ǂ���

    void Start()
    {   
        slider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
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
    /// �̗͂��v�Z����
    /// </summary>
    /// <param name="damage">�_���[�W</param>
    /// <returns></returns>
    public int CulculationHealth(uint damage)
    {
        uint d0 = ((damage & 0b0001) >> 0) * (uint)force[0].x; //
        uint d1 = ((damage & 0b0010) >> 1) * (uint)force[1].x; //
        uint d2 = ((damage & 0b0100) >> 2) * (uint)force[2].x; //
        return (int)(d0 + d1 + d2);
    }
}