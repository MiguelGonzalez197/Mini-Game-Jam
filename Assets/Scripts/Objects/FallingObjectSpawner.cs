using System.Collections;
using UnityEngine;

/// <summary>
/// Controla cuándo y dónde aparece cada amenaza:
/// 1) Elige una X aleatoria y muestra un aviso en la UI (Canvas), siempre arriba de la pantalla.
/// 2) Al terminar la animación de aviso, instancia el objeto físico en esa misma X.
/// Colócalo en un GameObject vacío en la escena (ej. "Spawner").
/// </summary>
public class FallingObjectSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject fallingObjectPrefab;   // el objeto físico (con FallingObject.cs)
    public GameObject warningUIPrefab;       // ícono de aviso, prefab de UI (hijo de un Canvas)

    [Header("Frecuencia de aparición (segundos entre inicios de aviso)")]
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    [Header("Duración del aviso antes de que nazca el objeto")]
    public float minWarningTime = 0.6f;
    public float maxWarningTime = 1.4f;

    [Header("Rango horizontal de aparición (posición en el mundo)")]
    public float minX = -8f;
    public float maxX = 8f;

    [Header("Altura a la que nace el objeto, sobre el jugador")]
    public float spawnHeightAbovePlayer = 6f;


    [Header("Referencias")]
    public Transform playerTransform;
    public RectTransform uiCanvasRect; // el RectTransform del Canvas (Screen Space - Overlay)
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            StartCoroutine(SpawnSequence());
        }
    }

    IEnumerator SpawnSequence()
    {
        float x = Random.Range(minX, maxX);
        float warningTime = Random.Range(minWarningTime, maxWarningTime);

        GameObject warningUI = null;
        if (warningUIPrefab != null && uiCanvasRect != null && mainCamera != null)
        {
            warningUI = Instantiate(warningUIPrefab, uiCanvasRect);
            PositionWarningUI(warningUI.GetComponent<RectTransform>(), x);

            // Sincroniza la duración del pulso con el tiempo de espera
            var pulse = warningUI.GetComponent<WarningIconPulse>();
            if (pulse != null) pulse.SetDuration(warningTime);
        }

        yield return new WaitForSeconds(warningTime);

        if (warningUI != null) Destroy(warningUI);

        SpawnFallingObject(x);
    }

    void PositionWarningUI(RectTransform rect, float worldX)
    {
        // Convierte la X del mundo a X de pantalla; la Y queda fija cerca del
        // borde superior del Canvas, así siempre se ve "arriba" sin importar
        // la posición de la cámara o del jugador.
        Vector3 worldPoint = new Vector3(worldX, 0f, 0f);
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(worldPoint);
        rect.position = new Vector2(screenPoint.x, uiCanvasRect.rect.height * 0.92f);
    }

    void SpawnFallingObject(float x)
    {
        if (fallingObjectPrefab == null) return;

        float baseY = playerTransform != null ? playerTransform.position.y : 0f;
        float y = baseY + spawnHeightAbovePlayer;

        Vector3 pos = new Vector3(x, y, 0f);

        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        Instantiate(fallingObjectPrefab, pos, rot);
    }
}