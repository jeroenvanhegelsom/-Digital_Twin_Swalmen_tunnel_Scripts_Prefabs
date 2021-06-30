using UnityEngine;

public class Behaviour_EscapeRouteSignModule : MonoBehaviour
{
    [SerializeField, Tooltip("Sign for the ascending route: on")]
    private GameObject AscendingOn;

    [SerializeField, Tooltip("Sign for the ascending route: off")]
    private GameObject AscendingOff;

    [SerializeField, Tooltip("Sign for the descending route: on")]
    private GameObject DescendingOn;

    [SerializeField, Tooltip("Sign for the descending route: off")]
    private GameObject DescendingOff;

    private IO_DynamicEscapeRouteIndication IOscript;

    void Start()
    {
        IOscript = transform.parent.GetComponent<IO_DynamicEscapeRouteIndication>();
    }

    void Update()
    {
        if (!IOscript.IO_dict["a_off"].Boolean)
        {
            AscendingOn.SetActive(IOscript.IO_dict["a_ascending"].Boolean);
            AscendingOff.SetActive(!IOscript.IO_dict["a_ascending"].Boolean);
            DescendingOn.SetActive(IOscript.IO_dict["a_descending"].Boolean);
            DescendingOff.SetActive(!IOscript.IO_dict["a_descending"].Boolean);
        }
        else
        {
            AscendingOn.SetActive(false);
            AscendingOff.SetActive(true);
            DescendingOn.SetActive(false);
            DescendingOff.SetActive(true);
        }
    }
}
