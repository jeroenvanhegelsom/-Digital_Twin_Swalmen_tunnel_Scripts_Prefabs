using System.Collections.Generic;
using UnityEngine;

public class IO_MainDoor : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField, Tooltip("The door is opened")]
        private string s_open;

        [SerializeField, Tooltip("The door is closed")]
        private string s_closed;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    public bool doorOpen;

    private void Start()
    {
        // Filling the I/O dictionary

            // Sensors
            IO_dict.Add("s_open", Static_Functions.FindBoolToggle(s_open));
            IO_dict.Add("s_closed", Static_Functions.FindBoolToggle(s_closed));
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

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_open"].Boolean = doorOpen;
        IO_dict["s_closed"].Boolean = !doorOpen;
    }
}