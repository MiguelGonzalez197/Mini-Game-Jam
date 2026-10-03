using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LoseTrigger : MonoBehaviour
{
    [SerializeField]
    private UISceneManager uiSceneManager;

    [SerializeField]
    private TextMeshProUGUI loseText;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Perdiste");
            loseText.text = "Perdiste";

            uiSceneManager.ShowRestart();
        }
    }
}
