using UnityEngine;

/// <summary>
/// Va en el prefab del ícono de aviso (UI, hijo de un Canvas).
/// Hace un pulso de escala mientras está activo y, al cumplirse su duración, se oculta.
/// El spawner le pasa la duración con SetDuration() para que coincida con el tiempo de aviso.
/// </summary>
public class WarningIconPulse : MonoBehaviour
{
    public float pulseSpeed = 14f;
    public float pulseAmount = 0.25f;

    [Tooltip("Segundos que dura el pulso antes de ocultarse. Si es 0 o menos, no se oculta solo.")]
    public float duration = 1f;

    private RectTransform rect;
    private float t;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetDuration(float seconds)
    {
        duration = seconds;
    }

    void Update()
    {
        t += Time.deltaTime;

        if (duration > 0f && t >= duration)
        {
            rect.localScale = Vector3.one;
            gameObject.SetActive(false); // oculta el warning
            return;
        }

        float scale = 1f + Mathf.Sin(t * pulseSpeed) * pulseAmount;
        rect.localScale = Vector3.one * scale;
    }
}