using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using static Call.ConstantValue;

public class PhotoManager : MonoBehaviour
{
    [SerializeField] private RawImage displayImage = null; //��ʃC���[�W
    [SerializeField] private GameObject obj; //�i�[�p�I�u�W�F�N�g
    [SerializeField] private Canvas canvas; //�i�[�p�L�����o�X
    private WebCamTexture webCameraTexture = null; //Web�J�����e�N�X�`��
    private readonly Vector3 IMAGE_POS = new Vector3(-850, 425, 0); //�C���[�W�̈ʒu

    public static GameObject ImageObj = null; //�C���[�W�I�u�W�F�N�g

    private IEnumerator Start()
    {
        if (WebCamTexture.devices.Length == 0) yield break; //�f�o�C�X�̗L�����m�F

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam); //�J�������N�G�X�g
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) yield break; //�J�����̋����m�F

        WebCamDevice userCameraDevice = WebCamTexture.devices[0]; //�J�����f�o�C�X���擾
        webCameraTexture = new WebCamTexture(userCameraDevice.name, (int)WINDOW_SIZE.x, (int)WINDOW_SIZE.y); //�e�N�X�`�����擾
        displayImage.texture = webCameraTexture; //��ʂ̃C���[�W�e�N�X�`���Ɏ擾�����e�N�X�`������
        webCameraTexture.Play(); //�B�e�J�n
    }

    /// <summary>
    /// �B�e�J�n
    /// </summary>
    public void OnPlayWebCamera()
    {
        NullCheck(); //NULL�`�F�b�N
        webCameraTexture.Play(); //�B�e�J�n
    }

    /// <summary>
    /// �B�e��~
    /// </summary>
    public void OnStopWebCamera()
    {
        NullCheck(); //NULL�`�F�b�N
        webCameraTexture.Stop(); //�B�e��~

        //�C���[�W�𕡐����A��A�N�e�B�u��Ԃɂ���
        ImageObj = Instantiate(obj);
        ImageObj.transform.SetParent(canvas.transform, false);
        ImageObj.transform.localPosition = IMAGE_POS;
        ImageObj.SetActive(false);
    }

    /// <summary>
    /// NULL���ǂ����m�F����
    /// </summary>
    private void NullCheck()
    {
        //�J�������F�����Ă��Ȃ��Ƃ��A�������X�L�b�v
        if (webCameraTexture == null) return;
        if (webCameraTexture.isPlaying) return;
    }
}