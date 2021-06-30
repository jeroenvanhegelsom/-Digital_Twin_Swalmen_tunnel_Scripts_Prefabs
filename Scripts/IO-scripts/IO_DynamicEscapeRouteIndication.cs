using System.Collections.Generic;
using UnityEngine;

public class IO_DynamicEscapeRouteIndication: MonoBehaviour
{
    [Header("Outupt logic component names")]

        [SerializeField, Tooltip("The system is off")]
        private string a_off;

        [SerializeField, Tooltip("The route indication is ascending")]
        private string a_ascending;

        [SerializeField, Tooltip("The route indication is descending")]
        private string a_descending;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

    [HideInInspector]
    public Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_off", Static_Functions.FindBoolToggle(a_off));
            IO_dict.Add("a_ascending", Static_Functions.FindBoolToggle(a_ascending));
            IO_dict.Add("a_descending", Static_Functions.FindBoolToggle(a_descending));
    }

    private void Update()
    {
        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }
    }
}
