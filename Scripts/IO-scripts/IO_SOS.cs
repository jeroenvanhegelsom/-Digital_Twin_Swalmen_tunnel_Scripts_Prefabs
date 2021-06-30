using System.Collections.Generic;
using UnityEngine;

public class IO_SOS : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField, Tooltip("A wrong way driver is present in the traffic tube")]
        private string s_wrongwayDriver_on;

        [SerializeField, Tooltip("No wrong way driver is present in the traffic tube")]
        private string s_wrongwayDriver_off;

        [SerializeField, Tooltip("A stationary vehicle is present in the traffic tube")]
        private string s_stationaryVehicle_on;

        [SerializeField, Tooltip("No stationary vehicle is present in the traffic tube")]
        private string s_stationaryVehicle_off;

        [SerializeField, Tooltip("A speeding driver is present in the traffic tube")]
        private string s_speedingDriver_on;

        [SerializeField, Tooltip("No speeding driver is present in the traffic tube")]
        private string s_speedingDriver_off;

    [Header("Spawning components")]

        [SerializeField, Tooltip("Wrong way driver spawner for the traffic tube")]
        private Transform WrongwayDriverSpawner;

        [SerializeField, Tooltip("Still standing car spawner for the traffic tube")]
        private Transform StandingStillSpawner;

        [SerializeField, Tooltip("Speeding driver spawner for the traffic tube")]
        private Transform SpeedingDriverSpawner;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private bool WrongwayDriver, StationaryVehicle, SpeedingDriver;

    private void Start()
    {
        // Filling the I/O dictionary

            // Sensors
            IO_dict.Add("s_wrongwayDriver_on", Static_Functions.FindBoolToggle(s_wrongwayDriver_on));
            IO_dict.Add("s_wrongwayDriver_off", Static_Functions.FindBoolToggle(s_wrongwayDriver_off));
            IO_dict.Add("s_stationaryVehicle_on", Static_Functions.FindBoolToggle(s_stationaryVehicle_on));
            IO_dict.Add("s_stationaryVehicle_off", Static_Functions.FindBoolToggle(s_stationaryVehicle_off));
            IO_dict.Add("s_speedingDriver_on", Static_Functions.FindBoolToggle(s_speedingDriver_on));
            IO_dict.Add("s_speedingDriver_off", Static_Functions.FindBoolToggle(s_speedingDriver_off));
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > 1)
        {
            WrongwayDriver = WrongwayDriverSpawner.childCount > 0;
            StationaryVehicle = StandingStillSpawner.childCount > 0;
            SpeedingDriver = SpeedingDriverSpawner.childCount > 0;

            IO_dict["s_wrongwayDriver_on"].Boolean = WrongwayDriver;
            IO_dict["s_wrongwayDriver_off"].Boolean = !WrongwayDriver;
            IO_dict["s_stationaryVehicle_on"].Boolean = StationaryVehicle;
            IO_dict["s_stationaryVehicle_off"].Boolean = !StationaryVehicle;
            IO_dict["s_speedingDriver_on"].Boolean = SpeedingDriver;
            IO_dict["s_speedingDriver_off"].Boolean = !SpeedingDriver;
        }
    }
}
