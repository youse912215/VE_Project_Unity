using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using static Call.ConstantValue;

public class PhotoManager : MonoBehaviour
{
    [SerializeField] private RawImage displayImage = null; //画面イメージ
    [SerializeField] private GameObject obj; //格納用オブジェクト
    [SerializeField] private Canvas canvas; //格納用キャンバス
    private WebCamTexture webCameraTexture = null; //Webカメラテクスチャ
    private readonly Vector3 IMAGE_POS = new Vector3(-850, 425, 0); //イメージの位置

    public static GameObject ImageObj = null; //イメージオブジェクト

    private IEnumerator Start()
    {
        if (WebCamTexture.devices.Length == 0) yield break; //デバイスの有無を確認

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam); //カメラリクエスト
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) yield break; //カメラの許可を確認

        WebCamDevice userCameraDevice = WebCamTexture.devices[0]; //カメラデバイスを取得
        webCameraTexture = new WebCamTexture(userCameraDevice.name, (int)WINDOW_SIZE.x, (int)WINDOW_SIZE.y); //テクスチャを取得
        displayImage.texture = webCameraTexture; //画面のイメージテクスチャに取得したテクスチャを代入
        webCameraTexture.Play(); //撮影開始
    }

    /// <summary>
    /// 撮影開始
    /// </summary>
    public void OnPlayWebCamera()
    {
        NullCheck(); //NULLチェック
        webCameraTexture.Play(); //撮影開始
    }

    /// <summary>
    /// 撮影停止
    /// </summary>
    public void OnStopWebCamera()
    {
        NullCheck(); //NULLチェック
        webCameraTexture.Stop(); //撮影停止

        //イメージを複製し、非アクティブ状態にする
        ImageObj = Instantiate(obj);
        ImageObj.transform.SetParent(canvas.transform, false);
        ImageObj.transform.localPosition = IMAGE_POS;
        ImageObj.SetActive(false);
    }

    /// <summary>
    /// NULLかどうか確認する
    /// </summary>
    private void NullCheck()
    {
        //カメラが認識していないとき、処理をスキップ
        if (webCameraTexture == null) return;
        if (webCameraTexture.isPlaying) return;
    }
}