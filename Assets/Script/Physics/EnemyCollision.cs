using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static MaterialManager;
using static Call.VirusData;
using static BlinkingEnemy;
using static VirusMaterialData;

public class EnemyCollision : MonoBehaviour
{
    private GameObject opponent; //����i�G�j�I�u�W�F�N�g�i�[�p
    private GameObject obj; //�I�u�W�F�N�g
    private ActVirus actV; //�X�N���v�g
    private float rangeActiveTime; //�Փ˃J�E���g  
    private const float ACTIVE_COUNT = 10.0f; //�A�N�e�B�u�J�E���g
    private const float WAIT_FOR_SECONDS = 0.5f; //�ҋ@����
    private const float INCREASED_SECONDS = 0.1f; //��������

    public static bool cool; //�N�[���_�E��
    public static float cooldown; //�N�[���_�E���^�C��
    public static bool isEnemyCollision; //�Փˏ��

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj); //ActVirus�X�N���v�g���擾
        rangeActiveTime = 0.0f; //�Փ˃J�E���g��0��
        isEnemyCollision = false; //�Փˏ�Ԃ�false
        cool = false;
        cooldown = 0.0f;
    }

    /// <summary>
    /// �͈͂ɓ����Ă����
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g</param>
    void OnTriggerStay(Collider other)
    {
        if (actV.isGrabbedVirus) return; //�E�C���X�������Ă���Ƃ��́A�������X�L�b�v
        if (other.gameObject.tag != "Enemy") return; //�G�ȊO�́A�������X�L�b�v

        ChangeMaterialColor(this.gameObject, rangeMat[3]); //�}�e���A���J���[��ύX
        opponent = other.gameObject; //�͈͂ɓ������I�u�W�F�N�g���i�[
        isEnemyCollision = true; //�Փˏ�Ԃ�true
        GetEnemyDamage(opponent); //�G�̃_���[�W���擾
        ChangeVirusEffect(opponent); //�E�C���X�̃G�t�F�N�g��ύX
        StartCoroutine(CountRangeTime()); //�Փˎ��Ԃ��J�E���g

        if (rangeActiveTime <= ACTIVE_COUNT) return;
        var pObject = this.gameObject.transform.parent.gameObject; //�e�I�u�W�F�N�g���i�[
        actV.ExplosionVirus(gameObject.transform.parent.gameObject.transform.position); //�E�C���X���u�𔚔�������
        DecreaseCountVirus(pObject); //�E�C���X�������炷
        Destroy(pObject); //�e�I�u�W�F�N�g���폜
        isEnemyCollision = false; //�Փˏ�Ԃ�false
        rangeActiveTime = 0; //�Փ˃J�E���g�����Z�b�g
        if (cooldown == 0.0f) cool = true; //�N�[���_�E�����
    }

    /// <summary>
    /// �E�C���X�������炷
    /// </summary>
    /// <param name="pObject"></param>
    void DecreaseCountVirus(GameObject pObject)
    {
        if (!pObject) return; //��O�̓X�L�b�v
        vSetCount[GetVirusNumber()]--; //�ݒu�������炷

        //�^�O����v�����Ƃ�
        if (pObject.tag == VirusTagName[GetVirusNumber()])
        {
            vCreationCount[GetVirusNumber()]--; //�쐬�������炷
            isLimitCapacity[GetVirusNumber()] = false; //�e�ʂ̌��E��Ԃ�����
        }
    }

    /// <summary>
    /// �͈͂��痣�ꂽ�Ƃ�
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return; //�G�ȊO�́A�������X�L�b�v
        ChangeMaterialColor(this.gameObject, rangeMat[0]); //�}�e���A���J���[��ύX
    }

    /// <summary>
    /// �E�C���X�̎�ނ��擾
    /// </summary>
    /// <returns></returns>
    private int GetVirusNumber()
    {
        int n = 0; //�i�[�p�ϐ�
        //�E�C���X�^�O�̒l�Ɨv�f�ԍ���Ԃ��A�J��Ԃ�
        foreach (var (value, index) in VirusTagName.Select((value, index) => (value, index)))
        {
            //�^�O�ƈ�v�����Ƃ�
            if (value == this.gameObject.transform.parent.tag)
            {
                n = index; //�v�f�ԍ����i�[
                break; //���[�v�𔲂��o��
            }
        }
        return n;
    }

    /// <summary>
    /// �G�̃_���[�W���擾
    /// </summary>
    /// <param name="obj">�G�I�u�W�F�N�g</param>
    private void GetEnemyDamage(GameObject obj)
    {
        EnemyHealth eH; //EenemyHealth�X�N���v�g
        eH = obj.GetComponent<EnemyHealth>(); //�X�N���v�g���擾
        eH.isInfection = true; //������Ԃ�true
        eH.takenDamage |= (uint)(0b0001 << GetVirusNumber()); //�擾�����E�C���X�ԍ����A�V�t�g���Ă���OR�ő�����Z
        eH.totalDamage = (float)eH.CulculationHealth(eH.takenDamage); //�v�Z�����_���[�W���g�[�^���l�Ƃ��Ċi�[
    }

    /// <summary>
    /// �E�C���X�G�t�F�N�g��ύX
    /// </summary>
    /// <param name="obj">�G�I�u�W�F�N�g</param>
    private void ChangeVirusEffect(GameObject obj)
    {
        var ps = obj.GetComponentsInChildren<ParticleSystem>(); //�͈͂ɓ������G�̃p�[�e�B�N�����擾
        var renderer = obj.GetComponentsInChildren<ParticleSystemRenderer>(); // //�͈͂ɓ������G�̃p�[�e�B�N�������_���[���擾
        SetMaterials(obj);
        renderer[0].material = actV.defaultPs; //�}�e���A�����E�C���X�̎�ނɂ���ĕύX
        ps[0].Play(); //�p�[�e�B�N������
        renderer[1].material = actV.defaultPs; //�}�e���A�����E�C���X�̎�ނɂ���ĕύX
        ps[1].Play();
    }

    /// <summary>
    /// �͈͌��ʎ��Ԃ��J�E���g
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountRangeTime()
    {
        yield return new WaitForSeconds(WAIT_FOR_SECONDS);
        rangeActiveTime += INCREASED_SECONDS; //�J�E���g�J�n   
    }
}
