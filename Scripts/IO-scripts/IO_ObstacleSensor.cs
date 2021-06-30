using System.Collections.Generic;
using UnityEngine;

public class IO_ObstacleSensor : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField, Tooltip("An obstacle is present in the collider")]
        private string s_obst_on;

        [SerializeField, Tooltip("No obstacle is present in the collider")]
        private string s_obst_off;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private int NumberOfObstaclesInTrigger;

    private void Start()
    {
        // Filling the I/O dictionary

            // Sensors
            IO_dict.Add("s_obst_on", Static_Functions.FindBoolToggle(s_obst_on));
            IO_dict.Add("s_obst_off", Static_Functions.FindBoolToggle(s_obst_off));
    }

    void Update()
    {
        // --- Set the sensor values based on states of the components ---
        IO_dict["s_obst_on"].Boolean = NumberOfObstaclesInTrigger > 0;
        IO_dict["s_obst_off"].Boolean = NumberOfObstaclesInTrigger == 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CarCollider")
        {
            NumberOfObstaclesInTrigger += 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "CarCollider")
        {
            NumberOfObstaclesInTrigger -= 1;
        }
    }
}
