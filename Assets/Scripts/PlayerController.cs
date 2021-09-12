using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RadialGravity))]
public class PlayerController : MonoBehaviour
{
	Rigidbody rb;
	RadialGravity rg;
	float speed = 10.0f;
	float hSpeed = 50.0f;

	float targetVertAngle = 0.0f;


	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rg = GetComponent<RadialGravity>();
	}

	private void Update()
	{
		handleMouseLocking();
		handleRotationLockAndMovement();

		handleRotation();
	}

	private void handleMovement(Vector3 up, Vector3 inFront)
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
		transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
	}

	private void handleRotation()
	{
		transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * hSpeed, Space.Self);

		var camObj = transform.GetChild(0);

		float currAngle = camObj.transform.rotation.eulerAngles.x;

		if (currAngle > 90) currAngle -= 360;

		targetVertAngle = Mathf.Clamp(targetVertAngle - Input.GetAxis("Mouse Y") * Time.deltaTime * hSpeed, -90, 90);

		camObj.transform.localRotation = Quaternion.Lerp(camObj.transform.localRotation, Quaternion.Euler(targetVertAngle, 0, 0), 0.6f);
	}

	private void handleRotationLockAndMovement()
	{
		GameObject mainSource = rg.MainSource();
		Vector3 up = transform.position - mainSource.transform.position;
		Vector3 upNormalized = up.normalized;
		Vector3 forward = Vector3.Cross(transform.right, up);
		Vector3 inFront = (forward + up);


		transform.LookAt(inFront, upNormalized);

		handleMovement(up, inFront);

	}

	private static void handleMouseLocking()
	{
		if (Input.GetMouseButton(0))
			Cursor.lockState = CursorLockMode.Locked;

		if (Input.GetKey(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
	}
}
