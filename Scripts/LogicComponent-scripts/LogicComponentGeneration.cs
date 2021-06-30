using UnityEngine;

public class LogicComponentGeneration : MonoBehaviour
{
    [Header("References")]

        [SerializeField, Tooltip("Prefab object for the actuator logic components")]
        private GameObject actuatorPrefab;
        [SerializeField, Tooltip("Prefab object for the sensor logic components")]
        private GameObject sensorPrefab;
        [SerializeField, Tooltip("Prefab object for the button logic components")]
        private GameObject buttonPrefab;

        [SerializeField, Tooltip("Parent object of the actuator logic components")]
        private Transform actuatorParent;
        [SerializeField, Tooltip("Parent object of the sensor logic components")]
        private Transform sensorParent;
        [SerializeField, Tooltip("Parent object of the button logic components")]
        private Transform buttonParent;

    [Header("Settings")]

        [SerializeField, Tooltip("Option bool true if GUI logic objects also need to be generated")]
        private bool GenerateGUILogicComponents = false;
    
    // Text file paths
    private string outputNames;
    private string inputNames;

    // Arrays of input and output file lines
    private string[] outputList;
    private string[] inputList;

    public void GenerateNewLogicComponents()
    {
        Destroy_Sensors_Actuators_Buttons();
        Generate_Sensors_Actuators_Buttons();
    }

    private void Destroy_Sensors_Actuators_Buttons()
    {
        // Loop through the children of the parents and destroy the child objects
        foreach (Transform child in actuatorParent)
        {
            DestroyImmediate(child.gameObject);
        }

        foreach (Transform child in sensorParent)
        {
            DestroyImmediate(child.gameObject);
        }

        foreach (Transform child in buttonParent)
        {
            DestroyImmediate(child.gameObject);
        }

        // Check if all child objects are destroyed and run the function again if this is not the case
        if (!(actuatorParent.childCount == 0 && sensorParent.childCount == 0 && buttonParent.childCount == 0))
        {
            Destroy_Sensors_Actuators_Buttons();
        }
    }

    private void Generate_Sensors_Actuators_Buttons()
        {

        // Find the file paths and read in the name lists
        outputNames = System.IO.Path.Combine(Application.dataPath, "_outputNames.txt");
        inputNames = System.IO.Path.Combine(Application.dataPath, "_inputNames.txt");
        outputList = System.IO.File.ReadAllLines(outputNames);
        inputList = System.IO.File.ReadAllLines(inputNames);

        // Create the actuators, sensors and buttons

        foreach (string outputLine in outputList)
        {
            // varName is the first 'word' on the the line
            string varName = outputLine.Trim().Split((" ").ToCharArray()[0])[0];

            // Instantiate an actuator if the line contains a correct variable name of the correct type
            if (varName.StartsWith("dvar") && outputLine.Contains("HW") && outputLine.Contains("BOOL"))
            {
                if (varName.Contains("GUI"))
                {
                    if (GenerateGUILogicComponents)
                    {
                        InstantiateLogicComponent(varName, buttonPrefab, buttonParent);
                    }
                }
                else
                {
                    InstantiateLogicComponent(varName, actuatorPrefab, actuatorParent);
                }
            }
        }

        Debug.Log("Outputs Generated");

        foreach (string inputLine in inputList)
        {
            // varName is the first 'word' on the the line
            string varName = inputLine.Trim().Split((" ").ToCharArray()[0])[0];

            // Instantiate an actuator if the line contains a correct variable name
            if (varName.StartsWith("ivar"))
            {
                // Decide it the variable name represents a button or sensor input
                if (varName.Contains("button"))
                {
                    if (GenerateGUILogicComponents)
                    {
                        InstantiateLogicComponent(varName, buttonPrefab, buttonParent);
                    }
                }
                else
                {
                    InstantiateLogicComponent(varName, sensorPrefab, sensorParent);
                }
            }
        }

        Debug.Log("Inputs Generated");
    }

    void InstantiateLogicComponent(string ComponentName, GameObject PrefabToInstantiate, Transform ComponentParent)
    {
        // Instantiate the logic object, set the desired name and assign it to the desired parent
        GameObject newLogicComponent = Instantiate(PrefabToInstantiate, Vector3.zero, Quaternion.identity);
        newLogicComponent.name = ComponentName;
        newLogicComponent.transform.parent = ComponentParent;
    }
}
