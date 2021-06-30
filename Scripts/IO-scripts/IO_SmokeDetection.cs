﻿using System.Collections.Generic;
using UnityEngine;

public class IO_SmokeDetection : MonoBehaviour
{
    [Header("Input logic component names")]

        [SerializeField]
        private string s_state0;
        [SerializeField]
        private string s_state1;
        [SerializeField]
        private string s_state2;
        [SerializeField]
        private string s_state3;
        [SerializeField]
        private string s_state4;
        [SerializeField]
        private string s_state5;
        [SerializeField]
        private string s_state6;
        [SerializeField]
        private string s_state7;
        [SerializeField]
        private string s_state8;

    [Header("Settings and Components")]

        [SerializeField, Tooltip("Minimum smoke intensity used in the interpolation for determining the smoke level")]
        private float SmokeInteisityMin;

        [SerializeField, Tooltip("Maximum smoke intensity used in the interpolation for determining the smoke level)")]
        private float SmokeInteisityMax;

        [SerializeField, Tooltip("Particle system that represents the smoke")]
        private ParticleSystem SmokeParticleSystem;

    [Header("Debugging Values")]

        [SerializeField, Tooltip("Current smoke level measured by the sensor")]
        private float sensorValue;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private float sensedSmokeAlpha;

    private void Start()
    {
        // Sensors
        IO_dict.Add("s_state0", Static_Functions.FindBoolToggle(s_state0));
        IO_dict.Add("s_state1", Static_Functions.FindBoolToggle(s_state1));
        IO_dict.Add("s_state2", Static_Functions.FindBoolToggle(s_state2));
        IO_dict.Add("s_state3", Static_Functions.FindBoolToggle(s_state3));
        IO_dict.Add("s_state4", Static_Functions.FindBoolToggle(s_state4));
        IO_dict.Add("s_state5", Static_Functions.FindBoolToggle(s_state5));
        IO_dict.Add("s_state6", Static_Functions.FindBoolToggle(s_state6));
        IO_dict.Add("s_state7", Static_Functions.FindBoolToggle(s_state7));
        IO_dict.Add("s_state8", Static_Functions.FindBoolToggle(s_state8));
    }

    void Update()
    {
        sensedSmokeAlpha = SmokeParticleSystem.main.startColor.color.a;
        sensorValue = Mathf.RoundToInt((sensedSmokeAlpha - SmokeInteisityMin) / (SmokeInteisityMax - SmokeInteisityMin) * 8);

        for (int i = 0; i < 9; i++)
        {
            IO_dict["s_state" + i.ToString()].Boolean = (i == sensorValue);
        }
    }
}
