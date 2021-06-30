using UnityEngine.SceneManagement;
using UnityEngine;

public class GUI_GoToScene : MonoBehaviour
{
    [SerializeField, Tooltip("Name of the scene to load")]
    private string sceneName;

    public void Button_GoToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
