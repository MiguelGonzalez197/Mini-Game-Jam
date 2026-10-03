using UnityEngine;

/// <summary>
/// Va en el hijo "Aviso" del prefab del objeto que cae.
/// Hace un pulso de escala y, al cumplirse la duración, oculta SOLO este
/// objeto (el padre sigue cayendo normalmente).
/// </summary>
public class AvisoPulse : MonoBehaviour
{
    public float pulseSpeed = 14f;
    public float pulseAmount = 0.25f;

    [Tooltip("Segundos que se muestra el aviso antes de ocultarse.")]
    public float duration = 1f;

    private Vector3 baseScale;
    private float t;

    void Awake()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t >= duration)
        {
            gameObject.SetActive(false); // solo desactiva Aviso, no el padre
            return;
        }

        float scale = 1f + Mathf.Sin(t * pulseSpeed) * pulseAmount;
        transform.localScale = baseScale * scale;
    }
}