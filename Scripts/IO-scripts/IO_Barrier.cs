using System.Collections.Generic;
using UnityEngine;

public class IO_Barrier : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("No instruction is given")]
        private string a_noChoice;

        [SerializeField, Tooltip("Open the barrier")]
        private string a_open;

        [SerializeField, Tooltip("Close the barrier")]
        private string a_close;

        [SerializeField, Tooltip("Stop moving the barrier")]
        private string a_stop;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The barrier is fully opened")]
        private string s_opened;

        [SerializeField, Tooltip("The barrier is opening")]
        private string s_opening;

        [SerializeField, Tooltip("The barrier has stopped moving")]
        private string s_stopped;

        [SerializeField, Tooltip("The barrier is closing")]
        private string s_closing;

        [SerializeField, Tooltip("The barrier is fully closed")]
        private string s_closed;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Collider that prevents vehicles from driving through the barrier")]
        private GameObject BarrierCollider;

        [SerializeField, Tooltip("Accepted offset of the motor position sensing [deg]")]
        private float SensorOffsetPos;

    private Actuator_RotationalMotor motor;
    private Vector3 colliderStartPosition;

    [HideInInspector]
    public Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        colliderStartPosition = BarrierCollider.transform.position;

        // Components to communicate
        motor = GetComponentInChildren<Actuator_RotationalMotor>();

        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_noChoice", Static_Functions.FindBoolToggle(a_noChoice));
            IO_dict.Add("a_open", Static_Functions.FindBoolToggle(a_open));
            IO_dict.Add("a_close", Static_Functions.FindBoolToggle(a_close));
            IO_dict.Add("a_stop", Static_Functions.FindBoolToggle(a_stop));

            // Sensors
            IO_dict.Add("s_opened", Static_Functions.FindBoolToggle(s_opened));
            IO_dict.Add("s_opening", Static_Functions.FindBoolToggle(s_opening));
            IO_dict.Add("s_stopped", Static_Functions.FindBoolToggle(s_stopped));
            IO_dict.Add("s_closing", Static_Functions.FindBoolToggle(s_closing));
            IO_dict.Add("s_closed", Static_Functions.FindBoolToggle(s_closed));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }

        // --- Enable events based on the actuator states ---
        if (IO_dict["a_noChoice"].Boolean)
        {
            // Do nothing
        }
        else if (IO_dict["a_open"].Boolean)
        {
            motor.RotationDirection = 1;
        }
        else if (IO_dict["a_close"].Boolean)
        {
            motor.RotationDirection = -1;
        }
        else if (IO_dict["a_stop"].Boolean)
        {
            motor.RotationDirection = 0;
        }
        else
        {
            // Default state
            motor.RotationDirection = 0;
        }

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_opened"].Boolean = 
            Mathf.Abs(motor.OpenRotation - motor.currentRotation) < SensorOffsetPos;
        IO_dict["s_opening"].Boolean =
            Mathf.Abs(motor.ClosedRotation - motor.currentRotation) < SensorOffsetPos;
        IO_dict["s_stopped"].Boolean =
            motor.RotationDirection == 0;
        IO_dict["s_closing"].Boolean =
            motor.RotationDirection == 1;
        IO_dict["s_closed"].Boolean =
            motor.RotationDirection == -1;

        // Move the collider out of the way when the barrier is opened
        if (IO_dict["s_opened"].Boolean)
        {
            BarrierCollider.transform.position = Vector3.Lerp(transform.position, colliderStartPosition + Vector3.up * 20, Time.deltaTime * 100);
        }
        else
        {
            BarrierCollider.transform.position = Vector3.Lerp(transform.position, colliderStartPosition, Time.deltaTime * 100);
        }
    }
}