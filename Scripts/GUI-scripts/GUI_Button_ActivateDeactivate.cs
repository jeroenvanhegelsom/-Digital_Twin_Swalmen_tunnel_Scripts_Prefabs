using UnityEngine;

public class GUI_Button_ActivateDeactivate : MonoBehaviour
{
    [SerializeField, Tooltip("Gameobjects to activate on button press")]
    private GameObject[] GO_Activate;

    [SerializeField, Tooltip("Gameobjects to deactivate on button press")]
    private GameObject[] GO_Deactivate;

    public void ButtonPress()
    {
        for (int i = 0; i < GO_Activate.Length; i++)
        {
            GO_Activate[i].SetActive(true);
        }

        for (int i = 0; i < GO_Deactivate.Length; i++)
        {
            GO_Deactivate[i].SetActive(false);
        }
    }
}
