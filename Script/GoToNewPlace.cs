using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private string newPlaceName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(newPlaceName);
        }
    }
}
