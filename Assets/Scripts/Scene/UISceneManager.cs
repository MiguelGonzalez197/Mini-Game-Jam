using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    [SerializeField]
    private Animator restartButtonAnimator;
    [SerializeField]
    private string currentScene;

    void Start()
    {
        GameTimer.OnTimeOver += ShowRestart;
    }

    void OnDestroy()
    {
        GameTimer.OnTimeOver -= ShowRestart;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Prueba");
    }

    private void ShowRestart()
    {
        if (restartButtonAnimator == null) return;

        restartButtonAnimator.CrossFade("Show", 0f);

    }
}
