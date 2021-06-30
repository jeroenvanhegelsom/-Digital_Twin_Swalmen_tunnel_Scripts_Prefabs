using UnityEngine;

public class vehicle_spawner : MonoBehaviour
{
    [Header("Vehicle Prefab Objects")]

        [SerializeField, Tooltip("Array of vehicles that this spawner generates")]
        private GameObject[] spawn_vehicles;

    [Header("Spawner Settings")]

        [SerializeField, Tooltip("Minimum time between spawining vehicles [s]")]
        private float t_inter_min = 2;

        [SerializeField, Tooltip("Maximum time between spawining vehicles [s]")]
        private float t_inter_max = 6;

        [SerializeField, Tooltip("Unoccupied distance in front of the spawner needed for spawning a new vehicle [m]")]
        private float min_spawn_dist;

    private float timer;
    private GameObject new_vehicle;
    private float last_dist = 50;
    [HideInInspector]
    public bool SpawnerOn = false;

    private void Start()
    {
        // Set the timer to a random value in the bounds
        timer = Random.Range(t_inter_min, t_inter_max);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        // Determine whether to span a new gameobject
        if (timer < 0 && last_dist > min_spawn_dist && SpawnerOn)
        {
            timer = Random.Range(t_inter_min, t_inter_max);
            Spawn_vehicle();
        }

        // Obtain the distance from the spawner to the vehicle that was spawned last
        // If the spawner is off, set the last distance to enable spawning when it starts again
        if (SpawnerOn)
        {
            last_dist = Vector3.Magnitude(transform.position - new_vehicle.transform.position);
        }
        else
        {
            last_dist = min_spawn_dist + 1;
        }
    }

    private void Spawn_vehicle()
    {
        // Spawn a random vehicle from the vehicles array
        new_vehicle = Instantiate(spawn_vehicles[Random.Range(0, spawn_vehicles.Length)], transform.position, transform.rotation);
        new_vehicle.transform.parent = transform.parent;
    }
}
