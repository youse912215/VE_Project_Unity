using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public int HP = 10;
	public int ATK = 1;

	public void setHP(int hp) { HP = hp; }      // HP�̒l��ݒ�
	public int getHP() { return HP; }           // HP�̒l��Ԃ�
	public void setATK(int atk) { ATK = atk; }  // ATK�̒l��ݒ�
	public int getATK() { return ATK; }
	

	public void damage(EnemyAttack status)
	{
		HP = Mathf.Max(0, HP - status.getATK());
	}
		// Start is called before the first frame update
		void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		//�@�����܂��̓L�����N�^�[��ǂ���������
		
			
		}
		
		
		
}
