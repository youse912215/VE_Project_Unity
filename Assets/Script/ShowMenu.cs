using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;

public class ShowMenu : MonoBehaviour
{
    public static Vector3 mousePos; //�}�E�X���W
    public static Camera cam; //�J�����I�u�W�F�N�g

    public static bool isFeelVirus;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //���C���J�����擾
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            mousePos.x + DIFF_X,
            mousePos.y + DIFF_Y,
            transform.position.z
        ); //���j���[�̈ʒu���}�E�X�̉E���ɔz�u
    }

    /// <summary>
    /// �J�����̈ʒu���擾
    /// </summary>
    public static void GetCameraPos()
    {
        mousePos = Input.mousePosition; //�}�E�X�̍��W���擾
        mousePos.z = CAM_DISTANCE; //�}�E�Xz���W�ɃJ�����Ƃ̋�������
    }

    /// <summary>
    /// �X�N���[����̃}�E�X���W��Ԃ�
    /// </summary>
    public static Vector3 ReturnOnScreenMousePos()
    {
        GetCameraPos(); //�J�����ʒu�擾
        return cam.ScreenToWorldPoint(mousePos); //�X�N���[�������[���h�ϊ�
    }
}
