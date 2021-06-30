using UnityEngine;

public class IO_CentralCorridorLighting : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("The light is on")]
        private string a_on;

    private BoolToggle a_on_boolean;
    [HideInInspector]
    public bool lightsOn;
    private bool SavedValue;

    private void Start()
    {
        a_on_boolean = Static_Functions.FindBoolToggle(a_on);
    }

    private void Update()
    {
        if (SavedValue != a_on_boolean.Boolean)
        {
            SavedValue = a_on_boolean.Boolean;
            lightsOn = SavedValue;
        }
    }
}
