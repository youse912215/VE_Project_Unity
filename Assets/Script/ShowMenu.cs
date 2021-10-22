using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    //マウスとの差分座標
    private const float diffX = 50.0f;
    private const float diffY = -75.0f;

    public static float a;

    // Start is called before the first frame update
    void Start()
    {
        a = 1;
    }

    // Update is called once per frame
    void Update()
    {



        transform.position = new Vector3(
            ActVirus.mousePos.x + diffX,
            ActVirus.mousePos.y + diffY,
            transform.position.z
        ); 
    }
}
