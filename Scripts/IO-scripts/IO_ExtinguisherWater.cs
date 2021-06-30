using System.Collections.Generic;
using UnityEngine;

public class IO_ExtinguisherWater : MonoBehaviour
{

    [Header("Output logic component names")]

        [SerializeField, Tooltip("Turn the pump on")]
        private string a_pump_on;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The water level is low")]
        private string s_low_on;

        [SerializeField, Tooltip("The water level is not low")]
        private string s_low_off;

        [SerializeField, Tooltip("The water level is high")]
        private string s_high_on;

        [SerializeField, Tooltip("The water level is not high")]
        private string s_high_off;

    [Header("Settings")]

        [SerializeField, Tooltip("Pumping velocity of the water surface")]
        private float v_pump;

        [SerializeField, Tooltip("Maximum water level height")]
        private float maxHeight;

        private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
        private float sensedWaterLevel;

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
            transform.position += v_pump * Vector3.down * Time.deltaTime;
        }

        // Determine the water level
        sensedWaterLevel = transform.position.y;

        // --- Set the sensor values based on the water level ---
        IO_dict["s_low_on"].Boolean = sensedWaterLevel > 0.1 * maxHeight;
        IO_dict["s_low_off"].Boolean = sensedWaterLevel <= 0.1 * maxHeight;
        IO_dict["s_high_on"].Boolean = sensedWaterLevel > 0.8 * maxHeight;
        IO_dict["s_high_off"].Boolean = sensedWaterLevel <= 0.8 * maxHeight;
    }
}