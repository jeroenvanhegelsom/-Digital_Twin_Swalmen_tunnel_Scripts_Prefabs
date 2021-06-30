using System.Collections.Generic;
using UnityEngine;

public class IO_HeightDetection : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("Light on")]
        private string a_light_on;

        [SerializeField, Tooltip("Start the timer")]
        private string a_advice_timerStarted;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("High vehicle detected")]
        private string s_on;

        [SerializeField, Tooltip("No high vehicle detected")]
        private string s_off;

        [SerializeField, Tooltip("No high vehicle detected")]
        private string s_timerFinished;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Duration of the timer")]
        private float timerDuration;

        [SerializeField, Tooltip("Upper sign lights")]
        private LightOnOff[] SignLightsUpper;

        [SerializeField, Tooltip("Lower sign lights")]
        private LightOnOff[] SignLightsLower;

        [SerializeField, Tooltip("Lower sign lights")]
        private float flickerFrequency;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private bool highVehicleDetected = false;
    private bool lightsOn = false;
    private bool lightsOnFlickerBool;
    private bool timerStarted = false;
    private float timer;
    private float flickerTimer;

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_light_on", Static_Functions.FindBoolToggle(a_light_on));
            IO_dict.Add("a_timerStarted", Static_Functions.FindBoolToggle(a_advice_timerStarted));
        
            // Sensors
            IO_dict.Add("s_on", Static_Functions.FindBoolToggle(s_on));
            IO_dict.Add("s_off", Static_Functions.FindBoolToggle(s_off));
            IO_dict.Add("s_timerFinished", Static_Functions.FindBoolToggle(s_timerFinished));
    }

    private void Update()
    {
        if (timerStarted)
        {
            timer -= Time.deltaTime;

            if (timer < 0) {
                timerStarted = false;
            }
        }

        if (lightsOn)
        {
            flickerTimer -= Time.deltaTime;

            if (flickerTimer < 0)
            {
                // Reverse the lights (upper and lower)

                flickerTimer = 1 / flickerFrequency;
                lightsOnFlickerBool = !lightsOnFlickerBool;

                foreach (LightOnOff SignLightUp in SignLightsUpper)
                {
                    SignLightUp.LightOn = lightsOnFlickerBool;
                }

                foreach (LightOnOff SignLightLow in SignLightsLower)
                {
                    SignLightLow.LightOn = !lightsOnFlickerBool;
                }
            }
        }

        // Check whether multiple actuators are on and activate a red box signal
        if (Static_Functions.CountTrueActuators(IO_dict) > 1)
        {
            WarningBox.SetActive(true);
        }

        // --- Enable events based on the actuator states ---
        lightsOn = IO_dict["a_light_on"].Boolean;

        if(IO_dict["a_timerStarted"].Boolean)
        {
            if (!timerStarted)
            {
                timer = timerDuration;
                timerStarted = true;
            } 
        }

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_on"].Boolean = highVehicleDetected;
        IO_dict["s_off"].Boolean = !highVehicleDetected;
        IO_dict["s_timerFinished"].Boolean = !timerStarted;
    }

    // Function called when the receiver receives a high signal from the beam
    public void HighSignal() 
    {
        highVehicleDetected = false;
    }

    // Function called when the receiver receives a low signal from the beam
    public void LowSignal()
    {
        highVehicleDetected = true;
    }
}