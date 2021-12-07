using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class CommonFunction : MonoBehaviour
    {
        private const string objName = "GameManager"; //�Q�[���I�u�W�F�N�g��

        /// <summary>
        /// flag�𔽓]����
        /// </summary>
        public static bool ReverseFlag(bool flag)
        {
            return !flag ? true : false;
        }

        public static float Integerization(int n)
        {
            return (n <= -1) ? n * -1.0f : n * 1.0f;
        }
        /// <summary>
        /// ���̃I�u�W�F�N�g�̃}�e���A�����w��̃}�e���A���J���[�ɕς���
        /// </summary>
        /// <param name="other">���̃I�u�W�F�N�g</param>
        /// <param name="mtl">�}�e���A��</param>
        public static void ChangeMaterialColor(GameObject obj, Material mtl)
        {
            obj.GetComponent<Renderer>().material = mtl; //�}�e���A������
        }

        public static GameObject GetHierarchyObject(string s)
        {
            return GameObject.Find(s);
        }

        /// <summary>
        /// ���̃X�N���v�g�̃I�u�W�F�N�g���擾
        /// </summary>
        /// <typeparam name="T">���̃X�N���v�g</typeparam>
        /// <param name="obj">�I�u�W�F�N�g</param>
        public static T GetOtherScriptObject<T>(GameObject obj)
        {
            obj = GameObject.Find(objName);
            return obj.GetComponent<T>();
        }
    }
}


