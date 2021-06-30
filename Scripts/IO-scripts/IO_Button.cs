using System.Collections;
using UnityEngine;

public class IO_Button : MonoBehaviour
{
    [SerializeField, Tooltip("Name of the button logic component belonging to the button")]
    private string buttonString;

    private BoolToggle ButtonBoolToggle;

    private void Start()
    {
        // Connect the button to its logic component
        ButtonBoolToggle = Static_Functions.FindBoolToggle(buttonString);
    }
    public void ButtonPressed() 
    {
        StartCoroutine(ButtonPressedCoRoutine());
    }
    
    IEnumerator ButtonPressedCoRoutine()
    {
        // Make the button value true for 0.05 [s]   
        ButtonBoolToggle.Boolean = true;
        yield return new WaitForSeconds(0.05f);
        ButtonBoolToggle.Boolean = false;
    }
}
