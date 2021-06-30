using UnityEngine;

public class Behaviour_Ventilator : MonoBehaviour
{
    [Header("Settings and Components")]

        [SerializeField, Tooltip("Factor to map a setting integer to a motor velocity")]
        private float MappingFactor;

    // Components to communicate
    private IO_Ventilation ventilatorLogic;
    private Actuator_DCMotor DCMotorActuator;

    // Current ventilator settings
    private int currentVentilatorSetting;
    private bool currentDirectionDriving;

    private void Start()
    {
        // Get the logic and actuator components
        ventilatorLogic = GetComponentInParent<IO_Ventilation>();
        DCMotorActuator = GetComponent<Actuator_DCMotor>();
    }

    void Update()
    {
        // If the ventilatersetting in the IO-script changes, update it
        if (currentVentilatorSetting != ventilatorLogic.VentilatorSetting)
        {
            // Update the velocity of the ventilators
            currentVentilatorSetting = ventilatorLogic.VentilatorSetting;

            if (currentDirectionDriving)
            {
                DCMotorActuator.currentTargetPLC = MappingFactor * currentVentilatorSetting;
            }
            else
            {
                DCMotorActuator.currentTargetPLC = -MappingFactor * currentVentilatorSetting;
            }
            
        }

        if (currentDirectionDriving != ventilatorLogic.VentilatorDrivingDirection)
        {
            currentDirectionDriving = ventilatorLogic.VentilatorDrivingDirection;
        }
    }
}
