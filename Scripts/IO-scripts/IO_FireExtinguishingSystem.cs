using System.Collections.Generic;
using UnityEngine;

public class IO_FireExtinguishingSystem : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("The pump is on")]
        private string a_pump_on;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The water level in the bassin is low")]
        private string s_low_on;

        [SerializeField, Tooltip("The water level in the bassin is not low")]
        private string s_low_off;

        [SerializeField, Tooltip("The water level in the bassin is high")]
        private string s_high_on;

        [SerializeField, Tooltip("The water level in the bassin is not high")]
        private string s_high_off;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_pump_on", Static_Functions.FindBoolToggle(a_pump_on));
           
            // Sensors
            IO_dict.Add("s_low_on", Static_Functions.FindBoolToggle(s_low_on));
            IO_dict.Add("s_low_off", Static_Functions.FindBoolToggle(s_low_off));
            IO_dict.Add("s_high_on", Static_Functions.FindBoolToggle(s_high_on));
            IO_dict.Add("s_high_off", Static_Functions.FindBoolToggle(s_high_off));
    }

    private void Update()
    {
        // --- Enable events based on the actuator states ---
        if (IO_dict["a_pump_on"].Boolean)
        {
            ;
        }
        else
        {
            ;
        }

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_low_on"].Boolean = true;
        IO_dict["s_low_off"].Boolean = false;
        IO_dict["s_high_on"].Boolean = false;
        IO_dict["s_high_off"].Boolean = true;
    }
}