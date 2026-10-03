using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private PlayerLauncher playerLauncherComponent;
    [SerializeField]
    private LineRenderer lineRendererComponent;


    private bool canMove = true;


    void Start()
    {
        GameTimer.OnTimeOver += StunPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        GameTimer.OnTimeOver -= StunPlayer;
    }

    public void StunPlayer()
    {

        Debug.Log("Stunt");

        // 1. Bloqueamos la lectura de inputs
        canMove = false;

        playerLauncherComponent.enabled = false;
        lineRendererComponent.enabled = false;

        // 2. Cortamos cualquier inercia o movimiento actual
        playerRigidbody.linearVelocity = Vector2.zero;

        playerRigidbody.isKinematic = false;

        // Opcional: Si aplicas fuerzas angulares (rotación), también detenla
        playerRigidbody.angularVelocity = 0f;
    }

}
