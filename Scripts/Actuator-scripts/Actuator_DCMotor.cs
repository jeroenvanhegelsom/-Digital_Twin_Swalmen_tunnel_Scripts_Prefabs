using UnityEngine;
using u040.prespective.referenceobjects.kinetics.motor.dcmotor;

public class Actuator_DCMotor : MonoBehaviour
{
    [Header("Actuation")]

        [SerializeField, Tooltip("Input key for manual debugging input")]
        private KeyCode inputkey;

        [SerializeField, Tooltip("LimitedServoMotor component")]
        public DCMotor Motor;

    [Header("Motor Properties")]

        [SerializeField, Tooltip("Turn manual mode on or off, ALWAYS STARTS FALSE")]
        private bool manualMode = false;

        [Tooltip("Target velocity of the motor [deg/s]")]
        public float targetVelocity;

    [HideInInspector]
    public float currentTargetPLC;
    [HideInInspector]
    private bool motorOnManual = false;

    private void Start()
    {
        // Always disable manual mode on startup
        manualMode = false;
    }

    void Update()
    {
        // Check if manual mode is set
        if (manualMode)
        {
            // Manually switching the motor state
            if (Input.GetKeyDown(inputkey))
            {
                motorOnManual = !motorOnManual;
            }

            if (motorOnManual)
            {
                // Rotate at the target velocity
                Motor.TargetVelocity = targetVelocity;
            }
            else
            {
                // Stop rotating
                Motor.TargetVelocity = 0;
            }
        }
        else
        {
            // Use value coming from the PLC control logic
            Motor.TargetVelocity = currentTargetPLC;
        }
    }
}
