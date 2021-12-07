using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAND
{
    public class CreateRandom : MonoBehaviour
    {
        public static int rand;
        private const int RAND_MAX = 1000;
        private const int RAND_MIN = -1000;

        // Start is called before the first frame update
        void Start()
        {
            rand = 0;
            Random.InitState(System.DateTime.Now.Millisecond);
        }

        // Update is called once per frame
        void Update()
        {
            rand = Random.Range(RAND_MIN, RAND_MAX);
        }
    }
}


