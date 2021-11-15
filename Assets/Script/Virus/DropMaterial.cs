using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using static VirusMaterialData;

public class DropMaterial : MonoBehaviour
{
    private int rand;
    private int luckyTime;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(100);
        
    }

    // Update is called once per frame
    void Update()
    {
        //DateTime t = DateTime.Now;
        //luckyTime = (t.Second + t.Minute) % 60 != 0 ? 0 : 1;
        //Debug.Log("•b:" + luckyTime);
        //Debug.Log("ƒ‰ƒ“ƒ_ƒ€" + GenerateRandom());

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
