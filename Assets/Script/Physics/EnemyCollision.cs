using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static MaterialManager;
using static ActVirus;
using static Call.VirusData;

public class EnemyCollision : MonoBehaviour
{
    private GameObject enemyObj;

    GameObject obj;
    ActVirus actV;
    private float cCount;
    private bool isCollision;
    private const float ACTIVE_COUNT = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj);
        cCount = 0;
        isCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollision) return;
        CountCollisionTime();
    }

    void OnTriggerStay(Collider other) {
        if (actV.isGrabbedVirus) return;
		if (other.gameObject.tag != "Enemy") return;

        ChangeMaterialColor(this.gameObject, rangeMat[3]);
        enemyObj = other.gameObject;
        isCollision = true;

        Debug.Log("Num::" + GetVirusNumber());

        var ps = enemyObj.GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startColor = new Color(255, 255, 255);
        ps.Play();
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Enemy") return;
        ChangeMaterialColor(this.gameObject, rangeMat[0]);
        
    }

    private void CountCollisionTime()
    {
        cCount += 0.1f;

        if (cCount <= ACTIVE_COUNT) return;
        this.gameObject.transform.parent.gameObject.SetActive(false);
        cCount = 0;
        isCollision = false;
    }

    private int GetVirusNumber()
    {
        int n = 0;
        foreach (var (value, index) in VirusTagName.Select((value, index) => (value, index)))
        {
            if (value == this.gameObject.transform.parent.tag)
            {
                n = index;
                break;
            }
        }        
        return n;
    }
}
