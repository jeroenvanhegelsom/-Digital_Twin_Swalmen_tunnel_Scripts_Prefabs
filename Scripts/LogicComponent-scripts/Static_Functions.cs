using System.Collections.Generic;
using UnityEngine;

public class Static_Functions : MonoBehaviour
{
    public static int CountTrueActuators(Dictionary<string, BoolToggle> IO_dict)
    {
        int value = 0;

        foreach (var dict_entry in IO_dict)
        {
            if (dict_entry.Key.StartsWith("a") && dict_entry.Value.Boolean)
            {
                value++;
            }
        }

        return value;
    }

    public static BoolToggle FindBoolToggle(string IOName)
    {
        if (IOName.StartsWith("dvar"))
        {
            // The variable is an output
            Transform actuatorParent = GameObject.Find("ActuatorLogicComponents").transform;
            return actuatorParent.Find(IOName).GetComponent<BoolToggle>();
        }
        else if (IOName.StartsWith("ivar"))
        {
            // The variable is an input
            if (IOName.Contains("button"))
            {
                // The variable is a button input
                Transform buttonParent = GameObject.Find("ButtonLogicComponents").transform;
                return buttonParent.Find(IOName).GetComponent<BoolToggle>();
            } 
            else 
            {
                // The variable is a sensor input
                Transform sensorParent = GameObject.Find("SensorLogicComponents").transform;
                return sensorParent.Find(IOName).GetComponent<BoolToggle>();
            }
        }
        else
        {
            Debug.LogWarning(IOName + " is not an input or output variable");
            return null;
        }
    }
}
