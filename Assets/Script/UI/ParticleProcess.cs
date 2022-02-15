using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProcess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        //Destroy(this.gameObject);
        Debug.Log("destroy");
    }
}
