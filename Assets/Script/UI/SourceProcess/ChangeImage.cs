using UnityEngine;
using UnityEngine.UI;

using static InitSources;
using static SynthesizeVirus;

public class ChangeImage : MonoBehaviour
{ 
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
        img.sprite = infoSp[0];
    }

    // Update is called once per frame
    void Update()
    {
        img.sprite = infoSp[(int)currentCode];
    }
}
