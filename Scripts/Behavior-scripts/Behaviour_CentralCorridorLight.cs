using UnityEngine;

public class Behaviour_CentralCorridorLight : MonoBehaviour
{
    [Header("Settings and Components")]

        [SerializeField, Tooltip("Array of light objects in this component")]
        private Light[] Lights;

    private IO_CentralCorridorLighting corridorLightLogic;
    private bool LightIsOn;

    private void Start()
    {
        // Get the logic component
        corridorLightLogic = GetComponentInParent<IO_CentralCorridorLighting>();
    }

    void Update()
    {
        // If the lightsetting in the IO-script changes, update it
        if (LightIsOn != corridorLightLogic.lightsOn)
        {
            LightIsOn = corridorLightLogic.lightsOn;

            // Update the intensity of the lights
            foreach (Light lightObject in Lights)
            {
                if (LightIsOn)
                {
                    lightObject.intensity = 1;
                }
                else
                {
                    lightObject.intensity = 0;
                }
            }
        }
    }
}
