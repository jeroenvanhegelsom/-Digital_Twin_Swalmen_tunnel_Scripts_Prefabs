using System.Collections.Generic;
using UnityEngine;

public class IO_EmergencyExit : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField, Tooltip("The door is opened")]
        private string s_open;

        [SerializeField, Tooltip("The door is closed")]
        private string s_closed;

    [Header("Output logic component names")]

        [SerializeField, Tooltip("The sound beacon is muted")]
        private string a_mute_on;

        [SerializeField, Tooltip("The sound beacon is active")]
        private string a_soundBeacon_on;

        [SerializeField, Tooltip("The contour lighting is on")]
        private string a_contourLighting_on;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    public bool doorOpen, soundBeaconOn, muted, coutourLightingOn;

    private void Start()
    {
        // Filling the I/O dictionary

            // Sensors
            IO_dict.Add("s_open", Static_Functions.FindBoolToggle(s_open));
            IO_dict.Add("s_closed", Static_Functions.FindBoolToggle(s_closed));

            // Actuators
            IO_dict.Add("a_mute_on", Static_Functions.FindBoolToggle(a_mute_on));
            IO_dict.Add("a_soundBeacon_on", Static_Functions.FindBoolToggle(a_soundBeacon_on));
            IO_dict.Add("a_contourLighting_on", Static_Functions.FindBoolToggle(a_contourLighting_on));
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.IsChildOf(transform) && hit.transform.name == "Door")
                {
                    doorOpen = !doorOpen;
                }
            }
        }

        // --- Enable events based on the actuator states ---
        muted               = IO_dict["a_mute_on"].Boolean;
        soundBeaconOn       = IO_dict["a_soundBeacon_on"].Boolean;
        coutourLightingOn   = IO_dict["a_contourLighting_on"].Boolean;

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_open"].Boolean = doorOpen;
        IO_dict["s_closed"].Boolean = !doorOpen;
    }
}