using UnityEngine;
using System.Collections;

public class vehicle_behaviour : MonoBehaviour
{

    [Header("Driving Properties")]

        [SerializeField, Tooltip("Maximum speed [km/h]")]
        private float max_speed = 1f;

        [SerializeField, Tooltip("Acceleration of the vehicle [km/h/s]")]
        private float acc = 1f;

        [SerializeField, Tooltip("Braking decelleration of the vehicle [km/h/s]")]
        private float dec = 1f;

        [SerializeField, Tooltip("Tag of the collider that destroys this vehicle")]
        private string DestroyTag = "vehicle_destroy";

    private float speed = 0;
    private bool driving = true;

    private void Start() 
    {
        // Start with half the maximum speed
        speed = max_speed/2;
    }

    private void FixedUpdate() 
    {
        // Determine whether to accelerate or decellerate
        if(driving){
            if(speed < max_speed){
                speed += acc/3.6f*Time.fixedDeltaTime;
            }
        } else {
            if(speed > 0){
                speed -= dec/3.6f*Time.fixedDeltaTime;
            }
        }

        // Prevent the vehicle from going backwards
        if(speed < 0){
            speed = 0;
        }

        // Move forward with the current speed
        transform.Translate(speed / 3.6f * Vector3.forward * Time.fixedDeltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if there is an obstacle in front of the vehicle
        if (other.gameObject.CompareTag("vehicle_stop"))
        {
            driving = false;
        }

        // Check if the vehicle destroyer is in front of the vehicle
        if (other.gameObject.CompareTag(DestroyTag))
        {
            // Warp the vehicle such that it exits all triggers before being destroyed
            StartCoroutine(WarpAndDestroy());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if there is an obstacle in front of the vehicle
        if (other.gameObject.CompareTag("vehicle_stop"))
        {
            driving = true;
        }
    }

    public void WarpAndDestroyVehicle()
    {
        // Warp the vehicle such that it exits all triggers before being destroyed
        StartCoroutine(WarpAndDestroy());
    }

    IEnumerator WarpAndDestroy()
    {
        max_speed = 10000;
        speed = 10000;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
