using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static MaterialManager;
using static ActVirus;
using static WarriorData;
using static Call.VirusData;

public class EnemyCollision : MonoBehaviour
{
    private GameObject enemyObj; //�G�I�u�W�F�N�g�i�[�p

    private GameObject obj; //�I�u�W�F�N�g
    private ActVirus actV; //�X�N���v�g
    private float cCount; //�Փ˃J�E���g
    private bool isCollision; //�Փˏ��
    private const float ACTIVE_COUNT = 10.0f; //�A�N�e�B�u�J�E���g

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj); //ActVirus�X�N���v�g���擾
        cCount = 0; //�Փ˃J�E���g��0��
        isCollision = false; //�Փˏ�Ԃ�false
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollision) return;
        CountCollisionTime();
    }

    /// <summary>
    /// �͈͂ɓ����Ă����
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g</param>
    void OnTriggerStay(Collider other) {
        if (actV.isGrabbedVirus) return; //�E�C���X�������Ă���Ƃ��́A�������X�L�b�v
		if (other.gameObject.tag != "Enemy") return; //�G�ȊO�́A�������X�L�b�v

        ChangeMaterialColor(this.gameObject, rangeMat[3]); //�}�e���A���J���[��ύX
        enemyObj = other.gameObject; //�͈͂ɓ������I�u�W�F�N�g���i�[
        isCollision = true; //�Փˏ�Ԃ�true
        GetEnemyDamage(enemyObj); //�G�̃_���[�W���擾
        ChangeVirusEffect(enemyObj); //�E�C���X�̃G�t�F�N�g��ύX
    }

    /// <summary>
    /// �͈͂��痣�ꂽ�Ƃ�
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g</param>
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Enemy") return; //�G�ȊO�́A�������X�L�b�v

        ChangeMaterialColor(this.gameObject, rangeMat[0]); //�}�e���A���J���[��ύX
        
    }

    /// <summary>
    /// �Փ˃J�E���g���v�Z���A�E�C���X������
    /// </summary>
    private void CountCollisionTime()
    {
        cCount += 0.1f; //�J�E���g�J�n

        if (cCount <= ACTIVE_COUNT) return; //

        this.gameObject.transform.parent.gameObject.SetActive(false); //�͈͂��A�N�e�B�u��Ԃ�
        cCount = 0; //�Փ˃J�E���g�����Z�b�g
        isCollision = false; //�Փˏ�Ԃ�false
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
        eH.damage |= (uint)(0b0001 << GetVirusNumber()); //�擾�����E�C���X�ԍ����A�V�t�g���Ă���OR�ő�����Z
        eH.total = (float)eH.CulculationHealth(eH.damage); //�v�Z�����_���[�W���g�[�^���l�Ƃ��Ċi�[
    }

    /// <summary>
    /// �E�C���X�G�t�F�N�g��ύX
    /// </summary>
    /// <param name="obj">�G�I�u�W�F�N�g</param>
    private void ChangeVirusEffect(GameObject obj)
    {
        var ps = obj.GetComponentsInChildren<ParticleSystem>(); //�͈͂ɓ������G�̃p�[�e�B�N�����擾
        var renderer = obj.GetComponentsInChildren<ParticleSystemRenderer>(); // //�͈͂ɓ������G�̃p�[�e�B�N�������_���[���擾
        renderer[GetVirusNumber()].material = virusMat[GetVirusNumber()]; //�}�e���A�����E�C���X�̎�ނɂ���ĕύX
        ps[GetVirusNumber()].Play(); //�p�[�e�B�N������ 
    }
}
