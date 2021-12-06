using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColonyHealth : MonoBehaviour
{
    //最大HPと現在のHP。
    private float maxHp = 50000;
    public static float currentHp;
    //Sliderを入れる
    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Sliderを満タン
        currentHp = maxHp; //現在のHPに最大HPを代入
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp;

        if (currentHp > 0.0f) return;
            SceneManager.LoadScene("End");
    }
}
