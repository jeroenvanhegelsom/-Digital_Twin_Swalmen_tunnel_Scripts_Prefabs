using UnityEngine;

public class Behaviour_AidCabinet_A : MonoBehaviour
{
    [SerializeField, Tooltip("Motor controller of the left door")]
    private Actuator_LimitedServoMotor leftDoorMotorActuator;

    [SerializeField, Tooltip("Motor controller of the right door")]
    private Actuator_LimitedServoMotor rightDoorMotorActuator;

    [SerializeField, Tooltip("Box shown if the extinguisher is used")]
    private GameObject ExtinguisherBox;

    [SerializeField, Tooltip("Box shown if the emergency phone hose is used")]
    private GameObject EmergencyPhoneBox;

    [SerializeField, Tooltip("Box shown if the fire hose is used")]
    private GameObject FireHoseBox;

    [SerializeField, Tooltip("Box shown if the extinguisher pump is turned on")]
    private GameObject ExtinguisherPumpButtonBox;

    [SerializeField, Tooltip("IO script for communication with the booleans")]
    private IO_AidCabinet_A IOscript;

    // Update is called once per frame
    void Update()
    {
        if (IOscript.DoorsOpen)
        {
            leftDoorMotorActuator.currentTargetPLC = leftDoorMotorActuator.targetRotation;
            rightDoorMotorActuator.currentTargetPLC = rightDoorMotorActuator.targetRotation;
        }
        else
        {
            leftDoorMotorActuator.currentTargetPLC = 0;
            rightDoorMotorActuator.currentTargetPLC = 0;
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

        if (IOscript.FireHoseOn)
        {
            FireHoseBox.SetActive(true);
        }
        else
        {
            FireHoseBox.SetActive(false);
        }

        if (IOscript.ExtinguisherPumpOn)
        {
            ExtinguisherPumpButtonBox.SetActive(true);
        }
        else
        {
            ExtinguisherPumpButtonBox.SetActive(false);
        }
    }
}
