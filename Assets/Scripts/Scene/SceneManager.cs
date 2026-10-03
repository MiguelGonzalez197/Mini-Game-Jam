using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private Animator restartButtonAnimator_;

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

        restartButtonAnimator_.CrossFade("Show", 0f);

    }
}
