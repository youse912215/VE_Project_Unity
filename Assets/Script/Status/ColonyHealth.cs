using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonyHealth : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP�B
    private float maxHp = 3000;
    public static float currentHp;
    //Slider������
    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp; ;
    }
}
