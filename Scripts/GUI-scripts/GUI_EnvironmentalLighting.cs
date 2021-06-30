using UnityEngine;
using UnityEngine.UI;

public class GUI_EnvironmentalLighting : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Directional light representing the sun")]
        private Light Sun;

        [SerializeField, Tooltip("Text in GUI where the value is displayed")]
        private Text GUIValueText;

        [SerializeField, Tooltip("Maximal intensity of the sun")]
        private float MaxIntensity;

        [SerializeField, Tooltip("Minimal intensity of the sun")]
        private float MinIntensity;

        [SerializeField, Tooltip("Starting intensity level (0-8) of the sun")]
        private int InitialLevel;

    private int SunIntensityLevel;

    private void Start()
    {
        SunIntensityLevel = InitialLevel;
        SetSunIntensity();
    }

    public void ButtonPlus()
    {
        if (SunIntensityLevel < 8)
        {
            SunIntensityLevel += 1;
            SetSunIntensity();
        }
    }

    public void ButtonMinus()
    {
        if (SunIntensityLevel > 0)
        {
            SunIntensityLevel -= 1;
            SetSunIntensity();
        }
    }

    private void SetSunIntensity()
    {
        Sun.intensity = SunIntensityLevel / 8f * (MaxIntensity - MinIntensity) + MinIntensity;
        GUIValueText.text = SunIntensityLevel.ToString();
    }
}
