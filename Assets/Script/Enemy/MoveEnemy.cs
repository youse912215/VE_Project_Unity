using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WarriorData;

public class MoveEnemy : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = GetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= 650.0f) return;
        transform.position += new Vector3(0, 0, speed);
    }

    private float GetMoveSpeed()
    {
        float r = (float)Random.Range(10, 50) * 0.05f * MOVE_SPEED;
        return r;
    }
}
