using System.Collections.Generic;
using UnityEngine;

public class IO_Ventilation : MonoBehaviour
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
        [SerializeField, Tooltip("Ventilation goes with the driving direction")]
        private string a_drivingDirection;
        [SerializeField, Tooltip("Ventilation goes against the driving direction")]
        private string a_againstDrivingDirection;

    [Header("Settings and Components")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

    [Header("Debugging Values")]

        [Tooltip("Current ventilator setting (integer ranging from 0 to 8)")]
        public int VentilatorSetting;

        [Tooltip("Current ventilator direction (true is conform driving direction)")]
        public bool VentilatorDrivingDirection;

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
        IO_dict.Add("a_drivingDirection", Static_Functions.FindBoolToggle(a_drivingDirection));
        IO_dict.Add("a_againstDrivingDirection", Static_Functions.FindBoolToggle(a_againstDrivingDirection));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 2)
        {
            WarningBox.SetActive(true);
        }

        // Set the ventilator settings based on the actuator boolean states
        VentilatorDrivingDirection = IO_dict["a_drivingDirection"].Boolean;

        for (int i = 0; i < 9; i++)
        {
            if (IO_dict["a_s" + i.ToString()].Boolean)
            {
                VentilatorSetting = i;
            }
        }
    }
}
