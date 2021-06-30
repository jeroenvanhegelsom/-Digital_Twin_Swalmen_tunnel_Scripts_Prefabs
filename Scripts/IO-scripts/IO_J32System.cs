using System.Collections.Generic;
using UnityEngine;

public class IO_J32System : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("The sign is on")]
        private string a_on;

    [Header("Debugging value")]

        [Tooltip("Whether the system is on or off")]
        public bool J32SystemOn = false;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_on", Static_Functions.FindBoolToggle(a_on));
    }

    private void Update()
    {
        // --- Enable events based on the actuator states ---
        J32SystemOn = IO_dict["a_on"].Boolean;
    }
}