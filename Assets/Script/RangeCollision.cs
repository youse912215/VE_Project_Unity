using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static Call.VirusData;
using static MaterialManager;
using static MouseCollision;

public class RangeCollision : MonoBehaviour
{
    private GameObject obj;
    public static bool[,] currentVirus = new bool[CATEGORY, OWNED];
    public static int count;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;

        //Debug.Log("(0, 0):::" + currentVirus[0,0]);
        //Debug.Log("(0, 1):::" + currentVirus[0,1]);
        //Debug.Log("(1, 0):::" + currentVirus[1,0]);
        //Debug.Log("(1, 1):::" + currentVirus[1,1]);
        //Debug.Log("(2, 0):::" + currentVirus[2,0]);
        //Debug.Log("(2, 1):::" + currentVirus[2,1]);
        //Debug.Log("”ÍˆÍ::" + transform.position);
    }

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;
            obj = other.gameObject;
            ChangeRangeColor(obj, mat[2]);
            isRangeCollision = true;
	}

    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;
            obj = other.gameObject; 
            ChangeRangeColor(obj, mat[0]);
            isRangeCollision = false;
    }
}
