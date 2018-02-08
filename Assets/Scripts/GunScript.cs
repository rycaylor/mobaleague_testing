using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Collections;



public class GunScript : MonoBehaviour {

	// Use this for initialization
	public Camera fpsCam;
	public GameObject muzzleFLash;
	public GameObject hitEffect;
	public Text AmmoCounterText;
	Animator anim;

	public int Damage = 10;
	public float Range = 100f;
	public float FireRate =  .1f;
	public float fireTimer;
	public int BulletsPerMag = 30;
	public int Magazine = 250;

	public AudioClip impact;
	AudioSource audioSource;

	void Start()
	{
		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		AmmoCounter ();
		if (Input.GetButton ("Fire1")) {
			if (BulletsPerMag <= 0) {} else { audioSource.Play(); Shoot ();  } 
		}
		if (fireTimer < FireRate) {
			fireTimer += Time.deltaTime;
		}
	}

	void AmmoCounter()
	{
		AmmoCounterText.text = "Ammo: " + BulletsPerMag + "/" + Magazine;

	}

	IEnumerator Reload(){
		anim.SetBool ("Reload", true);
		yield return new WaitForSeconds (3);
		if (BulletsPerMag <= 0) {
			if (Magazine <= 0) {
				Magazine = 0;
			} else if (Magazine > 30) {
				Magazine -= 30;
				BulletsPerMag = 30;
			} else {
				BulletsPerMag = Magazine;
				Magazine -= Magazine;
			}
			anim.SetBool ("Reload", false);
		}
	}

	void Shoot(){
		if (fireTimer < FireRate)
			return;
		anim.SetBool("Shoot", true);
		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, Range)) {
			var targetCreep = hit.transform.GetComponent<CreepHealth> ();
			if (targetCreep != null) {
				targetCreep.TakeDamage (Damage);
			}
			var targetPlayer = hit.transform.GetComponent<PlayerHealth> ();
			if (targetPlayer != null) {
				targetPlayer.TakeDamage (Damage);
			}

			GameObject Impact = Instantiate (hitEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (Impact, 0.1f);
			}
		anim.SetBool("Shoot", false);
		fireTimer = 0.0f;
		BulletsPerMag -= 1;
		if (BulletsPerMag <= 0) {
			if (Magazine <= 0) {
				return;
			} else {
				StartCoroutine (Reload ());
				return;
			}
		}
	}
}
