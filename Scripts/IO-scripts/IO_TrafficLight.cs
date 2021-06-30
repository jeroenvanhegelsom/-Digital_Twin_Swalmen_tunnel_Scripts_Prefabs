using System.Collections.Generic;
using UnityEngine;

public class IO_TrafficLight : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("No instruction is given")]
        private string a_noChoice;

        [SerializeField, Tooltip("Flashing yellow light")]
        private string a_flashing;

        [SerializeField, Tooltip("Red light")]
        private string a_red;

        [SerializeField, Tooltip("Green light")]
        private string a_green;

        [SerializeField, Tooltip("No lights on")]
        private string a_off;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The light is flashing")]
        private string s_flashing;

        [SerializeField, Tooltip("The red light is on")]
        private string s_red;

        [SerializeField, Tooltip("The green light is on")]
        private string s_green;

        [SerializeField, Tooltip("The lights are all off")]
        private string s_off;

    [Header("Settings")]

        [SerializeField, Tooltip("Collider that lets people stop in front of the red light")]
        private GameObject VehicleStopCollider;

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Frequency for the yellow flickering light setting [Hz]")]
        private float flicker_frequency;

    [Header("Indicator Light Components")]

        [SerializeField, Tooltip("Red indicator light")]
        private LightOnOff Light_Red;

        [SerializeField, Tooltip("Yellow indicator light")]
        private LightOnOff Light_Yellow;

        [SerializeField, Tooltip("Green indicator light")]
        private LightOnOff Light_Green;

        [Tooltip("Initial state of the traffic light")]
        public TrafficlightStates TrafficlightState;

    // Flashing light variables
    private bool flashing_timer_bool;
    private float timer;

    // BarrierStates enumeration
    public enum TrafficlightStates { flashing, red, green, off };

    private Vector3 colliderStartPosition;
    [HideInInspector]
    public Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        colliderStartPosition = VehicleStopCollider.transform.position;

        // Filling the I/O dictionary

        // Actuators
            IO_dict.Add("a_noChoice", Static_Functions.FindBoolToggle(a_noChoice));
            IO_dict.Add("a_flashing", Static_Functions.FindBoolToggle(a_flashing));
            IO_dict.Add("a_red", Static_Functions.FindBoolToggle(a_red));
            IO_dict.Add("a_green", Static_Functions.FindBoolToggle(a_green));
            IO_dict.Add("a_off", Static_Functions.FindBoolToggle(a_off));

            // Sensors
            IO_dict.Add("s_flashing", Static_Functions.FindBoolToggle(s_flashing));
            IO_dict.Add("s_red", Static_Functions.FindBoolToggle(s_red));
            IO_dict.Add("s_green", Static_Functions.FindBoolToggle(s_green));
            IO_dict.Add("s_off", Static_Functions.FindBoolToggle(s_off));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }

        // --- Enable certain events based on the actuator states ---
        if (IO_dict["a_noChoice"].Boolean)
        {
            // Keep the current state
        }
        else if (IO_dict["a_flashing"].Boolean)
        {
            TrafficlightState = TrafficlightStates.flashing;
        }
        else if (IO_dict["a_red"].Boolean)
        {
            TrafficlightState = TrafficlightStates.red;
        } 
        else if (IO_dict["a_green"].Boolean)
        {
            TrafficlightState = TrafficlightStates.green;
        }
        else if (IO_dict["a_off"].Boolean)
        {
            TrafficlightState = TrafficlightStates.off;
        }

        // --- Determine the behaviour ---

        if (TrafficlightState == TrafficlightStates.flashing)
        {
            // A boolean is used which changes every time the timer runs out
            // which creates the flashing light effect
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                flashing_timer_bool = !flashing_timer_bool;
                timer = 1/flicker_frequency;

                Light_Red.LightOn = false;
                Light_Yellow.LightOn = flashing_timer_bool;
                Light_Green.LightOn = false;
            }
        } 
        else if (TrafficlightState == TrafficlightStates.green)
        {
            Light_Red.LightOn = false;
            Light_Yellow.LightOn = false;
            Light_Green.LightOn = true;
        }
        else if (TrafficlightState == TrafficlightStates.red)
        {
            Light_Red.LightOn = true;
            Light_Yellow.LightOn = false;
            Light_Green.LightOn = false;
        }
        else if (TrafficlightState == TrafficlightStates.off)
        {
            Light_Red.LightOn = false;
            Light_Yellow.LightOn = false;
            Light_Green.LightOn = false;
        }

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_flashing"].Boolean   = (TrafficlightState == TrafficlightStates.flashing);
        IO_dict["s_red"].Boolean        = (TrafficlightState == TrafficlightStates.red);
        IO_dict["s_green"].Boolean      = (TrafficlightState == TrafficlightStates.green);
        IO_dict["s_off"].Boolean        = (TrafficlightState == TrafficlightStates.off);

        // Move the collider onto the road when the light is red
        if (TrafficlightState == TrafficlightStates.red)
        {
            VehicleStopCollider.transform.position = Vector3.Lerp(transform.position, colliderStartPosition, Time.deltaTime * 100);
        }
        else
        {
            VehicleStopCollider.transform.position = Vector3.Lerp(transform.position, colliderStartPosition + Vector3.up * 20, Time.deltaTime * 100);
        }
    }
}