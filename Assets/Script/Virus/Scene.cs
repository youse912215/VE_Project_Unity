using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static WarriorData;

public class Scene : MonoBehaviour
{
    public static int sceneName; //シーン番号

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, false); //画面サイズ
        Application.targetFrameRate = 60; //FPS固定
       // img.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {Application.Quit(); return; }

        sceneName = SceneManager.GetActiveScene().buildIndex; //現在のシーン番号を保存

        if (deadCount <= 10) return;
        SceneManager.LoadScene("End");
    }

    public void startgame()
    {
        SceneManager.LoadScene("Simulation");
    }
}
