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

        public const int CATEGORY = 3; //�E�C���X�̎��
        public const int OWNED = 5; //�E�C���X�ۗ̕L��
        public const float CAM_DISTANCE = 4500.0f;

        public static int[] vNum = new int[CATEGORY]; //�e�E�C���X�̐ݒu��
        public static bool[] isLimitCapacity = new bool[CATEGORY]; //���E�e��
        public static string VirusName = "Virus"; //�E�C���X����

        /// <summary>
        /// �E�C���X�ԍ��ɑΉ�����A�l���Z�b�g����
        /// </summary>
        /// <param name="v">�E�C���X�\����</param>
        /// <param name="n">�E�C���X��</param>
        public static void InitValue(Virus[,] v, VIRUS_NUM n)
        {
            int i = (int)n; //�ԍ��擾
            vNum[i] = 0; //�ۗL���擾

            for (int a = 0; a < OWNED; a++) {
                v[i, a].pos = pos[i]; //�ʒu�擾
                v[i, a].rot = rot[i]; //�p�x�擾
                v[i, a].scl = scl[i]; //�X�P�[���擾
                v[i, a].force = force[i]; //3�͎擾
                v[i, a].isActivity = false; //������Ԏ擾
            }
        }
        
        /// <summary>
        /// �E�C���X���A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="n">�E�C���X��</param>
        public static void GetVirus(VIRUS_NUM n)
        {
            int i = (int)n; //�E�C���X�ԍ�

            //�E�C���X�����ʔԍ����ۗL�������Ȃ�
            if (vNum[i] < OWNED - 1) vNum[i]++;
            else isLimitCapacity[i] = true; //���E�e�ʂɓ��B
        }

        /// <summary>
        /// �E�C���X�𐶐����A���������擾����
        /// </summary>
        /// <param name="v">�E�C���X�\����</param>
        /// <param name="n">�E�C���X��</param>
        public static void GenerationVirus(Virus[,] v, VIRUS_NUM n)
        {
            int i = (int)n; //�E�C���X�ԍ�
            int j = vNum[i]; //�E�C���X�����ʔԍ�
            v[i, j].isActivity = true; //������Ԃ��A�N�e�B�u
        }

        /// <summary>
        /// �ݒu�w�肵���E�C���X�̍��W��ۑ�����
        /// </summary>
        /// <param name="v">�E�C���X�\����</param>
        /// <param name="n">�E�C���X��</param>
        /// <param name="obj">�E�C���X�I�u�W�F�N�g�i�v���n�u�j</param>
        /// <param name="wPos">���[���h���W</param>
        public static void SaveVirusPosition(Virus[,] v, VIRUS_NUM n, GameObject[,] obj, Vector3 wPos)
        {
            int i = (int)n; //�E�C���X�ԍ�
            int j = vNum[i]; //�E�C���X�����ʔԍ�
            obj[i, j].transform.position = wPos; //���݂̃}�E�X�̃��[���h���W��ۑ�
            v[i, j].isActivity = false; //������Ԃ��C���A�N�e�B�u
        }

        /// <summary>
        /// �͈͊O����
        /// </summary>
        /// <param name="n">�E�C���X��</param>
        /// <returns>���E�e�ʂ̏��</returns>
        public static bool ProcessOutOfRange(int n)
        {
            return isLimitCapacity[n];
        }
    }
}