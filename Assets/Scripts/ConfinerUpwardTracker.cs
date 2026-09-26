using UnityEngine;

public class ConfinerUpwardTracker : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Arrastra aquí el Transform de tu Jugador")]
    public Transform player;

    private float highestYPos;
    private float initialOffsetY;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Guardamos la distancia original entre el límite y el jugador
        // para que no se descentre al empezar a subir.
        initialOffsetY = transform.position.y - player.position.y;

        // Inicializamos el récord de altura
        highestYPos = player.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Si el jugador supera la altura máxima registrada...
        if (player.position.y > highestYPos)
        {
            // 1. Actualizamos el récord
            highestYPos = player.position.y;

            // 2. Movemos el GameObject del PolygonCollider2D hacia arriba
            Vector3 newPosition = transform.position;
            newPosition.y = highestYPos + initialOffsetY;
            transform.position = newPosition;
        }
    }
}
