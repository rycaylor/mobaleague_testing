using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepMovement : MonoBehaviour {

	public float speed = 5f;

	private float chaseRange = 25.0f;
	private float attackRange = 15.0f;

	private Transform target;
	private int waypointIndex = 0;
	private Transform[] Points;
	public GameObject Path;
//	public CharacterController controller;
	private float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private bool enemyInRange;

	void Start()
	{
		if (Path.GetInstanceID () == GameObject.Find ("RedLeftPath").GetInstanceID ()) {
			Points = RedLeftWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("RedLeftMidPath").GetInstanceID ()) {
			Points = RedMidLWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("RedRightMidPath").GetInstanceID ()) {
			Points = RedMidRWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("RedRightPath").GetInstanceID ()) {
			Points = RedRightWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("BlueLeftPath").GetInstanceID ()) {
			Points = BlueLeftWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("BlueLeftMidPath").GetInstanceID ()) {
			Points = BlueMidLWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("BlueRightMidPath").GetInstanceID ()) {
			Points = BlueMidRWaypoints.points;
		} else if (Path.GetInstanceID () == GameObject.Find ("BlueRightPath").GetInstanceID ()) {
			Points = BlueRightWaypoints.points;
		}

		target = Points [0];

	}
	void Update ()
	{
		if (enemyInRange) {

		} else {	
			Vector3 dir = target.position - transform.position;
			transform.Translate (dir.normalized * speed * Time.deltaTime);

			if (Vector3.Distance (transform.position, target.position) <= 0.2f) {
				GetNextWaypoint ();
			}
		}
	}
	void GetNextWaypoint(){
		waypointIndex++;
		target = Points [waypointIndex];
	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag != this.tag){
			enemyInRange = true;
		}
	}
	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag != this.tag) {
			enemyInRange = false;
		}
	}
}

