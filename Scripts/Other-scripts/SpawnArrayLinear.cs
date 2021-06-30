using UnityEngine;

public class SpawnArrayLinear : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Direction in which the array is spawned")]
        private Vector3 arrayDirection;
        [SerializeField, Tooltip("Number of the components shawned in the array (including the starting component)")]
        private int arrayLength;
        [SerializeField, Tooltip("GameObject that is compied and put in the array")]
        private GameObject arrayObject;
        [SerializeField, Tooltip("Distance between two objects in the array")]
        private float arrayIntervalDistance;

    void Start()
    {
        // Build the array
        for (int i = 1; i < arrayLength; i++) {
            GameObject instantiatedLight = Instantiate(arrayObject, transform.position + i * arrayIntervalDistance * arrayDirection, Quaternion.identity);
            instantiatedLight.transform.parent = transform;
        } 
    }
}
