using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private float count;
    public bool isLoading;

    [SerializeField] private Image loadImage;
    [SerializeField] private Text hintText;

    private List<string> comma = new List<string>{" ", ".", "..", "...", "....", "....."};

    private void Start()
    {
        count = 0.0f;
        isLoading = true;
    }

    // Start is called before the first frame update
    private void Update()
    {
        hintText.text = "Day " + (Scene.DAY + 1).ToString() + comma[(int)count];

        if(isLoading) StartCoroutine(CountLoadTime());

        if (count < 5.0f) return;
        this.GetComponent<CanvasManager>().LoadCanvasEnabled(false);
        StopCoroutine(CountLoadTime());
        count = 0.0f;
        isLoading = false;
        this.GetComponent<SuppliesVirus>().endCoroutine = false;
        
    }

    private IEnumerator CountLoadTime()
    {
        while (isLoading)
        {
            yield return new WaitForSeconds(0.1f);
            loadImage.color += new Color(0.002f, 0.002f, 0.002f, 0);
            count += 0.005f;
        }
        
    }
}
