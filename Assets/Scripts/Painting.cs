using System.Drawing;
using UnityEngine;
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

        // Fill the texture with white (or any base color)
        Color[] fillColor = new Color[textureWidth * textureHeight];
        for (int i = 0; i < fillColor.Length; i++)
            fillColor[i] = defaultCanvasColor;

        canvasTexture.SetPixels(fillColor);
        canvasTexture.Apply();
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
        float maxX = 10 * transform.localScale.x;
        float maxZ = 10 * transform.localScale.z;

        // Convert hitPosition to local texture coordinates
        Vector3 localPos = transform.InverseTransformPoint(hitPosition);

        Debug.Log($"Local Pos: {localPos}");

        float u = 1-((localPos.x/(2*maxX)) + 0.5f); // Convert to [0,1] range
        float v = 1-((localPos.z/(2*maxZ)) + 0.5f);

        Debug.Log($"0-1: {new Vector2(u,v)}");

        int pixelX = Mathf.FloorToInt(u * canvasTexture.width);
        int pixelY = Mathf.FloorToInt(v * canvasTexture.height);

        Debug.Log($"Pixel Pos: {new Vector2(pixelX, pixelY)}");

        // Paint a circle on the texture
        int brushRadius = Mathf.FloorToInt(bubble.radius*10/transform.localScale.x);//(bubble.radius * canvasTexture.width);

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
}
