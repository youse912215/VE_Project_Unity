using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.CommonFunction;
using static Call.VirusData;
using static RAND.CreateRandom;
using static VirusMaterialData;
using static WarriorData;
using static PrepareVirus;
using static CameraManager;
using static ColonyHealth;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    /* private */
    [SerializeField] private Slider slider; //�X���C�_�[
    [SerializeField] private ParticleSystem impactPs; //�Ռ��g�̃p�[�e�B�N���V�X�e��
    [SerializeField] private ParticleSystem bloodPs; //�����Ԃ��̃p�[�e�B�N���V�X�e��
    [SerializeField] private ParticleSystem steamPs; //steam�̃p�[�e�B�N���V�X�e��
    [SerializeField] private ParticleSystem jewelPs; //��΂̃p�[�e�B�N���V�X�e��
    [SerializeField] private Material mat;

    private ParticleSystem impactEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem bloodEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem steamEffect; //�X�`�[���̃G�t�F�N�g
    private ParticleSystem jewelEffect; //��΂̃G�t�F�N�g

    private int enemyRank; //�K��
    private const int MAX_RANK = 5; //�ő�K��
    private float currentHp; //���݂�HP
    private float maxHp; //�ő�HP
    private const float INIT_HEALTH = 2.5f; //����HP�Œ�l
    private const float ATTACK_DAMAGE = 1.5f; //�U����
    private bool isImpactSet; //�Ռ��G�t�F�N�g���Z�b�g�������ǂ���
    private const float IMPACT_POS_Z = -170.0f; //�Ռ��G�t�F�N�g��Z���W
    private const float EFFECT_HEIGHT = 250.0f; //�G�t�F�N�g����
    private const int ENEMEY1_LAYER = 7; //�G1�̃��C���ԍ�
    private readonly Vector3 STEAM_POS = new Vector3(-65.0f, 145.0f, -180.0f);
    private int getCount = 0;
    private int getMaterial = 99;
    private const int MAX_DROP = 5; //�ő�h���b�v��
    private List<bool> isVirusDamage = new List<bool> { false, false, false };
    private readonly List<float> PENETRATION_DEFENCE_LIST = new List<float> {0.0f, 1.5f, 3.0f, 4.5f, 6.0f}; //�ђʖh�䃊�X�g
    private float pDefence = 0.0f; //�ђʖh��

    /* public */
    public uint takenDamage; //��_���[�W
    public float totalDamage; //���v�_���[�W
    public bool isInfection; //�����������ǂ���
    public bool isDead; //���񂾂��ǂ���

    private MoveEnemy mE;
    private DamageManager dM;
    private Vector3 newPos;

    /// <summary>
    /// �J�n����
    /// </summary>
    void Start()
    {
        slider.value = 1; //Slider�𖞃^��
        maxHp = Integerization(rand) * INIT_HEALTH; //�ő�HP�������_���Ŏ擾
        currentHp = maxHp; //���݂�HP�ɍő�HP����
        takenDamage = 0b0000;
        totalDamage = 0.0f;
        isImpactSet = false;
        impactEffect = Instantiate(impactPs); //�G�t�F�N�g����
        impactEffect.Stop(); //�G�t�F�N�g��~
        bloodEffect = Instantiate(bloodPs); //�G�t�F�N�g����
        bloodEffect.Stop(); //�G�t�F�N�g��~
        jewelEffect = Instantiate(jewelPs); //�G�t�F�N�g����
        jewelEffect.Stop(); //�G�t�F�N�g��~

        mE = this.gameObject.GetComponent<MoveEnemy>();
        dM = this.gameObject.GetComponent<DamageManager>();

        enemyRank = (int)Integerization(rand) % MAX_RANK; //�K�����擾
        pDefence = PENETRATION_DEFENCE_LIST[enemyRank]; //���X�g����ђʖh����擾

        if (this.gameObject.layer != ENEMEY1_LAYER) return; //�Ώۃ��C���[�ȊO�́A�������X�L�b�v
        steamEffect = Instantiate(steamPs); //�G�t�F�N�g����
        steamEffect.Play(); //�G�t�F�N�g�J�n

        steamEffect.transform.rotation =
                Quaternion.Euler(new Vector3(180.0f, QUARTER_CIRCLE * mE.startPos, 0.0f));
        newPos = new Vector3(-150.0f * mE.startPos, 0, 0);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        UpdateSteamEffect(); //�X�`�[���G�t�F�N�g�X�V
        AttackAction(); //�U�����s��

        if (!isInfection) return; //�������ȊO�́A�������X�L�b�v
        InfectionAction(); //�E�C���X�������s��

        if (currentHp > 0.0f) return; //�����Ă���Ԃ́A�������X�L�b�v
        DropMaterial(); //�f�ނ��h���b�v
        DeadAction(); //���S���s��
    }

    /// <summary>
    /// �̗͂��v�Z����
    /// </summary>
    /// <returns></returns>
    public float CulculationHealth(int type)
    {
        var d = new float[SET_LIST_COUNT];
        //0
        isVirusDamage[virusSetList[0]] = (type == virusSetList[0]) ? true : false;
        d[0] = isVirusDamage[virusSetList[0]] ? FORCE_WEIGHT[colonyLevel] * force[virusSetList[0]].x : 0.0f;
        //1
        isVirusDamage[virusSetList[1]] = (type == virusSetList[1]) ? true : false;
        d[1] = isVirusDamage[virusSetList[1]] ? FORCE_WEIGHT[colonyLevel] * force[virusSetList[1]].x : 0.0f;
        //2
        isVirusDamage[virusSetList[2]] = (type == virusSetList[2]) ? true : false;
        d[2] = isVirusDamage[virusSetList[2]] ? FORCE_WEIGHT[colonyLevel] * force[virusSetList[2]].x : 0.0f;
        return d[0] + d[1] + d[2];
    }

    /// <summary>
    /// �U�����s��
    /// </summary>
    private void AttackAction()
    {
        if (transform.position.z <= TARGET_POS)
        {
            ColonyHealth.currentHp -= Integerization(rand) % ATTACK_DAMAGE; //�R���j�[�ւ̍U��

            if (isImpactSet) return; //�Ռ��g���Z�b�g���Ă���Ȃ�A�������X�L�b�v            
            if (mE.startPos != 0) StartCoroutine("RotationBody");
            SetEffectPos(impactEffect, IMPACT_POS_Z); //�G�t�F�N�g�̈ʒu���Z�b�g
            impactEffect.Play(); //�Ռ��g�G�t�F�N�g
            isImpactSet = true; //�Z�b�g�t���O��true
            //GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// �E�C���X�������s��
    /// </summary>
    private void InfectionAction()
    {
        currentHp -= totalDamage; //�E�C���X�̍��v�_���[�W���AHP�����炷
        slider.value = currentHp / maxHp; //HP�o�[�̌v�Z
    }

    /// <summary>
    /// ���S���s��
    /// </summary>
    private void DeadAction()
    {
        if (!gameObject) return;
        exp += dM.GetExp(enemyRank); //�o���l�擾
        colonyLevel += dM.CulculationColonyLevel(); //�R���j�[���x�����v�Z
        Debug.Log("�o���l::" + exp);
        Debug.Log("���x��::" + colonyLevel);
        deadCount++; //�݌v�̎��S�����J�E���g
        Destroy(impactEffect); //�Ռ��g�G�t�F�N�g���폜
        Destroy(steamEffect); //steam�G�t�F�N�g���폜
        SetEffectPos(bloodEffect); //���G�t�F�N�g�̈ʒu���Z�b�g
        SetEffectPos(jewelEffect); //��΃G�t�F�N�g�̈ʒu���Z�b�g
        bloodEffect.Play(); //���̃G�t�F�N�g����
        jewelEffect.Play(); //��΂̃G�t�F�N�g����
        Destroy(gameObject); //�I�u�W�F�N�g���폜
    }

    /// <summary>
    /// �G�t�F�N�g�̈ʒu���Z�b�g����
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="diffZ"></param>
    private void SetEffectPos(ParticleSystem effect, float diffZ = 0.0f)
    {
        var effectPos = new Vector3(
                transform.position.x,
                transform.position.y + EFFECT_HEIGHT,
                transform.position.z + diffZ);
        effect.transform.position = effectPos;
    }

    /// <summary>
    /// �X�`�[���G�t�F�N�g�X�V
    /// </summary>
    private void UpdateSteamEffect()
    {
        if (this.gameObject.layer != ENEMEY1_LAYER) return; //�Ώۃ��C���[�ȊO�́A�������X�L�b�v
        steamEffect.transform.position = transform.position + STEAM_POS
            + newPos; //�X�V
    }

    /// <summary>
    /// �̂���]����
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotationBody()
    {
        //�������̊ԌJ��Ԃ�
        while ((mE.startPos == -1 && this.gameObject.transform.rotation.y <= 0.01f)
            || (mE.startPos == 1 && this.gameObject.transform.rotation.y >= -0.01f))
        {
            yield return new WaitForSeconds(0.1f); //0.1f�҂�

            //�Ώۂ��G1���C���[�̂Ƃ�
            if (this.gameObject.layer == ENEMEY1_LAYER)
            {
                newPos += new Vector3(mE.startPos * 15.0f, 0, 0); //���W���X�V
                steamEffect.transform.Rotate(0, mE.startPos * 5.0f, 0); //�X�`�[���G�t�F�N�g����]
            }
            this.gameObject.transform.Rotate(0, mE.startPos * -5.0f, 0); //�G�I�u�W�F�N�g����]
        }
    }

    //�A�C�e���𗎂Ƃ�����
    private void DropMaterial()
    {
        getCount = (int)Integerization(rand) % MAX_DROP + 1; //1~MAX_DROP�擾
        getMaterial = (int)Integerization(rand) % vMatNam; //�f�ޔԍ����擾
        vMatOwned[getMaterial] += getCount; //�����f�ރ��X�g�ɉ�����
        //Debug.Log(VIRUS_NAME[getMaterial] + "��" + getCount + "����");
    }
}