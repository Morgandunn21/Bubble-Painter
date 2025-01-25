using UnityEngine;

public class BubbleBlower : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform bubbleSpawnPoint;
    public float bubbleSpawnRate;
    public Color bubbleColor;
    public float defaultBubbleSpeed;

    private bool spawningBubbles;
    private float bubbleCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawningBubbles = false;   
        bubbleCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleCooldown < 0)
        {
            if (spawningBubbles)
            {
                SpawnBubbles();
                bubbleCooldown = 1 / bubbleSpawnRate;
            }
        }
        else
        {
            bubbleCooldown -= Time.deltaTime;
        }
    }

    public void SpawnBubbles()
    {
        SpawnBubble();
    }

    public void SpawnBubble(Vector3? bubbleOffset = null, float speedMult = 1, float radius = 0.2f)
    {
        Vector3 offset = bubbleOffset ?? Vector3.zero;
        Bubble bubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position + offset, bubbleSpawnPoint.rotation).GetComponent<Bubble>();

        bubble.SetSpeed(defaultBubbleSpeed * speedMult);
        bubble.SetColor(bubbleColor);
        bubble.SetRadius(radius);
    }

    public void SetBlowingbubbles(bool blowingbubbles)
    {
        this.spawningBubbles = blowingbubbles;
    }
}
