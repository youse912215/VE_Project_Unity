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
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    /* private */
    [SerializeField] private Slider slider; //�X���C�_�[
    [SerializeField] private ParticleSystem impactPs; //�Ռ��g�̃p�[�e�B�N���V�X�e��
    [SerializeField] private ParticleSystem bloodPs; //�����Ԃ��̃p�[�e�B�N���V�X�e��
    [SerializeField] private ParticleSystem steamPs; //steam�̃p�[�e�B�N���V�X�e��
    [SerializeField] private Material mat;

    private ParticleSystem impactEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem bloodEffect; //�Ռ��g�̃G�t�F�N�g
    private ParticleSystem steamEffect;

    private float currentHp; //���݂�HP
    private float maxHp; //�ő�HP
    private const float INIT_HEALTH = 2.5f;
    private const float ATTACK_DAMAGE = 1.5f; //�U����
    private bool isImpactSet; //�Ռ��G�t�F�N�g���Z�b�g�������ǂ���
    private const float IMPACT_POS_Z = -170.0f; //�Ռ��G�t�F�N�g��Z���W
    private const float EFFECT_HEIGHT = 250.0f; //�G�t�F�N�g����
    private const int ENEMEY1_LAYER = 7; //�G1�̃��C���ԍ�
    private readonly Vector3 STEAM_POS = new Vector3(-65.0f, 145.0f, -180.0f);
    private int getCount = 0;
    private int getMaterial = 99;
    private const int MAX_DROP = 5;

    /* public */
    public uint takenDamage; //��_���[�W
    public float totalDamage; //���v�_���[�W
    public bool isInfection; //�����������ǂ���
    public bool isDead; //���񂾂��ǂ���

    private MoveEnemy mE;
    private Vector3 newPos;

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

        mE = this.gameObject.GetComponent<MoveEnemy>();

        if (this.gameObject.layer != ENEMEY1_LAYER) return; //�Ώۃ��C���[�ȊO�́A�������X�L�b�v
        steamEffect = Instantiate(steamPs); //�G�t�F�N�g����
        steamEffect.Play(); //�G�t�F�N�g�J�n

        steamEffect.transform.rotation =
                Quaternion.Euler(new Vector3(180.0f, QUARTER_CIRCLE * mE.startPos, 0.0f));
        newPos = new Vector3(-150.0f * mE.startPos, 0, 0);
    }

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
    /// <param name="damage">�_���[�W</param>
    /// <returns></returns>
    public int CulculationHealth(uint damage)
    {
        uint d0 = ((damage & 0b11111111) >> 0) * (uint)force[virusSetList[currentSetNum]].x; //
        uint d1 = ((damage & 0b11111111) >> 1) * (uint)force[virusSetList[currentSetNum]].x; //
        uint d2 = ((damage & 0b11111111) >> 2) * (uint)force[virusSetList[currentSetNum]].x; //
        return (int)(d0 + d1 + d2);
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
        deadCount++; //�݌v�̎��S�����J�E���g
        Destroy(impactEffect); //�Ռ��g�G�t�F�N�g���폜
        SetEffectPos(bloodEffect); //�G�t�F�N�g�̈ʒu���Z�b�g
        Destroy(steamEffect); //steam�G�t�F�N�g���폜
        bloodEffect.Play(); //���̃G�t�F�N�g
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
                                                   //
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
        getCount = rand % MAX_DROP + 1; //1~MAX_DROP�擾
        getMaterial = rand % vMatNam; //�f�ޔԍ����擾
        vMatOwned[getMaterial] += getCount; //�����f�ރ��X�g�ɉ�����
    }
}