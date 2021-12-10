using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static Call.ConstantValue;

public class ColonyHealth : MonoBehaviour
{
    private float maxHp = 5000.0f; //最大HP
    public static float currentHp; //現在のHP
    
    [SerializeField] private Slider slider; //Sliderを入れる
    [SerializeField] private ParticleSystem fire; //炎パーティクル
    private ParticleSystem fireEffect; //炎エフェクト
    private bool isFire; //炎エフェクトフラグ

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Sliderを満タン
        currentHp = maxHp; //現在のHPに最大HPを代入
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp;

        if (currentHp > 1000.0f) return;
        GetFireEffect(); //炎エフェクトを取得

        if (currentHp > 0.0f) return;
            SceneManager.LoadScene("LOSE"); //ゲームオーバー
    }

    /// <summary>
    /// 炎エフェクトを取得
    /// </summary>
    private void GetFireEffect()
    {
        if (isFire) return;
        fireEffect = Instantiate(fire);
        fireEffect.transform.position = FIRE_POS;
        fireEffect.transform.rotation = Quaternion.Euler(FIRE_ROT);
        fireEffect.Play(); //炎
        isFire = true;
    }
}
