using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private float count = 0.0f;
    private bool isLoading = true;

    [SerializeField] private Image loadImage;
    [SerializeField] private Text hintText;

    private List<string> comma = new List<string>{" ", ".", "..", "...", "....", "....."};

    private void Start()
    {
        
    }

    // Start is called before the first frame update
    private void Update()
    {
        hintText.text = "Day" + Scene.DAY + " end" + comma[(int)count];

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
            count += 0.01f;
        }
        
    }
}
