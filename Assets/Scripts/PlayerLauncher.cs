using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    [Header("Configuración de Lanzamiento")]
    public float launchForceMultiplier = 5f;
    public float maxDragDistance = 3f;

    private Rigidbody2D rb;
    private LineRenderer lr;
    private bool isDragging = false;
    private Vector2 dragStartPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        // Inicializamos el LineRenderer
        lr.positionCount = 2;
        lr.enabled = false;

        // Comenzamos anclados
        AnchorPlayer();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculamos el vector en la dirección opuesta al arrastre
            Vector2 dragVector = dragStartPos - mousePos;

            // Limitamos la distancia máxima de arrastre
            if (dragVector.magnitude > maxDragDistance)
            {
                dragVector = dragVector.normalized * maxDragDistance;
            }

            // Dibujamos la línea visual indicando la dirección y fuerza
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, (Vector2)transform.position + dragVector);
        }
    }

    void OnMouseDown()
    {
        // Solo permitimos arrastrar si el jugador está casi quieto (anclado)
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            isDragging = true;
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lr.enabled = true;
        }
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;
        lr.enabled = false;
        rb.isKinematic = false; // Activamos las físicas para que pueda volar

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragVector = dragStartPos - mousePos;

        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        // Aplicamos la fuerza en modo Impulso
        rb.AddForce(dragVector * launchForceMultiplier, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Anchor"))
        {
            // Centramos al jugador en el anclaje exactamente
            transform.position = other.transform.position;
            AnchorPlayer();
        }
    }

    private void AnchorPlayer()
    {
        rb.isKinematic = true; // Desactiva la gravedad y fuerzas externas
        rb.linearVelocity = Vector2.zero; // Detiene el movimiento por completo
        rb.angularVelocity = 0f;
    }
}
