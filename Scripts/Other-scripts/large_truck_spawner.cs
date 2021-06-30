using UnityEngine;

public class large_truck_spawner : MonoBehaviour
{
    [Header("Spawner Settings")]

        [SerializeField, Tooltip("Large truck object")]
        private GameObject truck_large_high;

    private GameObject new_vehicle;

    public void SpawnLargeTruck()
    {
        new_vehicle = Instantiate(truck_large_high, transform.position, transform.rotation);
        new_vehicle.transform.parent = transform.parent;
    }
}
