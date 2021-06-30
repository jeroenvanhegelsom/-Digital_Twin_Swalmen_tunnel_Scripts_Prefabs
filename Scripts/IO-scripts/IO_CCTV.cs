using System.Collections.Generic;
using UnityEngine;

public class IO_CCTV : MonoBehaviour
{
    [Header("Outupt logic component names")]

        [SerializeField, Tooltip("The light is flashing")]
        private string a_surveying;

        [SerializeField, Tooltip("The red light is on")]
        private string a_surveyingCurrent;

        [SerializeField, Tooltip("The green light is on")]
        private string a_direction_with;

        [SerializeField, Tooltip("The lights are all off")]
        private string a_direction_against;

        [SerializeField, Tooltip("The lights are all off")]
        private string a_registration_on;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The light is flashing")]
        private string s_autoSwitch_on;

        [SerializeField, Tooltip("The red light is on")]
        private string s_autoSwitch_off;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private Transform CCTVParent;
        
        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

    [HideInInspector]
    public Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_surveying", Static_Functions.FindBoolToggle(a_surveying));
            IO_dict.Add("a_surveyingCurrent", Static_Functions.FindBoolToggle(a_surveyingCurrent));
            IO_dict.Add("a_direction_with", Static_Functions.FindBoolToggle(a_direction_with));
            IO_dict.Add("a_direction_against", Static_Functions.FindBoolToggle(a_direction_against));
            IO_dict.Add("a_registration_on", Static_Functions.FindBoolToggle(a_registration_on));

            // Sensors
            IO_dict.Add("s_autoSwitch_on", Static_Functions.FindBoolToggle(s_autoSwitch_on));
            IO_dict.Add("s_autoSwitch_off", Static_Functions.FindBoolToggle(s_autoSwitch_off));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }

        IO_dict["s_autoSwitch_on"].Boolean = true;
        IO_dict["s_autoSwitch_off"].Boolean = false;
    }
}
