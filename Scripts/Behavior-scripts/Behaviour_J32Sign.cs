using UnityEngine;

public class Behaviour_J32Sign : MonoBehaviour
{
    [SerializeField, Tooltip("Grey cover GameObject used to 'turn off' the J32 sign")]
    private GameObject SignCover;

    [SerializeField, Tooltip("IO script of the J32 system")]
    private IO_J32System IO_script;

    private void Update()
    {
        // --- Enable events based on the actuator states ---
        SignCover.SetActive(!IO_script.J32SystemOn);
    }
}