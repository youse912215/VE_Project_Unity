using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSources : MonoBehaviour
{
    public static Sprite[] infoSp = new Sprite[8];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < infoSp.Length; ++i)
            infoSp[i] = Resources.Load<Sprite>("Image/Information" + i.ToString());
    }
}
