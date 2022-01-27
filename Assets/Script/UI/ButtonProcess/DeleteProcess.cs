using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static Call.VirusData;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;

public class DeleteProcess : MonoBehaviour
{
    GameObject obj;
    ActVirus actV;

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj);
    }

    /// <summary>
    /// pushing delete button
    /// </summary>
    public void DeleteButtonPush()
    {
        int mode = actV.buttonMode;
        actV.SetUIActivity(true);

        if (!menuMode)
        {
            DestroyBeforeVirus(actV.buttonMode); // �E�C���X���폜����
            actV.isGrabbedVirus = false;
        }
        else
        {
            Destroy(actV.vObject[actV.column, actV.row]); //�E�C���X���폜����
            SortVirusArray(actV.column, actV.row, actV); //�E�C���X�z����\�[�g
        }
        
        ReverseMenuFlag(BACK); //���j���[�����
        actV.isOpenMenu = false; //���j���[�t���O��false

        //���E�e�ʂɒB���Ă��Ȃ��Ƃ��A�������X�L�b�v
        if (actV.vParents[mode].setCount != actV.vParents[mode].creationCount) return;
        isLimitCapacity[mode] = false; //�e�ʂ̌��E��Ԃ�����
    }

    /// <summary>
    /// �E�C���X���폜����
    /// </summary>
    private void DestroyBeforeVirus(int mode)
    {
        Destroy(actV.vObject[mode, actV.row]); //�Q�[���I�u�W�F�N�g���폜
    }

    /// <summary>
    /// �E�C���X�z����\�[�g����
    /// </summary>
    private void SortVirusArray(int column, int row, ActVirus act)
    {
        var list1 = new List<VirusChildren>(); //���X�g��`
        var list2 = new List<GameObject>(); //���X�g��`
        VirusChildren[] structArray = act.vChildren.Cast<VirusChildren>().ToArray(); //�E�C���X�\���̔z����ꎟ����
        GameObject[] objectArray = act.vObject.Cast<GameObject>().ToArray(); //�E�C���X�Q�[���I�u�W�F�N�g�z����ꎟ����

        //�w��̃E�C���X�̃��X�g���쐬
        for (int i = 0; i < OWNED; ++i)
        {
            list1.Add(structArray[column * OWNED + i]);
            list2.Add(objectArray[column * OWNED + i]);
        }

        //�v�f�𖖔��ɒǉ�
        list1.Add(list1[row]); 
        list2.Add(list2[row]);
        //�w��̗v�f���폜
        list1.RemoveAt(row);
        list2.RemoveAt(row); 

        //���񂳂����Q�[���I�u�W�F�N�g���X�g�ɍX�V
        for (int i = 0; i < OWNED; ++i)
        {
            act.vChildren[column, i] = list1[i];
            act.vObject[column, i] = list2[i];
        }
    }
}
