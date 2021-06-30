using UnityEngine;

public class Actuator_RotationalMotor : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Rotational velocity [deg/s] around local x-axis")]
        private float rotVel;

        [Tooltip("Rotation in the opened state [deg]")]
        public float OpenRotation;

        [Tooltip("Rotation in the closed state [deg]")]
        public float ClosedRotation;

        [Tooltip("True: initial state is opened")]
        public bool InitialOpen;

        [Tooltip("Horizontal direction vector")]
        public Vector3 horizontalDir;

    // Rotation direction, 1: forward, 0: stopped, -1:backward
    [HideInInspector]
    public int RotationDirection = 0;
    //[HideInInspector]
    public float currentRotation;
    private Vector3 SignedAngleAxis;

    private void Start()
    {
        SignedAngleAxis = Vector3.Cross(horizontalDir, transform.forward);
    }

    private void FixedUpdate()
    {
        currentRotation = Vector3.SignedAngle(horizontalDir, transform.forward, SignedAngleAxis);

        if (RotationDirection == 1 && currentRotation < OpenRotation)
        {
            transform.Rotate(Vector3.right * rotVel * RotationDirection * Time.fixedDeltaTime, Space.Self);
        }
        else if(RotationDirection == -1 && currentRotation > ClosedRotation)
        {
            transform.Rotate(Vector3.right * rotVel * RotationDirection * Time.fixedDeltaTime, Space.Self);
        }

    }
}
