using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
	Image img;
	private readonly Vector4 ToColor = new Color(0.7f, 0f, 0f, 0.7f);
	private readonly Color IMG_COLOR = new Color(0.5f, 0f, 0f, 0.01f);
	private const float DEAD_HEALTH = 1000.0f; //���S���C���t�߂�HP

	void Start()
	{
		img = GetComponent<Image>(); //�R���|�[�l���g���擾
		img.color = Color.clear; //�����ɂ���
		this.img.color = IMG_COLOR; //�C���[�W�J���[�ɕύX
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Space)) ColonyHealth.currentHp += 500.0f;

		//DEAD_HEALTH�ȉ��Ȃ��ʂ����X�ɐԂ�����
		if (ColonyHealth.currentHp <= DEAD_HEALTH)
			this.img.color = Color.Lerp(this.img.color, ToColor, Time.deltaTime);
		else this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
	}
}