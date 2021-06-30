using UnityEngine;

public class Behaviour_EmergencyExit: MonoBehaviour
{
    [SerializeField, Tooltip("Motor controller")]
    private Actuator_LimitedServoMotor DoorMotorActuator;
    [SerializeField, Tooltip("IO script for communication with the booleans")]
    private IO_EmergencyExit IOscript;

    [SerializeField, Tooltip("Enabled controur lighting GameObject")]
    private GameObject ContourLightingOn;
    [SerializeField, Tooltip("Disabled controur lighting GameObject")]
    private GameObject ContourLightingOff;
    [SerializeField, Tooltip("Motor controller")]
    private AudioSource SoundBeaconAudioSource;

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

        if (IOscript.coutourLightingOn)
        {
            ContourLightingOn.SetActive(true);
            ContourLightingOff.SetActive(false);
        }
        else
        {
            ContourLightingOn.SetActive(false);
            ContourLightingOff.SetActive(true);
        }

        if (IOscript.soundBeaconOn && !IOscript.muted)
        {
            SoundBeaconAudioSource.mute = false;
        }
        else
        {
            SoundBeaconAudioSource.mute = true;
        }
    }
}
