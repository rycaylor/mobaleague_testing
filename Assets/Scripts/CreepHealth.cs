using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreepHealth : MonoBehaviour {

	public const int maxHealth = 100;

	//[SyncVar(hook="OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage(int amount){
		currentHealth -= amount;
		if(currentHealth <= 0){
			Destroy (gameObject);
		}
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);

	}
		


}
