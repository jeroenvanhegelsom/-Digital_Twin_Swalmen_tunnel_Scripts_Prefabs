using UnityEngine;
using UnityEngine.UI;

public class GUI_Smoke : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Smoke particle systems in the traffic tubes")]
        private ParticleSystem[] SmokeParticleSystems;

        [SerializeField, Tooltip("Text fields in GUI where the value is displayed")]
        private Text[] GUIValueTexts;

        [SerializeField, Tooltip("Maximal intensity of the smoke")]
        private float MaxIntensity;

        [SerializeField, Tooltip("Minimal intensity of the smoke")]
        private float MinIntensity;

        [SerializeField, Tooltip("Starting intensity level (0-8) of the smoke")]
        private int InitialLevel;

    public int[] SmokeIntensityLevels = new int[2];

    private void Start()
    {
        for (int i = 0; i < SmokeIntensityLevels.Length; i++)
        {
            SmokeIntensityLevels[i] = InitialLevel;
            SetSmokeIntensity(i);
        }        
    }

    public void ButtonPlus(int i)
    {
        if (SmokeIntensityLevels[i-1] < 8)
        {
            SmokeIntensityLevels[i-1] += 1;
            SetSmokeIntensity(i-1);
        }
    }

    public void ButtonMinus(int i)
    {
        if (SmokeIntensityLevels[i-1] > 0)
        {
            SmokeIntensityLevels[i-1] -= 1;
            SetSmokeIntensity(i-1);
        }
    }

    private void SetSmokeIntensity(int i)
    {
        Color newColor = new Color(0, 0, 0, SmokeIntensityLevels[i] / 8f * (MaxIntensity - MinIntensity) + MinIntensity);
        ParticleSystem.MainModule settings = SmokeParticleSystems[i].main;
        settings.startColor = new ParticleSystem.MinMaxGradient(newColor);
        GUIValueTexts[i].text = SmokeIntensityLevels[i].ToString();
    }
}
