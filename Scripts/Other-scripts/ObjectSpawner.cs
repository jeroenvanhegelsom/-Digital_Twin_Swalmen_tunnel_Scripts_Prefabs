using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]

        [SerializeField, Tooltip("Object to spawn")]
        private GameObject ObjectToInstantiate;

        [SerializeField, Tooltip("Whether to destroy the previously spawned object when spawning a new object")]
        private bool DestroyPreviousObject = false;

    private GameObject SpawnedObject;
    private bool CanSpawn;
    private float timer = 2;

    private void Update()
    {
        if (!CanSpawn)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                CanSpawn = true;
                timer = 2;
            }
        }
        
    }

    public void SpawnObject()
    {
        if (CanSpawn)
        {
            if (DestroyPreviousObject)
            {
                Destroy(SpawnedObject);
                SpawnedObject = Instantiate(ObjectToInstantiate, transform.position, transform.rotation);
                SpawnedObject.transform.parent = transform;
            }
            else
            {
                SpawnedObject = Instantiate(ObjectToInstantiate, transform.position, transform.rotation);
                SpawnedObject.transform.parent = transform;
            }
        }
    }
}
