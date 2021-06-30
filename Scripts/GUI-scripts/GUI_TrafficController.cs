using UnityEngine;
using UnityEngine.UI;

public class GUI_TrafficController : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Vehicle spawners")]
        private vehicle_spawner[] vehicleSpawners;

        [SerializeField, Tooltip("Button text of the traffic toggle button")]
        private Text TrafficToggleButtonText;

    private bool TrafficOn = false;

    public void Button_TrafficToggle()
    {
        TrafficOn = !TrafficOn;

        for (int i = 0; i < vehicleSpawners.Length; i++)
        {
            vehicleSpawners[i].SpawnerOn = TrafficOn;
        }

        if (TrafficOn) {
            TrafficToggleButtonText.text = "Verkeer uit";
        } else {
            TrafficToggleButtonText.text = "Verkeer aan";
        }
    }

    public void Button_DestroyTraffic()
    {
        vehicle_behaviour[] CurrentTraffic = GameObject.FindObjectsOfType<vehicle_behaviour>();

        foreach (vehicle_behaviour vehicle in CurrentTraffic) {
            vehicle.WarpAndDestroyVehicle();
        }
    }
}
