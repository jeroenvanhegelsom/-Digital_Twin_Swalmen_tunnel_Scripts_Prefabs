using System.Collections.Generic;
using UnityEngine;

public class IO_TunnelLight : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField]
        private string a_s0;
        [SerializeField]
        private string a_s1;
        [SerializeField]
        private string a_s2;
        [SerializeField]
        private string a_s3;
        [SerializeField]
        private string a_s4;
        [SerializeField]
        private string a_s5;
        [SerializeField]
        private string a_s6;
        [SerializeField]
        private string a_s7;
        [SerializeField]
        private string a_s8;

    [Header("Settings and Components")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

    [Header("Debugging Values")]

        [Tooltip("Current light setting (integer ranging from 0 to 8)")]
        public int Lightsetting;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_s0", Static_Functions.FindBoolToggle(a_s0));
            IO_dict.Add("a_s1", Static_Functions.FindBoolToggle(a_s1));
            IO_dict.Add("a_s2", Static_Functions.FindBoolToggle(a_s2));
            IO_dict.Add("a_s3", Static_Functions.FindBoolToggle(a_s3));
            IO_dict.Add("a_s4", Static_Functions.FindBoolToggle(a_s4));
            IO_dict.Add("a_s5", Static_Functions.FindBoolToggle(a_s5));
            IO_dict.Add("a_s6", Static_Functions.FindBoolToggle(a_s6));
            IO_dict.Add("a_s7", Static_Functions.FindBoolToggle(a_s7));
            IO_dict.Add("a_s8", Static_Functions.FindBoolToggle(a_s8));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }

        // Set the light setting based on the actuator boolean states
        for (int i = 0; i < 9; i++)
        {
            if (IO_dict["a_s" + i.ToString()].Boolean)
            {
                Lightsetting = i;
            }
        }
    }
}
