using System.Collections.Generic;
using UnityEngine;

public class IO_Overpressure : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("No overpressure")]
        private string a_off;

        [SerializeField, Tooltip("Overpressure to the left traffic tube")]
        private string a_left;

        [SerializeField, Tooltip("Overpressure to the right traffic tube")]
        private string a_right;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Box representing overpressure to the left traffic tube")]
        private GameObject OverpressureIndicatorLeft;

        [SerializeField, Tooltip("Box representing overpressure to the right traffic tube")]
        private GameObject OverpressureIndicatorRight;

    [HideInInspector]
    public Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_off", Static_Functions.FindBoolToggle(a_off));
            IO_dict.Add("a_left", Static_Functions.FindBoolToggle(a_left));
            IO_dict.Add("a_right", Static_Functions.FindBoolToggle(a_right));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }

        OverpressureIndicatorLeft.SetActive(IO_dict["a_left"].Boolean);
        OverpressureIndicatorRight.SetActive(IO_dict["a_right"].Boolean);
    }
}