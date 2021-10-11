using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;

public class ActVirus : MonoBehaviour
{
    Virus[] virus = new Virus[num];

    // Start is called before the first frame update
    void Start()
    {
        setValue(virus, CODE_CLD);
        setValue(virus, CODE_INF);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("COLD::" + virus[0].force);
        Debug.Log("INF::" + virus[1].force);
    }
}
