using UnityEngine;
using System;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static event Action OnTimeOver;

    [Header("Configuración del Tiempo")]
    [Tooltip("Tiempo inicial en segundos.")]
    [SerializeField] private float timeRemaining = 60f;
    [SerializeField] private bool timerIsRunning = true;

    [Tooltip("Tiempo que se resta al chocar con un objeto.")]
    [SerializeField] private float penaltyAmount = 5f;

    [Header("Referencias de UI")]
    [SerializeField] private TextMeshProUGUI timerText;



    // --- SUSCRIPCIÓN AL EVENTO ---
    private void OnEnable()
    {
        // Nos suscribimos al evento del objeto que cae
        FallingObject.OnPlayerHit += ApplyTimePenalty;
    }

    private void OnDisable()
    {
        // MUY IMPORTANTE: Siempre desuscribirse de los eventos estáticos 
        // para evitar errores de memoria o referencias nulas si cambias de escena
        FallingObject.OnPlayerHit -= ApplyTimePenalty;
    }

    void OnDestroy()
    {
        FallingObject.OnPlayerHit -= ApplyTimePenalty;
    }

    // Este método se ejecutará automáticamente cuando FallingObject lance el evento
    private void ApplyTimePenalty(GameObject player, GameObject fallingObject)
    {
        Debug.Log("¡El jugador fue golpeado! Penalización de " + penaltyAmount + " segundos.");
        SubtractTime(penaltyAmount);
    }
    // -----------------------------

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                }

                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                Debug.Log("¡Se acabó el tiempo!");
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimeOver?.Invoke();

                // Aquí iría tu lógica de GameOver
            }
        }
    }

    public void SubtractTime(float amount)
    {
        timeRemaining -= amount;
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
        }
        UpdateTimerDisplay(timeRemaining);
    }

    public void AddTime(float amount)
    {
        timeRemaining += amount;
        UpdateTimerDisplay(timeRemaining);
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timerText == null) return;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer() => timerIsRunning = false;
    public void StartTimer() => timerIsRunning = true;
}