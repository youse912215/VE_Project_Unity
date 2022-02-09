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

    private ParticleSystem impactEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem bloodEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem steamEffect; //�X�`�[���̃G�t�F�N�g
    private ParticleSystem jewelEffect; //��΂̃G�t�F�N�g

    private int enemyRank; //�K��
    private const int MAX_RANK = 5; //�ő�K��
    private float currentHp; //���݂�HP
    private float maxHp; //�ő�HP
    private const float INIT_HEALTH = 300.0f; //����HP�Œ�l
    private const float HEALTH_WEIGHT = 500.0f;
    private const float ATTACK_DAMAGE = 1.5f; //�U����
    private bool isImpactSet; //�Ռ��G�t�F�N�g���Z�b�g�������ǂ���
    private const float IMPACT_POS_Z = -170.0f; //�Ռ��G�t�F�N�g��Z���W
    private const float EFFECT_HEIGHT = 250.0f; //�G�t�F�N�g����
    private const int ENEMEY1_LAYER = 7; //�G1�̃��C���ԍ�
    private readonly Vector3 STEAM_POS = new Vector3(-65.0f, 145.0f, -ONE_CIRCLE);
    private int getCount = 0;
    private int getMaterial = 99;
    private const int MAX_DROP = 5; //�ő�h���b�v��
    private List<bool> isVirusDamage = new List<bool> { false, false, false, false, false, false, false, false };
    private float pDefence = 0.0f; //�ђʖh��

    /* public */
    public float totalDamage = 0.0f; //���v�_���[�W
    private float takenDamage = 0.0f;
    public bool isInfection; //�����������ǂ���
    public bool isDead; //���񂾂��ǂ���

    private MoveEnemy mE;
    private DamageManager dM;
    private Vector3 updatePos;

    /// <summary>
    /// �J�n����
    /// </summary>
    void Start()
    {
        slider.value = 1; //Slider�𖞃^��
        isImpactSet = false;
        impactEffect = Instantiate(impactPs); //�G�t�F�N�g����
        impactEffect.Stop(); //�G�t�F�N�g��~

        //���X�N���v�g�擾
        mE = this.gameObject.GetComponent<MoveEnemy>();
        dM = this.gameObject.GetComponent<DamageManager>();

        enemyRank = (int)Integerization(rand) % MAX_RANK; //�K�����擾
        pDefence = GetArmor(); //���X�g����ђʖh����擾
        maxHp = INIT_HEALTH * (enemyRank + 1) + HEALTH_WEIGHT * WaveGauge.currentDay; //�ő�HP���擾
        currentHp = maxHp; //���݂�HP�ɍő�HP����

        if (this.gameObject.layer != ENEMEY1_LAYER) return; //�Ώۃ��C���[�ȊO�́A�������X�L�b�v
        steamEffect = Instantiate(steamPs); //�G�t�F�N�g����
        steamEffect.Play(); //�G�t�F�N�g�J�n

        steamEffect.transform.rotation =
                Quaternion.Euler(new Vector3(ONE_CIRCLE, QUARTER_CIRCLE * mE.startPos, 0.0f));
        updatePos = new Vector3(-150.0f * mE.startPos, 0, 0);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        //�L�����o�X���[�h��TowerDefense�ȊO�̂Ƃ��A�������X�L�b�v
        if (CanvasManager.canvasMode != CanvasManager.CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        UpdateSteamEffect(); //�X�`�[���G�t�F�N�g�X�V
        AttackAction(); //�U�����s��

        if (!isInfection) return; //�������ȊO�́A�������X�L�b�v
        InfectionAction(); //�E�C���X�������s��

        if (currentHp > 0.0f) return; //�����Ă���Ԃ́A�������X�L�b�v
        DropMaterial(); //�f�ނ��h���b�v
        DeadAction(); //���S���s��
    }

    /// <summary>
    /// �ђʃV�[���h���擾
    /// </summary>
    /// <returns></returns>
    private float GetArmor()
    {
        if (this.gameObject.layer == ENEMEY1_LAYER)
            return PENETRATION_DEFENCE_LIST0[enemyRank];
        return PENETRATION_DEFENCE_LIST1[enemyRank];
    }

    /// <summary>
    /// �̗͂��v�Z����
    /// </summary>
    public void CulculationHealth(int type)
    {
        if (isVirusDamage[type]) return;
        isVirusDamage[type] = true;
        totalDamage += (isVirusDamage[type]) ? FORCE_WEIGHT[colonyLevel] * force[type].x : 0.0f;

        Debug.Log("isDamage::" + isVirusDamage[0] + ":" + isVirusDamage[1] + ":" + isVirusDamage[2] + ":" + isVirusDamage[3] + ":"
             + isVirusDamage[4] + ":" + isVirusDamage[5] + ":" + isVirusDamage[6] + ":" + isVirusDamage[7] + ":");
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
        deadCount++; //�݌v�̎��S�����J�E���g
        SetStopAction(impactEffect, true); //�Ռ��G�t�F�N�g���폜
        if (this.gameObject.layer == ENEMEY1_LAYER)
            SetStopAction(steamEffect, true); //�G1�̂݁A�X�`�[���G�t�F�N�g���폜  
        DeadEffect(bloodPs, bloodEffect); //���G�t�F�N�g
        DeadEffect(jewelPs, jewelEffect); //��΃G�t�F�N�g
        Destroy(gameObject); //�G�I�u�W�F�N�g���폜
    }

    /// <summary>
    /// �G�t�F�N�g�̒�~�ݒ�
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="isLoop"></param>
    private void SetStopAction(ParticleSystem effect, bool isLoop)
    {
        var iMain = effect.main;
        iMain.loop = false;
        iMain.stopAction = ParticleSystemStopAction.Destroy;
        if (!isLoop) return;
        effect.Stop();
    }

    /// <summary>
    /// ���S���G�t�F�N�g
    /// </summary>
    /// <param name="ps"></param>
    /// <param name="effect"></param>
    private void DeadEffect(ParticleSystem ps, ParticleSystem effect)
    {
        effect = Instantiate(ps); //�G�t�F�N�g����
        SetEffectPos(effect); //�ʒu���Z�b�g   
        SetStopAction(effect, false);
        effect.Play(); //�G�t�F�N�g����
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
            + updatePos; //�X�V
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
                updatePos += new Vector3(mE.startPos * 15.0f, 0, 0); //���W���X�V
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