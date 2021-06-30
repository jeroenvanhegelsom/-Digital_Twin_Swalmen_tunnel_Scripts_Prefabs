using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("GameObject representing the light that is off")]
        private GameObject LightOffGO;
        [SerializeField, Tooltip("GameObject representing the light that is on")]
        private GameObject LightOnGO;

    [Header("Debugging value")]

        [SerializeField, Tooltip("Whether the light is on of off")]
        public bool LightOn = false;

    private bool currentSetting;

    private void Update()
    {
        if (currentSetting != LightOn)
        {
            currentSetting = LightOn;

            LightOnGO.SetActive(LightOn);
            LightOffGO.SetActive(!LightOn);
        }
    }
}
