using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static MaterialManager;

public class RangeCollision : MonoBehaviour
{
    public static bool isCollisionTrigger;

    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        isCollisionTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isCollisionTrigger);

        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;
            ChangeRangeColor(obj, other, mat[2]);
            isCollisionTrigger = true;
	}

    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;
            ChangeRangeColor(obj, other, mat[0]);
            isCollisionTrigger = false;
    }
}
