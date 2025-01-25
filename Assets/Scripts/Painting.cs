using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

public class Painting : MonoBehaviour
{
    public int textureWidth = 512;
    public int textureHeight = 512;
    public Color defaultCanvasColor;

    private Texture2D canvasTexture;
    private Renderer canvasRenderer;

    void Start()
    {
        canvasTexture = new Texture2D(textureWidth, textureHeight);
        canvasTexture.filterMode = FilterMode.Point; // Prevent blurring
        canvasRenderer = GetComponent<Renderer>();
        canvasRenderer.material.mainTexture = canvasTexture;

        InputHandler.Instance.OnClearCanvasInput.AddListener(OnClearCanvas);

        ClearCanvas();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bubble"))
        {
            Bubble bubble = other.collider.GetComponent<Bubble>();

            PaintBubble(other.transform.position, bubble);

            // Optional: Destroy or deactivate the bubble after painting
            Destroy(other.gameObject);
        }
    }

    public void PaintBubble(Vector3 hitPosition, Bubble bubble)
    {
        // Convert hitPosition to local texture coordinates
        Vector3 localPos = transform.InverseTransformPoint(hitPosition);

        Debug.Log(localPos);

        float u = 1-((localPos.x/10) + 0.5f); // Convert to [0,1] range
        float v = 1-((localPos.z/10) + 0.5f);

        int pixelX = Mathf.FloorToInt(u * canvasTexture.width);
        int pixelY = Mathf.FloorToInt(v * canvasTexture.height);

        // Paint a circle on the texture
        int brushRadius = Mathf.FloorToInt((bubble.radius/(transform.localScale.x * 10)) * canvasTexture.width);

        for (int y = -brushRadius; y <= brushRadius; y++)
        {
            for (int x = -brushRadius; x <= brushRadius; x++)
            {
                int px = pixelX + x;
                int py = pixelY + y;

                if (px >= 0 && px < canvasTexture.width && py >= 0 && py < canvasTexture.height)
                {
                    float dist = Mathf.Sqrt(x * x + y * y);
                    if (dist <= brushRadius)
                    {
                        // Blend colors (optional)
                        canvasTexture.SetPixel(px, py, bubble.color);
                    }
                }
            }
        }

        canvasTexture.Apply();
    }

    public void ClearCanvas()
    {
        // Fill the texture with white (or any base color)
        Color[] fillColor = new Color[textureWidth * textureHeight];
        for (int i = 0; i < fillColor.Length; i++)
            fillColor[i] = defaultCanvasColor;

        canvasTexture.SetPixels(fillColor);
        canvasTexture.Apply();
    }

    public void OnClearCanvas(InputValue value)
    {
        if (value.isPressed) ClearCanvas();
    }

    public Texture2D GetPainting()
    {
        return canvasTexture;
    }
}
