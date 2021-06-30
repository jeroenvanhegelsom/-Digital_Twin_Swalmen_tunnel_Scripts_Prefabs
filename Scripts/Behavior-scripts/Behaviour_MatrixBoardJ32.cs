using UnityEngine;
using UnityEngine.UI;

public class Behaviour_MatrixBoardJ32 : MonoBehaviour
{
    [Header("Matrix Sign Sprites")]

        [SerializeField]
        Sprite matrix_red_cross;

    [Header("Components")]

        [SerializeField, Tooltip("IO script of the J32 system")]
        private IO_J32System IO_script;
        [SerializeField, Tooltip("Image component of the matrix board")]
        private Image MatrixBoardImageComponent;

    void Update()
    {
        if (IO_script.J32SystemOn)
        {
            MatrixBoardImageComponent.sprite = matrix_red_cross; 
            MatrixBoardImageComponent.color = Color.white;
        }
        else
        {
            MatrixBoardImageComponent.sprite = null;
            MatrixBoardImageComponent.color = Color.black;
        }
    }
}
