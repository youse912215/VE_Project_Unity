using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class CommonFunction : MonoBehaviour
    {
        /// <summary>
        /// flag�𔽓]����
        /// </summary>
        public static bool ReverseFlag(bool flag)
        {
            return !flag ? true : false;
        }

        /// <summary>
        /// ���̃I�u�W�F�N�g�̃}�e���A�����w��̃}�e���A���J���[�ɕς���
        /// </summary>
        /// <param name="other">���̃I�u�W�F�N�g</param>
        /// <param name="mtl">�}�e���A��</param>
        public static void ChangeRangeColor(GameObject obj, Material mtl)
        {
            //obj = other.gameObject; //���Q�[���I�u�W�F�N�g���擾
            obj.GetComponent<Renderer>().material = mtl; //�}�e���A������
        }

        public static T GetActVirusScript<T>(GameObject obj)
        {
            obj = GameObject.Find("GameManager");
            return obj.GetComponent<T>();
        }
    }
}


