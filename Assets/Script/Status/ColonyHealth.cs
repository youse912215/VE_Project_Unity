using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static Call.ConstantValue;

public class ColonyHealth : MonoBehaviour
{
    private float maxHp = 5000; //最大HP
    public static float currentHp; //現在のHP
    
    [SerializeField] private Slider slider; //Sliderを入れる
    [SerializeField] private ParticleSystem fire; //炎エフェクト

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Sliderを満タン
        currentHp = maxHp; //現在のHPに最大HPを代入
        fire.transform.localPosition = FIRE_POS;
        fire.transform.localRotation = Quaternion.Euler(FIRE_ROT);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp;

        if (currentHp >= 1000.0f) return;
            fire.Play(); //炎

        if (currentHp > 0.0f) return;
            SceneManager.LoadScene("End");
    }
}
