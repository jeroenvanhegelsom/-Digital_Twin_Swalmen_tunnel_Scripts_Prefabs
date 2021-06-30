using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FreeMovementController : MonoBehaviour
{
	[Header("Settings")]

		[SerializeField, Tooltip("Acceleration of the camera [m/s^2]")]
		private float acceleration = 50;

		[SerializeField, Tooltip("Speed multiplyer when pressing shift")]
		private float accSprintMultiplier = 4;

		[SerializeField, Tooltip("Mouse look sensitivity")]
		private float lookSensitivity = 1;

		[SerializeField, Tooltip("Damping when stopping the movement")]
		private float dampingCoefficient = 5;

		[SerializeField, Tooltip("Whether or not to focus and lock cursor immediately on enable")]
		private bool focusOnEnable = true;

		[SerializeField, Tooltip("Canvas with the crosshair")]
		private GameObject CrosshairCanvas;

	private Vector3 velocity; // current velocity

    private void Start()
    {
		CrosshairCanvas.SetActive(Focused);
	}

    bool Focused
	{
		get => Cursor.lockState == CursorLockMode.Locked;
		set
		{
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = value == false;
		}
	}

	void OnEnable()
	{
		if (focusOnEnable) Focused = true;
	}

	void OnDisable() => Focused = false;

	void Update()
	{
		// Input
		if (Focused)
		{
			UpdateInput();
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			Focused = !Focused;
			CrosshairCanvas.SetActive(Focused);
		}

		// Move
		velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
	}

	void UpdateInput()
	{
		// Position
		velocity += GetAccelerationVector() * Time.deltaTime;

		// Rotation
		Vector2 mouseDelta = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
		transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
		transform.Rotate(Vector3.right, mouseDelta.y, Space.Self);

		// Leave cursor lock
		if (Input.GetKeyDown(KeyCode.Escape))
			Focused = false;
	}

	Vector3 GetAccelerationVector()
	{
		Vector3 moveInput = default;

		void AddMovement(KeyCode key, Vector3 dir)
		{
			if (Input.GetKey(key))
				moveInput += dir;
		}

		AddMovement(KeyCode.W, Vector3.forward);
		AddMovement(KeyCode.S, Vector3.back);
		AddMovement(KeyCode.D, Vector3.right);
		AddMovement(KeyCode.A, Vector3.left);
		AddMovement(KeyCode.Space, Vector3.up);
		AddMovement(KeyCode.LeftControl, Vector3.down);
		Vector3 direction = transform.TransformVector(moveInput.normalized);

		if (Input.GetKey(KeyCode.LeftShift))
			return direction * (acceleration * accSprintMultiplier); // "sprinting"
		return direction * acceleration; // "walking"
	}
}