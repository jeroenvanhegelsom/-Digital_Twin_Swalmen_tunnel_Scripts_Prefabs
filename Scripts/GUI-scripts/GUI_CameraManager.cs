using UnityEngine;

public class GUI_CameraManager : MonoBehaviour
{
    [Header("Settings")]

        [Tooltip("GameObject with the free camera")]
        public GameObject FreeCamGO;

    public void Button_MoveFreeCamTo(string NewPose)
    {
        string[] parametersTextArray = NewPose.Split(" "[0]);
        float[] parameters = new float[parametersTextArray.Length];

        for (int i = 0; i < parametersTextArray.Length - 1; i++) {
            parameters[i] = float.Parse(parametersTextArray[i]);
        }

        FreeCamGO.transform.position = //...
            new Vector3(parameters[0], parameters[1], parameters[2]);
        FreeCamGO.transform.rotation =  //...
            Quaternion.Euler(new Vector3(parameters[3], parameters[4], parameters[5]));
    }
}
