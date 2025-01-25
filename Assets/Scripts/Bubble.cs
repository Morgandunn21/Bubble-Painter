using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float lifetime = 10;

    public Color color {  get; private set; }
    public float speed { get; private set; }
    public float radius { get; private set; }
    private Rigidbody rb;
    private Material material;
    private float counter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        counter = 0;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        rb.AddForce(transform.forward*speed, ForceMode.VelocityChange);
    }

    public void SetColor(Color color)
    {
        this.color = color;
        material.color = this.color;
    }

    public void SetRadius(float radius)
    {
        this.radius = radius;
        transform.localScale = Vector3.one * radius;
    }
}
