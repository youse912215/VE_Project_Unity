using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonyHealth : MonoBehaviour
{
    //Å‘åHP‚ÆŒ»İ‚ÌHPB
    private float maxHp = 3000;
    public static float currentHp;
    //Slider‚ğ“ü‚ê‚é
    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Slider‚ğ–ƒ^ƒ“
        currentHp = maxHp; //Œ»İ‚ÌHP‚ÉÅ‘åHP‚ğ‘ã“ü
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp; ;
    }
}
