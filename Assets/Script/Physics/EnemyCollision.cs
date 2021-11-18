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
    private GameObject enemyObj;

    private GameObject obj;
    private ActVirus actV;
    private float cCount;
    private bool isCollision;
    private const float ACTIVE_COUNT = 30.0f;

    GameObject[] eList = new GameObject[ALL_ENEMEY_MAX * E_CATEGORY];

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj);
        cCount = 0;
        isCollision = false;
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

        ChangeMaterialColor(this.gameObject, rangeMat[3]);
        enemyObj = other.gameObject;
        isCollision = true;

        EnemyHealth eH;
        eH = enemyObj.GetComponent<EnemyHealth>();
        eH.isInfection = true;
        eH.damage |= (uint)(0b0001 << GetVirusNumber());
        Debug.Log(eH.damage);
        eH.total = (float)eH.CulculationHealth(eH.damage);
        

        var ps = enemyObj.GetComponentsInChildren<ParticleSystem>(); //�͈͂ɓ������G�̃p�[�e�B�N�����擾
        var renderer = enemyObj.GetComponentsInChildren<ParticleSystemRenderer>(); // //�͈͂ɓ������G�̃p�[�e�B�N�������_���[���擾
        renderer[GetVirusNumber()].material = virusMat[GetVirusNumber()]; //�}�e���A�����E�C���X�̎�ނɂ���ĕύX
        ps[GetVirusNumber()].Play(); //�p�[�e�B�N������      
    }

    /// <summary>
    /// �͈͂��痣�ꂽ�Ƃ�
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g</param>
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Enemy") return;
        ChangeMaterialColor(this.gameObject, rangeMat[0]);
        
    }

    /// <summary>
    /// �Փ˃J�E���g���v�Z���A�E�C���X������
    /// </summary>
    private void CountCollisionTime()
    {
        cCount += 0.1f; //�J�E���g�J�n

        if (cCount <= ACTIVE_COUNT) return;
        this.gameObject.transform.parent.gameObject.SetActive(false);
        cCount = 0;
        isCollision = false;
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
}
