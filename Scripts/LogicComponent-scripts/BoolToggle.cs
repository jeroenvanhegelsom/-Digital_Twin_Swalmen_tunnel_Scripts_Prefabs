using UnityEngine;

public class BoolToggle : MonoBehaviour
{
    [Tooltip("Boolean Value of the toggle")]
    public bool Boolean = false;

    public void Toggle(bool oToggle)
    {
        Boolean = oToggle;
    }
}
