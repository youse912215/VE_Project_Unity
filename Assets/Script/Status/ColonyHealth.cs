using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static Call.ConstantValue;

public class ColonyHealth : MonoBehaviour
{
    private float maxHp = 5000; //�ő�HP
    public static float currentHp; //���݂�HP
    
    [SerializeField] private Slider slider; //Slider������
    [SerializeField] private ParticleSystem fire; //���G�t�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
        fire.transform.localPosition = FIRE_POS;
        fire.transform.localRotation = Quaternion.Euler(FIRE_ROT);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp;

        if (currentHp >= 1000.0f) return;
            fire.Play(); //��

        if (currentHp > 0.0f) return;
            SceneManager.LoadScene("End");
    }
}
