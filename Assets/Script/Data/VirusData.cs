/* �E�C���X�f�[�^ */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class VirusData : MonoBehaviour
    {
        //�e�E�C���X�\����
        public struct VirusParents
        {
            public GameObject[] obj; //�e�I�u�W�F�N�g
            public string tag; //�^�O��
            public int setCount; //���ݐݒu������
            public int creationCount; //�쐬������
        }

        //�q�E�C���X�\����
        public struct VirusChildren
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
            CODE_NOV,
            CODE_EHF,
            CODE_EV,
            CODE_BD,
            CODE_ULT,
            CODE_NONE,
        }

        //�ʒu
        public static readonly Vector3[] pos =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
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
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
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
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        }; 

        //3��
        public static readonly Vector3[] force =
        {
            new Vector3(1, 1.0f, 1), //code.cld
            new Vector3(3, 2, 1), //code.inf
            new Vector3(2, 3, 3), //code.19
            new Vector3(1, 3, 1), //code.cld
            new Vector3(3, 1.25f, 1), //code.inf
            new Vector3(2, 1.5f, 3), //code.19
            new Vector3(5, 2, 1), //code.cld
            new Vector3(5, 5, 5), //code.ult

        };

        public static readonly Color[] COLOR_SET =
        {
            new Color(84, 7, 84),
            new Color(0, 7, 84),
            new Color(0, 120, 84),
            new Color(255,255,255),
            new Color(255,255,255),
            new Color(255,255,255),
            new Color(255,255,255),
            new Color(255,255,255),
        };

        public const int V_CATEGORY = 8; //�E�C���X�̎��
        public const int OWNED = 5; //�E�C���X�̍ő�ۗL��

        public static bool[] isLimitCapacity = new bool[V_CATEGORY]; //���E�e��
        public static string VirusHeadName = "Virus"; //�E�C���X����
        public static string[] VirusTagName = {"Cold", "Inf", "19", "Nov", "Ehf", "Ev", "Bd", "Ult"};
        public static readonly Vector3 V_SIZE = new Vector3(10.0f, 10.0f, 10.0f);
        public static Vector3 vRange = new Vector3(50.0f, 50.0f, 50.0f);

        public const float CONSTANT_FORCE = 5.0f;

        /// <summary>
        /// �E�C���X�ԍ��ɑΉ�����A�l���Z�b�g����
        /// </summary>
        /// <param name="vC">�E�C���X�\����</param>
        /// <param name="n">�E�C���X��</param>
        public static void InitValue(VirusParents[] vP, VirusChildren[,] vC, VIRUS_NUM n)
        {
            int i = (int)n; //�ԍ��擾
            vP[i].setCount = 0; //���ݐ��擾

            for (int a = 0; a < OWNED; a++) {
                vC[i, a].pos = pos[i]; //�ʒu�擾
                vC[i, a].rot = rot[i]; //�p�x�擾
                vC[i, a].scl = scl[i]; //�X�P�[���擾
                vC[i, a].force = force[i]; //3�͎擾
                vC[i, a].isActivity = false; //������Ԏ擾
            }
        }
        
        /// <summary>
        /// �E�C���X���A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="n">�E�C���X��</param>
        public static void GetVirus(VirusParents[] vP, int n)
        {
            if (vP[n].setCount == vP[n].creationCount) isLimitCapacity[n] = true; //���E�e�ʂɓ��B
        }

        /// <summary>
        /// �E�C���X�𐶐����A���������擾����
        /// </summary>
        /// <param name="vC">�E�C���X�\����</param>
        /// <param name="n">�E�C���X��</param>
        public static void GenerationVirus(VirusParents[] vP,�@VirusChildren[,] vC, int n)
        {
            int j = vP[n].setCount; //�E�C���X�����ʔԍ�
            vC[n, j].isActivity = true; //������Ԃ��A�N�e�B�u
        }

        /// <summary>
        /// �ݒu�w�肵���E�C���X�̍��W��ۑ�����
        /// </summary>
        /// <param name="vC">�E�C���X�\���̔z��</param>
        /// <param name="vNum">�E�C���X�ԍ�</param>
        /// <param name="sNum">�ݒu�ԍ�</param>
        /// <param name="obj">�Q�[���I�u�W�F�N�g</param>
        /// <param name="wPos">���[���h���W</param>
        public static void SaveVirusPosition(VirusChildren[,] vC, int vNum, int sNum,
            GameObject[,] obj, Vector3 wPos)
        {
            int i = vNum; //�E�C���X�ԍ�
            int j = sNum; //�E�C���X�����ʔԍ�
            obj[i, j].transform.position = wPos; //�E�C���X�I�u�W�F�N�g�ɁA���݂̃}�E�X�̃��[���h���W��ۑ�
            vC[i, j].pos = wPos;  //�E�C���X�\���̂ɁA���݂̃}�E�X�̃��[���h���W��ۑ�
            vC[i, j].isActivity = false; //������Ԃ��C���A�N�e�B�u
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