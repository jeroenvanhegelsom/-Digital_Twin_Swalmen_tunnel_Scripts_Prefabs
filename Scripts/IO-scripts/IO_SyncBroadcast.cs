using System.Collections.Generic;
using UnityEngine;

public class IO_SyncBroadcast : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("The broadcast is off")]
        private string a_off;

        [SerializeField, Tooltip("The broadcast is being reset")]
        private string a_reset;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The timer has timed out")]
        private string s_timeout;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Duration of the timer")]
        private float timerDuration;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private bool timerStarted = false;
    private float timer;

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_off", Static_Functions.FindBoolToggle(a_off));
            IO_dict.Add("a_reset", Static_Functions.FindBoolToggle(a_reset));

            // Sensors
            IO_dict.Add("s_timeout", Static_Functions.FindBoolToggle(s_timeout));
    }

    private void Update()
    {
        if (timerStarted)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timerStarted = false;
            }

        }

        // Check whether there are conflicting output signals
        if (IO_dict["a_off"].Boolean && IO_dict["a_reset"].Boolean)
        {
            WarningBox.SetActive(true);
        }

        // --- Enable events based on the actuator states ---
        if (IO_dict["a_off"].Boolean)
        {
            ;
        }
        else if (IO_dict["a_reset"].Boolean)
        {
            ;

        }
        else
        {
            ;
        }

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_timeout"].Boolean = !timerStarted;
    }
}