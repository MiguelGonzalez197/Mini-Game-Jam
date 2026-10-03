using UnityEngine;


public class PlayerLauncher : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [Header("Configuración de Lanzamiento")]
    public float launchForceMultiplier = 5f;
    public float maxDragDistance = 3f;

    [Header("Trayectoria Predictiva")]
    public int trajectorySteps = 30; // Número de puntos que formarán la curva
    public float timeStep = 0.05f;   // Tiempo simulado entre cada punto (menor = curva más detallada)

    private Rigidbody2D rb;
    private LineRenderer lr;
    private bool isDragging = false;
    private Vector2 dragStartPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        lr.positionCount = trajectorySteps;
        lr.enabled = false;

        AnchorPlayer();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = dragStartPos - mousePos;

            if (dragVector.magnitude > maxDragDistance)
            {
                dragVector = dragVector.normalized * maxDragDistance;
            }

            // Calculamos la fuerza exacta que se aplicaría
            Vector2 appliedForce = dragVector * launchForceMultiplier;

            // Dibujamos la parábola basándonos en esa fuerza
            DrawTrajectory(appliedForce);
        }
    }

    void OnMouseDown()
    {
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            isDragging = true;
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lr.enabled = true;
        }
    }

    void OnMouseUp()
    {

        if (!isDragging || !enabled) return;

        isDragging = false;
        lr.enabled = false;
        rb.isKinematic = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragVector = dragStartPos - mousePos;

        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        rb.AddForce(dragVector * launchForceMultiplier, ForceMode2D.Impulse);
        PlayAnimation("Impulso");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Anchor"))
        {
            PlayAnimation("Salto");
            transform.position = other.transform.position;
            AnchorPlayer();
        }
    }

    private void AnchorPlayer()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    // Método que calcula e inyecta los puntos curvos al LineRenderer
    private void DrawTrajectory(Vector2 force)
    {
        Vector2 startPos = transform.position;

        // Dado que usamos ForceMode2D.Impulse, Velocidad = Fuerza / Masa
        Vector2 initialVelocity = force / rb.mass;

        // Aceleración = Gravedad global * Escala de gravedad del Rigidbody
        Vector2 gravityCalc = Physics2D.gravity * rb.gravityScale;

        lr.positionCount = trajectorySteps;

        for (int i = 0; i < trajectorySteps; i++)
        {
            // Tiempo futuro simulado para este punto específico
            float t = i * timeStep;

            // Aplicamos la fórmula: P(t) = P0 + V0*t + 0.5*a*t^2
            Vector2 pointPosition = startPos + (initialVelocity * t) + (0.5f * gravityCalc * (t * t));

            lr.SetPosition(i, pointPosition);
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (playerAnimator == null) return;
        Debug.Log(animationName);
        playerAnimator.CrossFade(animationName, 0f);
    }
}