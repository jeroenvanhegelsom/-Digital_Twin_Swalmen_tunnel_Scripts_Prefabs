using UnityEngine;

public class Skybox_darkening : MonoBehaviour
{
    private Light SunLightObject;
    private float currentSunlightIntensity;

    void Start()
    {
        // Getting the direcional light component that represents the sun
        SunLightObject = GetComponent<Light>();
        currentSunlightIntensity = SunLightObject.intensity;
    }

    // Update is called once per frame
    void Update()
    {

        // Setting the skybox intensity to the intensity of the sunlight
        if (SunLightObject.intensity != currentSunlightIntensity) 
        {
            currentSunlightIntensity = SunLightObject.intensity;
            RenderSettings.skybox.SetFloat("_Exposure", currentSunlightIntensity);
        }
    }
}
