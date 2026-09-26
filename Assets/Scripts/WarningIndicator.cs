using UnityEngine;

/// <summary>
/// Va en el prefab del ícono de aviso (UI, hijo de un Canvas).
/// Solo hace un pulso de escala para llamar la atención mientras está activo;
/// el spawner es quien decide cuánto tiempo vive y cuándo lo destruye.
/// </summary>
public class WarningIconPulse : MonoBehaviour
{
    public float pulseSpeed = 14f;
    public float pulseAmount = 0.25f;

    private RectTransform rect;
    private float t;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        t += Time.deltaTime;
        float scale = 1f + Mathf.Sin(t * pulseSpeed) * pulseAmount;
        rect.localScale = Vector3.one * scale;
    }
}