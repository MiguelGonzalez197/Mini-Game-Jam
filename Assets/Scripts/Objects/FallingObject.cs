using System;
using UnityEngine;

/// <summary>
/// Va en el prefab del objeto que cae. El aviso ya se mostró en la UI antes de
/// que este objeto naciera, así que aquí solo hay caída, colisión, destrucción
/// por altura y la señal si tocó al jugador.
/// Requiere Rigidbody2D y un Collider2D marcado como "Is Trigger".
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class FallingObject : MonoBehaviour
{
    // Cualquier script puede suscribirse: FallingObject.OnPlayerHit += MiMetodo;
    public static event Action<GameObject, GameObject> OnPlayerHit;

    [Header("Velocidad de caída")]
    public float fallSpeed = 6f;

    [Header("Tags")]
    public string playerTag = "Player";

    [Header("Altura mínima antes de destruirse (posición Y del mundo)")]
    public float destroyBelowY = -10f;

    [Header("Aviso (hijo del prefab)")]
    public GameObject avisoIcon;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.linearVelocity = Vector2.down * fallSpeed;

        // Al empezar a caer, oculta solo el icono; el objeto sigue cayendo
        if (avisoIcon != null) avisoIcon.SetActive(false);
    }

    void FixedUpdate()
    {
        if (transform.position.y <= destroyBelowY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            OnPlayerHit?.Invoke(other.gameObject, gameObject); // solo emite la señal
            Destroy(gameObject);
        }
    }
}