using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI winText;

    [SerializeField]
    private GameObject loseTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ganaste");
            winText.text = "Ganaste";
            loseTrigger.SetActive(false);
        }
    }

}
