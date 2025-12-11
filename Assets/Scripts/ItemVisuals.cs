using UnityEngine;

public class ItemVisuals : MonoBehaviour
{
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.25f;
    public float rotateSpeed = 50f;
    public Color glowColor = Color.yellow;

    private Vector3 startPos;
    private Material itemMaterial;

    void Start()
    {
        startPos = transform.position;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            itemMaterial = rend.material;
            itemMaterial.EnableKeyword("_EMISSION");
        }
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (itemMaterial != null)
        {
            float emission = Mathf.PingPong(Time.time, 1.0f);
            Color finalColor = glowColor * Mathf.LinearToGammaSpace(emission);
            itemMaterial.SetColor("_EmissionColor", finalColor);
        }
    }
}