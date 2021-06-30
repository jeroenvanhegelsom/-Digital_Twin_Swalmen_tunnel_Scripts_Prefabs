using UnityEngine;

public class Behaviour_MainDoor : MonoBehaviour
{
    [SerializeField, Tooltip("Motor controller")]
    private Actuator_LimitedServoMotor DoorMotorActuator;
    [SerializeField, Tooltip("IO script for communication with the booleans")]
    private IO_MainDoor IOscript;

    // Update is called once per frame
    void Update()
    {
        if (IOscript.doorOpen)
        {
            DoorMotorActuator.currentTargetPLC = DoorMotorActuator.targetRotation;
        }
        else
        {
            DoorMotorActuator.currentTargetPLC = 0;
        }
    }
}
