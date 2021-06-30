using System.Collections.Generic;
using UnityEngine;

public class IO_TrafficTubeControl : MonoBehaviour
{
    [Header("Input logic component names")]
    
        [SerializeField, Tooltip("All traffic lights in the tube are off")]
        private string s_trafficlights_off;

        [SerializeField, Tooltip("All traffic lights in the tube are flashing")]
        private string s_trafficlights_flashing;

        [SerializeField, Tooltip("All traffic lights in the tube are red")]
        private string s_trafficlights_red;

        [SerializeField, Tooltip("All barriers in the tube are opened")]
        private string s_boomBarriers_bothOpened;

        [SerializeField, Tooltip("All barriers in the tube are opening")]
        private string s_boomBarriers_bothOpening;

        [SerializeField, Tooltip("All barriers in the tube have stopped")]
        private string s_boomBarriers_bothStopped;

        [SerializeField, Tooltip("All barriers in the tube are closing")]
        private string s_boomBarriers_bothClosing;

        [SerializeField, Tooltip("All barriers in the tube are closed")]
        private string s_boomBarriers_bothClosed;

    [Header("Component inputs")]

        [SerializeField, Tooltip("Traffic light 1 in the traffic tube")]
        private IO_TrafficLight TrafficLight1;

        [SerializeField, Tooltip("Traffic light 2 in the traffic tube")]
        private IO_TrafficLight TrafficLight2;

        [SerializeField, Tooltip("Barrier 1 in the traffic tube")]
        private IO_Barrier Barrier1;

        [SerializeField, Tooltip("Barrier 2 in the traffic tube")]
        private IO_Barrier Barrier2;

    private  Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Sensors
            IO_dict.Add("s_trafficlights_off", Static_Functions.FindBoolToggle(s_trafficlights_off));
            IO_dict.Add("s_trafficlights_flashing", Static_Functions.FindBoolToggle(s_trafficlights_flashing));
            IO_dict.Add("s_trafficlights_red", Static_Functions.FindBoolToggle(s_trafficlights_red));
            IO_dict.Add("s_boomBarriers_bothOpened", Static_Functions.FindBoolToggle(s_boomBarriers_bothOpened));
            IO_dict.Add("s_boomBarriers_bothOpening", Static_Functions.FindBoolToggle(s_boomBarriers_bothOpening));
            IO_dict.Add("s_boomBarriers_bothStopped", Static_Functions.FindBoolToggle(s_boomBarriers_bothStopped));
            IO_dict.Add("s_boomBarriers_bothClosing", Static_Functions.FindBoolToggle(s_boomBarriers_bothClosing));
            IO_dict.Add("s_boomBarriers_bothClosed", Static_Functions.FindBoolToggle(s_boomBarriers_bothClosed));
    }

    private void Update()
    {
        // --- Set the sensor values based on states of the components ---
        IO_dict["s_trafficlights_off"].Boolean = 
               (TrafficLight1.TrafficlightState == IO_TrafficLight.TrafficlightStates.off)
            && (TrafficLight2.TrafficlightState == IO_TrafficLight.TrafficlightStates.off);
        IO_dict["s_trafficlights_flashing"].Boolean =
               (TrafficLight1.TrafficlightState == IO_TrafficLight.TrafficlightStates.flashing)
            && (TrafficLight2.TrafficlightState == IO_TrafficLight.TrafficlightStates.flashing);
        IO_dict["s_trafficlights_red"].Boolean =
               (TrafficLight1.TrafficlightState == IO_TrafficLight.TrafficlightStates.red)
            && (TrafficLight2.TrafficlightState == IO_TrafficLight.TrafficlightStates.red);

        IO_dict["s_boomBarriers_bothOpened"].Boolean = (Barrier1.IO_dict["s_opened"] && Barrier2.IO_dict["s_opened"]);
        IO_dict["s_boomBarriers_bothOpening"].Boolean = (Barrier1.IO_dict["s_opening"] && Barrier2.IO_dict["s_opening"]);
        IO_dict["s_boomBarriers_bothStopped"].Boolean = (Barrier1.IO_dict["s_stopped"] && Barrier2.IO_dict["s_stopped"]);
        IO_dict["s_boomBarriers_bothClosing"].Boolean = (Barrier1.IO_dict["s_closing"] && Barrier2.IO_dict["s_closing"]);
        IO_dict["s_boomBarriers_bothClosed"].Boolean = (Barrier1.IO_dict["s_closed"] && Barrier2.IO_dict["s_closed"]);
    }
}