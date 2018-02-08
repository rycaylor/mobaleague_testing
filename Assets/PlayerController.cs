using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MouseLook))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 1f;

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
	private Animator anim;
	public AudioClip running;
	AudioSource audioSource;

	void Start ()
	{
		motor = GetComponent<MouseLook>();
		anim = GetComponent<Animator>();

		audioSource = GetComponent<AudioSource>();
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
//		Debug.Log(_movHorizontal + " " + _movHorizontal);
		Animating(_xMov, _zMov);
		if (_xMov > 0f || _zMov > 0f && !audioSource.isPlaying ) {
			audioSource.Play ();
		}
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

	

	}

	void Animating (float _xMov, float _zMov)
	{
		bool walking = _xMov > 0f || _zMov > 0f;
		bool backPedal = _xMov < 0f || _zMov < 0f;
		anim.SetBool ("Sprint", walking);
		anim.SetBool ("BackPedal", backPedal);
	}
}