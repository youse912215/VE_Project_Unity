using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material normalMtl; //ノーマルマテリアル
    public Material activeMtl; //アクション時マテリアル
    public Material collisionMtl; //衝突時マテリアル

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
