using System.Collections.Generic;
using UnityEngine;

public class IO_WaterClean : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("Turn the pump on")]
        private string a_pump_on;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The water level is 'low'")]
        private string s_low_on;

        [SerializeField, Tooltip("The water level is not 'low'")]
        private string s_low_off;

        [SerializeField, Tooltip("The water level is 'startwater'")]
        private string s_start_on;

        [SerializeField, Tooltip("The water level is not 'startwater'")]
        private string s_start_off;

        [SerializeField, Tooltip("The water level is 'max start'")]
        private string s_maxStart_on;

        [SerializeField, Tooltip("The water level is not 'max start'")]
        private string s_maxStart_off;

        [SerializeField, Tooltip("The water level is 'low high'")]
        private string s_lowHigh_on;

        [SerializeField, Tooltip("The water level is not 'low high'")]
        private string s_lowHigh_off;

        [SerializeField, Tooltip("The water level is 'high high'")]
        private string s_highHigh_on;

        [SerializeField, Tooltip("The water level is not 'high high'")]
        private string s_highHigh_off;

    [Header("Settings")]

        [SerializeField, Tooltip("Pumping velocity of the water surface")]
        private float v_pump;

        [SerializeField, Tooltip("Maximum water level height")]
        private float maxHeight;

        [SerializeField, Tooltip("Allowed offset for the range of the water level senor")]
        private float sensorOffset;

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
            IO_dict.Add("s_start_on", Static_Functions.FindBoolToggle(s_start_on));
            IO_dict.Add("s_start_off", Static_Functions.FindBoolToggle(s_start_off));
            IO_dict.Add("s_maxStart_on", Static_Functions.FindBoolToggle(s_maxStart_on));
            IO_dict.Add("s_maxStart_off", Static_Functions.FindBoolToggle(s_maxStart_off));
            IO_dict.Add("s_lowHigh_on", Static_Functions.FindBoolToggle(s_lowHigh_on));
            IO_dict.Add("s_lowHigh_off", Static_Functions.FindBoolToggle(s_lowHigh_off));
            IO_dict.Add("s_highHigh_on", Static_Functions.FindBoolToggle(s_highHigh_on));
            IO_dict.Add("s_highHigh_off", Static_Functions.FindBoolToggle(s_highHigh_off));
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

        Debug.Log(sensedWaterLevel > maxHeight * 1 / 4 + sensorOffset);

        // --- Set the sensor values based on the water level ---
        IO_dict["s_low_on"].Boolean =       sensedWaterLevel > maxHeight * 0 / 4 + sensorOffset;
        IO_dict["s_low_off"].Boolean =      sensedWaterLevel < maxHeight * 0 / 4;
        IO_dict["s_start_on"].Boolean =     sensedWaterLevel > maxHeight * 1 / 4 + sensorOffset;
        IO_dict["s_start_off"].Boolean =    sensedWaterLevel < maxHeight * 1 / 4;
        IO_dict["s_maxStart_on"].Boolean =  sensedWaterLevel > maxHeight * 2 / 4 + sensorOffset;
        IO_dict["s_maxStart_off"].Boolean = sensedWaterLevel < maxHeight * 2 / 4;
        IO_dict["s_lowHigh_on"].Boolean =   sensedWaterLevel > maxHeight * 3 / 4 + sensorOffset;
        IO_dict["s_lowHigh_off"].Boolean =  sensedWaterLevel < maxHeight * 3 / 4;
        IO_dict["s_highHigh_on"].Boolean =  sensedWaterLevel > maxHeight;
        IO_dict["s_highHigh_off"].Boolean = sensedWaterLevel < maxHeight;
    }
}