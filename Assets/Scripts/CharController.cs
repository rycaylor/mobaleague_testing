using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MouseLook))]
public class CharController : MonoBehaviour {
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1000f;

	[SerializeField]
	private float thrusterFuelBurnSpeed = 1f;
	[SerializeField]
	private float thrusterFuelRegenSpeed = 0.3f;
	private float thrusterFuelAmount = 1f;

	public float GetThrusterFuelAmount ()
	{
		return thrusterFuelAmount;
	}



	// Component caching
	private MouseLook motor;
	private Animator animator;

	void Start ()
	{
		motor = GetComponent<MouseLook>();
		animator = GetComponent<Animator>();

	}

	void Update ()
	{


		if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		//Setting target position for spring
		//This makes the physics act right when it comes to
		//applying gravity when flying over objects

		//Calculate movement velocity as a 3D vector
		float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		// Final movement vector
		Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

		// Animate movement
		animator.SetFloat("ForwardVelocity", _zMov);

		//Apply movement
		motor.Move(_velocity);

		//Calculate rotation as a 3D vector (turning around)
		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		//Apply rotation
		motor.Rotate(_rotation);

		//Calculate camera rotation as a 3D vector (turning around)
		float _xRot = Input.GetAxisRaw("Mouse Y");

		float _cameraRotationX = _xRot * lookSensitivity;

		//Apply camera rotation
		motor.RotateCamera(_cameraRotationX);

		// Calculate the thrusterforce based on player input


		// Apply the thruster force

	}
}