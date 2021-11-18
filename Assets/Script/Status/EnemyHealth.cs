using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;

public class EnemyHealth : MonoBehaviour
{
    //Å‘åHP‚ÆŒ»İ‚ÌHPB
    float maxHp = 1550;
    float currentHp;
    //Slider‚ğ“ü‚ê‚é
    [SerializeField]
    private Slider slider;

    void Start()
    {   
        slider.value = 1; //Slider‚ğ–ƒ^ƒ“
        currentHp = maxHp; //Œ»İ‚ÌHP‚ÉÅ‘åHP‚ğ‘ã“ü
    }

    void Update()
    {
        if (transform.position.z <= TARGET_POS)
        {
            currentHp -= 5;
            slider.value = currentHp / maxHp; ;
        }
    }
}