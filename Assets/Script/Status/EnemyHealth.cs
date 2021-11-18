using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;

public class EnemyHealth : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    float maxHp = 1550;
    float currentHp;
    //Slider������
    [SerializeField]
    private Slider slider;

    void Start()
    {   
        slider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
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