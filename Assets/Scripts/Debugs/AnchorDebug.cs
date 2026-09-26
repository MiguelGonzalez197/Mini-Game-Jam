using UnityEngine;

public class AnchorDebug : MonoBehaviour
{
    [Header("Configuración Visual")]
    public float radius = 0.5f;
    public Color gizmoColor = Color.green;

    // OnDrawGizmos se ejecuta automáticamente en el Editor de Unity
    void OnDrawGizmos()
    {
        // Define el color de la línea
        Gizmos.color = gizmoColor;

        // Dibuja un círculo vacío (wireframe) en la posición del objeto
        Gizmos.DrawWireSphere(transform.position, radius);

        // Si prefieres un círculo sólido (relleno), usa:
        // Gizmos.DrawSphere(transform.position, radius);
    }
}
