using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.VirusData;

public class EnemyHealth : MonoBehaviour
{
    //Å‘åHP‚ÆŒ»İ‚ÌHPB
    private float maxHp = 2000;
    private float currentHp;
    //Slider‚ğ“ü‚ê‚é
    [SerializeField]
    private Slider slider;

    public bool isInfection;
    public uint damage;
    public float total;

    void Start()
    {   
        slider.value = 1; //Slider‚ğ–ƒ^ƒ“
        currentHp = maxHp; //Œ»İ‚ÌHP‚ÉÅ‘åHP‚ğ‘ã“ü
        damage = 0b0000;
        total = 0.0f;
    }

    void Update()
    {
        if (transform.position.z <= TARGET_POS) ColonyHealth.currentHp -= 0.5f;

        if (!isInfection) return;

        currentHp -= total;
        slider.value = currentHp / maxHp;
    }

    public int CulculationHealth(uint damage)
    {
        uint d0 = ((damage & 0b0001) >> 0) * 1;
        uint d1 = ((damage & 0b0010) >> 1) * 2;
        uint d2 = ((damage & 0b0100) >> 2) * 3;
        return (int)(d0 + d1 + d2);
    }
}