using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MaterialManager : MonoBehaviour
{
    public static Material[] rangeMat = new Material[4]; //範囲マテリアル配列
    public static Material[] createMat = new Material[3]; //作成マテリアル配列
    public static Material[] virusMat = new Material[3];

    // Start is called before the first frame update
    void Start()
    {
        //マテリアルを取得
        for (int i = 0; i < rangeMat.Length; ++i)
            rangeMat[i] = Resources.Load<Material>("Material/Range/Range" + i.ToString());
        for (int i = 0; i < createMat.Length; ++i)
            createMat[i] = Resources.Load<Material>("Material/Create/Create" + i.ToString());
        for (int i = 0; i < virusMat.Length; ++i)
            virusMat[i] = Resources.Load<Material>("Material/Virus/Virus" + i.ToString());
    }
}
