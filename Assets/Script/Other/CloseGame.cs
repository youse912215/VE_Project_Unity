using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CloseGame : MonoBehaviour
{
    [SerializeField] private Text endText;
    [SerializeField] private AudioSource clickAudio;

    public void PushCloseButton()
    {
        if (endText.color.a != 1) return;
        clickAudio.PlayOneShot(clickAudio.clip);
        SceneManager.LoadScene("Start");
    }
}
