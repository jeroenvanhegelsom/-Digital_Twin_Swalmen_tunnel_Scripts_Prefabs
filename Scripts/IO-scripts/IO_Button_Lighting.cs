using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class IO_Button_Lighting : MonoBehaviour
{
    [Header("Button logic component names")]

        [SerializeField]
        private string b_s0;
        [SerializeField]
        private string b_s1;
        [SerializeField]
        private string b_s2;
        [SerializeField]
        private string b_s3;
        [SerializeField]
        private string b_s4;
        [SerializeField]
        private string b_s5;
        [SerializeField]
        private string b_s6;
        [SerializeField]
        private string b_s7;
        [SerializeField]
        private string b_s8;

    [Header("Debugging Values")]

        [Tooltip("Current ventilator setting (integer ranging from 0 to 8)")]
        public int LightingSetting;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();

    private void Start()
    {
        // Filling the I/O dictionary

            // Buttons
            IO_dict.Add("b_s0", Static_Functions.FindBoolToggle(b_s0));
            IO_dict.Add("b_s1", Static_Functions.FindBoolToggle(b_s1));
            IO_dict.Add("b_s2", Static_Functions.FindBoolToggle(b_s2));
            IO_dict.Add("b_s3", Static_Functions.FindBoolToggle(b_s3));
            IO_dict.Add("b_s4", Static_Functions.FindBoolToggle(b_s4));
            IO_dict.Add("b_s5", Static_Functions.FindBoolToggle(b_s5));
            IO_dict.Add("b_s6", Static_Functions.FindBoolToggle(b_s6));
            IO_dict.Add("b_s7", Static_Functions.FindBoolToggle(b_s7));
            IO_dict.Add("b_s8", Static_Functions.FindBoolToggle(b_s8));
    }
    public void Increase()
    {
        if (LightingSetting < 8)
        {
            LightingSetting += 1;
            StartCoroutine(ButtonPressedCoRoutine(LightingSetting));
        }
    }

    public void Decrease()
    {
        if (LightingSetting > 0)
        {
            LightingSetting += 1;
            StartCoroutine(ButtonPressedCoRoutine(LightingSetting));
        }
    }

    IEnumerator ButtonPressedCoRoutine(int i)
    {
        // Make the button value true for 0.05 [s]   
        IO_dict["b_s" + i.ToString()].Boolean = true;
        yield return new WaitForSeconds(.05f);
        IO_dict["b_s" + i.ToString()].Boolean = false;
    }
}
