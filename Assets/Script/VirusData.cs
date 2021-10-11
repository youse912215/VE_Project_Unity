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
            public int owned; //�ۗL�� 
            public bool isActivity; //�������
        }

        public enum VIRUS_NUM
        {
            CODE_CLD,
            CODE_INF,
            CODE_19,
        }

        /* �ʒu */
        public static readonly Vector3[] pos =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        };

        /* �ʒu */
        public static readonly Quaternion[] rot =
        {
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
            new Quaternion(0, 0, 0, 0),
        };

        /* �X�P�[�� */
        public static readonly Vector3[] scl =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        }; 

        /* 3�� */
        public static readonly Vector3[] force =
            {
            new Vector3(1, 1, 1), //code.cld
            new Vector3(3, 2, 1), //code.inf
            new Vector3(2, 3, 3), //code.19

            };

        public const int num = 3;
        private const int owned = 5;

        //�E�C���X�ԍ��ɑΉ�����A�l���Z�b�g����
        public static void setValue(Virus[] v, VIRUS_NUM n)
        {
            int num = (int)n;
            v[num].pos = pos[num]; //�ʒu�擾
            v[num].rot = rot[num]; //�p�x�擾
            v[num].scl = scl[num]; //�X�P�[���擾
            v[num].force = force[num]; //3�͎擾
            v[num].owned = owned;
        }
    }
}
