using System.Collections.Generic;
using UnityEngine;

public class IO_AidCabinet_C : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField, Tooltip("The doors of the cabinet are opened")]
        private string s_doorOpen;

        [SerializeField, Tooltip("The doors of the cabinet are closed")]
        private string s_doorClosed;

        [SerializeField, Tooltip("The emergency phone is turned on")]
        private string s_emergencyPhoneOn;

        [SerializeField, Tooltip("The emergency phone is turned off")]
        private string s_emergencyPhoneOff;

        [SerializeField, Tooltip("The hand extinguisher is in use")]
        private string s_handExtinguisherOn;

        [SerializeField, Tooltip("The hand extinguisher is not in use")]
        private string s_handExtinguisherOff;

    [Header("Debugging Value")]

        public bool DoorsOpen, EmergencyPhoneOn, HandExtinguisherOn;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

        // Sensors
        IO_dict.Add("s_doorOpen", Static_Functions.FindBoolToggle(s_doorOpen));
        IO_dict.Add("s_doorClosed", Static_Functions.FindBoolToggle(s_doorClosed));
        IO_dict.Add("s_emergencyPhoneOn", Static_Functions.FindBoolToggle(s_emergencyPhoneOn));
        IO_dict.Add("s_emergencyPhoneOff", Static_Functions.FindBoolToggle(s_emergencyPhoneOff));
        IO_dict.Add("s_handExtinguisherOn", Static_Functions.FindBoolToggle(s_handExtinguisherOn));
        IO_dict.Add("s_handExtinguisherOff", Static_Functions.FindBoolToggle(s_handExtinguisherOff));

    }

    private void Update()
    {

        IO_dict["s_doorOpen"].Boolean = DoorsOpen;
        IO_dict["s_doorClosed"].Boolean = !DoorsOpen;
        IO_dict["s_emergencyPhoneOn"].Boolean = EmergencyPhoneOn;
        IO_dict["s_emergencyPhoneOff"].Boolean = !EmergencyPhoneOn;
        IO_dict["s_handExtinguisherOn"].Boolean = HandExtinguisherOn;
        IO_dict["s_handExtinguisherOff"].Boolean = !HandExtinguisherOn;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.IsChildOf(transform))
                {
                    if (hit.transform.name == "Door")
                    {
                        DoorsOpen = !DoorsOpen;
                    }
                    else if (hit.transform.name == "EmergencyPhone")
                    {
                        EmergencyPhoneOn = !EmergencyPhoneOn;
                    }
                    else if (hit.transform.name == "Extinguisher")
                    {
                        HandExtinguisherOn = !HandExtinguisherOn;
                    }
                }
            }
        }
    }
}
