using UnityEngine;

public class Behaviour_AidCabinet_C : MonoBehaviour
{
    [SerializeField, Tooltip("Motor controller of the door")]
    private Actuator_LimitedServoMotor DoorMotorActuator;

    [SerializeField, Tooltip("Box shown if the extinguisher is used")]
    private GameObject ExtinguisherBox;

    [SerializeField, Tooltip("Box shown if the emergency phone hose is used")]
    private GameObject EmergencyPhoneBox;

    [SerializeField, Tooltip("IO script for communication with the booleans")]
    private IO_AidCabinet_C IOscript;

    // Update is called once per frame
    void Update()
    {
        if (IOscript.DoorsOpen)
        {
            DoorMotorActuator.currentTargetPLC = DoorMotorActuator.targetRotation;
        }
        else
        {
            DoorMotorActuator.currentTargetPLC = 0;
        }

        if (IOscript.HandExtinguisherOn)
        {
            ExtinguisherBox.SetActive(true);
        }
        else
        {
            ExtinguisherBox.SetActive(false);
        }

        if (IOscript.EmergencyPhoneOn)
        {
            EmergencyPhoneBox.SetActive(true);
        }
        else
        {
            EmergencyPhoneBox.SetActive(false);
        }
    }
}
