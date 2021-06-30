using UnityEngine;
using u040.prespective.referenceobjects.kinetics.motor.servomotor;

public class Actuator_LimitedServoMotor : MonoBehaviour
{
    [Header("Actuation")]

        [SerializeField, Tooltip("Input key for manual debugging input")]
        private KeyCode inputkey;

        [SerializeField, Tooltip("LimitedServoMotor component")]
        public LimitedServoMotor LimitedServoMotor;

    [Header("Motor Properties")]

        [SerializeField, Tooltip("Turn manual mode on or off, ALWAYS STARTS FALSE")]
        private bool manualMode = false;

        [Tooltip("Target rotation [deg] (can be different from 0 and can be determined by trial and error)")]
        public float targetRotation;

        [Tooltip("Offset of the motor rotation [deg] (determined by trial and error)")]
        public float motorOffset;

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
                // Rotate to the target position
                LimitedServoMotor.Target = targetRotation + motorOffset;
            }
            else
            {
                // Rotate to the starting position
                LimitedServoMotor.Target = motorOffset;
            }
        }
        else
        {
            // Use value coming from the PLC control logic
            LimitedServoMotor.Target = currentTargetPLC + motorOffset;
        }
    }
}
