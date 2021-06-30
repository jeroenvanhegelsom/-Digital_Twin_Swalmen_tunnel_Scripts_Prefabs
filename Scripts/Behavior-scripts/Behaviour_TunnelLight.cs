using UnityEngine;

public class Behaviour_TunnelLight : MonoBehaviour
{
    [Header("Settings and Components")]

        [SerializeField, Tooltip("Array of light objects in this component")] 
        private Light[] Lights;

        [SerializeField, Tooltip("Factor to map a setting integer to a light intensity")]
        private float IntensityFactor;

    // Components to communicate
    private IO_TunnelLight tunnelLightLogic;
    private int currentLightSetting;

    private void Start()
    {
        // Get the logic component
        tunnelLightLogic = GetComponentInParent<IO_TunnelLight>();
    }

    void Update()
    {
        // If the lightsetting in the IO-script changes, update it
        if (currentLightSetting != tunnelLightLogic.Lightsetting) {

            currentLightSetting = tunnelLightLogic.Lightsetting;

            // Update the intensity of the lights
            foreach (Light lightObject in Lights)
            {
                lightObject.intensity = currentLightSetting * IntensityFactor;
            }
        }
    }
}
