using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public int HP = 10;
	public int ATK = 1;

	public void setHP(int hp) { HP = hp; }      // HPの値を設定
	public int getHP() { return HP; }           // HPの値を返す
	public void setATK(int atk) { ATK = atk; }  // ATKの値を設定
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
		//　見回りまたはキャラクターを追いかける状態
		
			
		}
		
		
		
}
