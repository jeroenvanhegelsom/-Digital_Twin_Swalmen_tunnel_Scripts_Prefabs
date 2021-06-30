using System.Collections.Generic;
using UnityEngine;

public class IO_ExtinguisherPump : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField, Tooltip("The barrier is fully opened")]
        private string s_on;

        [SerializeField, Tooltip("The barrier is opening")]
        private string s_off;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

        // Sensors
        IO_dict.Add("s_on", Static_Functions.FindBoolToggle(s_on));
        IO_dict.Add("s_off", Static_Functions.FindBoolToggle(s_off));
    }

    private void Update()
    {
        // --- Set the sensor values based on states of the components ---
        IO_dict["s_on"].Boolean = false;
        IO_dict["s_off"].Boolean = true;
    }
}