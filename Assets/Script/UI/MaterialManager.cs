using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material normalMtl; //�m�[�}���}�e���A��
    public Material activeMtl; //�A�N�V�������}�e���A��
    public Material collisionMtl; //�Փˎ��}�e���A��

    public static Material[] mat = new Material[3];

    // Start is called before the first frame update
    void Start()
    {
        mat[0] = normalMtl;
        mat[1] = activeMtl;
        mat[2] = collisionMtl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
