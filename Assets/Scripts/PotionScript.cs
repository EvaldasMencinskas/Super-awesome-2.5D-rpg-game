using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{

	public float potionValue = 50f;


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (collider.gameObject.GetComponent<PlayerStats>().Health < collider.gameObject.GetComponent<PlayerStats>().maxHealth)
			{
				//This will recover my player's health for 50
				if (collider.gameObject.GetComponent<PlayerStats>().Health <= collider.gameObject.GetComponent<PlayerStats>().maxHealth - potionValue)
				{
					collider.gameObject.GetComponent<PlayerStats>().Health += potionValue;
					//MaxHealth =100
					//PotionScript = 50
					//Current HealthBar =40+potionValue =90
				}
				else if (collider.gameObject.GetComponent<PlayerStats>().Health > collider.gameObject.GetComponent<PlayerStats>().maxHealth - potionValue)
				{
					//MaxHealth =100
					//PotionScript = 50
					//Current HealthBar =60+potionValue =MaxHealth
					collider.gameObject.GetComponent<PlayerStats>().Health = collider.gameObject.GetComponent<PlayerStats>().maxHealth;
				}
				Destroy(gameObject);
			}
		}

	}

}