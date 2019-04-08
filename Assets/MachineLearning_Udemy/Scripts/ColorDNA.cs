using UnityEngine;

public class ColorDNA : MonoBehaviour
{
    //Gene for Color
    public float r;
    public float g;
    public float b;
    public float s;
    public float timeToDie = 0;

    private SpriteRenderer spriteRenderer;
    private Collider2D m_collider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();

        spriteRenderer.color = new Color(r, g, b);
        transform.localScale = Vector3.one * s;
    }

    private void OnMouseDown()
    {
        timeToDie = ColorPopulationManager.elapsed;
        spriteRenderer.enabled = false;
        m_collider.enabled = false;
    }

}
