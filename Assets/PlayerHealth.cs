using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour {
	public const int maxHealth = 200;

	//[SyncVar(hook="OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage(int amount){
		currentHealth -= amount;
		if(currentHealth <= 0){
			RpcRespawn();
		}
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);

	}

	[ClientRpc]
	void RpcRespawn(){
		if (isLocalPlayer) {
			transform.position = GameObject.Find ("BlueSpawn").transform.position;
			currentHealth = maxHealth;
		}
	}

}
