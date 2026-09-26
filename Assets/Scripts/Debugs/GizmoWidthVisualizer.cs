using UnityEngine;

public class GizmoWidthVisualizer : MonoBehaviour
{
    [Header("Configuración de Apertura")]
    [Tooltip("El ancho total de la línea en el eje X.")]
    public float widthX = 5f;

    [Tooltip("Color del gizmo.")]
    public Color gizmoColor = Color.green;

    // Se ejecuta para mostrar el gizmo siempre (o puedes usar OnDrawGizmosSelected)
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Tomamos la posición del objeto como el centro
        Vector3 center = transform.position;

        // Calculamos los extremos izquierdo y derecho dividiendo la apertura a la mitad
        Vector3 leftPoint = center + new Vector3(-widthX / 2f, 0f, 0f);
        Vector3 rightPoint = center + new Vector3(widthX / 2f, 0f, 0f);

        // 1. Dibujamos la línea principal horizontal
        Gizmos.DrawLine(leftPoint, rightPoint);

        // Opcional: Dibujar pequeñas marcas verticales en los extremos para que se note mejor el límite de la apertura
        float markerHeight = 0.4f;
        Gizmos.DrawLine(leftPoint + Vector3.up * (markerHeight / 2f), leftPoint - Vector3.up * (markerHeight / 2f));
        Gizmos.DrawLine(rightPoint + Vector3.up * (markerHeight / 2f), rightPoint - Vector3.up * (markerHeight / 2f));
    }
}