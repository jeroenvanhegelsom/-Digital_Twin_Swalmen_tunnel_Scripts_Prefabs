using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    [SerializeField, Tooltip("Velocity vector at which the texture scrolls")]
    private Vector2 textureScrollVelocity;

    private Vector2 currentOffset = Vector2.zero;
    private Renderer renderComponent;

    private void Start()
    {
        renderComponent = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        currentOffset += (textureScrollVelocity * Time.deltaTime);

        if (renderComponent.enabled)
        {
            renderComponent.materials[0].SetTextureOffset("_MainTex", currentOffset);
        }
    }
}
