using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using static VirusMaterialData;

public class DropMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(100);
        
    }

    private void drops(bool isdead)
    {
        if (!isdead) return;

        vMatOwned[GenerateRandom()]++;
        isdead = false;
    }

    private int GenerateRandom()
    {
        int n = UnityEngine.Random.Range(0, 100);

        return n;
    }
}
