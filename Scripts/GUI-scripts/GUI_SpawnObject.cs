using UnityEngine;

public class GUI_SpawnObject : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Transform that determines the spawn pose of the object")]
        private Transform TransformForSpawnPose;

        [SerializeField, Tooltip("Object that will be spawned")]
        private GameObject ObjectToSpawn;

    public void Button_SpawnObject()
    {
        Instantiate(ObjectToSpawn, TransformForSpawnPose.position, TransformForSpawnPose.rotation);
    }
}
