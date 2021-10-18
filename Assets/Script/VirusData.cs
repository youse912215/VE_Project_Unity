/* �E�C���X�f�[�^ */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class VirusData : MonoBehaviour
    {
        public struct Virus
        {
            public Vector3 pos; //�ʒu
            public Quaternion rot; //�p�x
            public Vector3 scl; //�X�P�[��

            public Vector3 force; //3�́i�E���́A�����́A�˔j�́j

            public float durableValue; //�ϋv�l
            public bool isActivity; //�������
        }

        //�E�C���X�ԍ�
        public enum VIRUS_NUM
        {
            CODE_CLD,
            CODE_INF,
            CODE_19,
        }

        //�ʒu
        public static readonly Vector3[] pos =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        };

        //�ʒu
        public static readonly Quaternion[] rot =
        {
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
        };

        //�X�P�[��
        public static readonly Vector3[] scl =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        }; 

        //3��
        public static readonly Vector3[] force =
            {
            new Vector3(1, 1, 1), //code.cld
            new Vector3(3, 2, 1), //code.inf
            new Vector3(2, 3, 3), //code.19

            };

        public const int CATEGORY = 3;
        public const int OWNED = 5;

        public static int[] owned = new int[CATEGORY];

        /// <summary>
        /// �E�C���X�ԍ��ɑΉ�����A�l���Z�b�g����
        /// </summary>
        /// <param name="v">�E�C���X�^�̕ϐ�</param>
        /// <param name="n">�E�C���X��</param>
        public static void InitValue(Virus[,] v, VIRUS_NUM n)
        {
            int i = (int)n; //�ԍ��擾
            owned[i] = 0; //�ۗL���擾

            for (int a = 0; a < OWNED; a++) v[i, a].pos = pos[i]; //�ʒu�擾
            for (int a = 0; a < OWNED; a++) v[i, a].rot = rot[i]; //�p�x�擾
            for (int a = 0; a < OWNED; a++) v[i, a].scl = scl[i]; //�X�P�[���擾
            for (int a = 0; a < OWNED; a++) v[i, a].force = force[i]; //3�͎擾
            for (int a = 0; a < OWNED; a++) v[i, a].isActivity = false; //������Ԏ擾
        }
        
        /// <summary>
        /// �E�C���X���A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="v">�E�C���X�^�̕ϐ�</param>
        /// <param name="n">�E�C���X��</param>
        public static void GetVirus(VIRUS_NUM n)
        {
            int i = (int)n;
            if (owned[i] != 0) owned[i]++;
        }

        public static void ReleaseVirus(VIRUS_NUM n)
        {
            int i = (int)n;
            if (owned[i] != 0) owned[i]--;
        }

        public static void SetVirus(Virus[,] v, VIRUS_NUM n, GameObject[,] obj)
        {
            int i = (int)n; //�E�C���X�ԍ�
            int j = owned[i]; //�E�C���X�����ʔԍ�
            v[i, j].isActivity = true;
        }

        public static void SaveVirusPosition(Virus[,] v, VIRUS_NUM n, GameObject[,] obj, Vector3 wPos)
        {
            int i = (int)n; //�E�C���X�ԍ�
            int j = owned[i]; //�E�C���X�����ʔԍ�
            obj[i, j].transform.position = wPos; //���݂̃}�E�X�̃��[���h���W��ۑ�
            v[i, j].isActivity = false;
        }

        public static bool ProcessOutOfRange(VIRUS_NUM n)
        {
            int i = (int)n;
            if (owned[i] >= OWNED - 1) return true;
            return false;
        }

        //public static int AllocationNumber(VIRUS_NUM n)
        //{
        //    int i = (int)n;
        //    return OWNED - owned[i] - 1;
        //}
    }
}