using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;

public class EnemyCollision : MonoBehaviour
{
    private bool isDamageCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("damageColl::" + isDamageCollision);
    }

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "meleeEnemy") return;



        isDamageCollision = true;
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != EnemyTags[0] || other.gameObject.tag != EnemyTags[1]) return;


    }
}
