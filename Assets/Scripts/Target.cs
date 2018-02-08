using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	// Use this for initialization
	public float health = 50f;

	public void TakeDamage(float amount){
		health -= amount;
		Debug.Log ("Object health " + health);
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
