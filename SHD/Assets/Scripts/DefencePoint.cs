using UnityEngine;
using System.Collections;

public class DefencePoint : MonoBehaviour
{
	GameManager manager;

	float health = 50;
	
	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager>();
	}

	public void Damage(float d)
	{
		health = health - d;
		
		if (health <= 0)
			Die();
	}

	public void Die()
	{
		PlayerPrefs.SetFloat ("LastScore", manager.points);
		
		Application.LoadLevel (1);
	}
}
