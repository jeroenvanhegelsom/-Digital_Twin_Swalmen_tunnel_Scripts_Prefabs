using UnityEngine;

public class SetSecondarySunIntensity : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Main sun")]
        private Light MainSun;

    private float ThisSunIntensity;
    private Light ThisSun;

    private void Start()
    {
        ThisSun = GetComponent<Light>();
    }

    private void Update()
    {
        if (MainSun.intensity != ThisSunIntensity)
        {
            ThisSunIntensity = MainSun.intensity / 2;
            ThisSun.intensity = ThisSunIntensity;
        } 
    }
}
