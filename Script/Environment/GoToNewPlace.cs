using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private string newPlaceName; // Nombre de la escena a la que transicionarás
    [SerializeField] private string goToPlaceName; // Identificador del SPAWN
    [Range(0, 1)] [SerializeField] private float timeToChange;

    [Header("Object")]
    private GameObject fade;

    [Header("Component")]
    private Animator anim;

    private void Start()
    {
        fade = GameObject.Find("Fade");
        anim = fade.GetComponent<Animator>();
        FadeAnimate();
    }
    private void FadeAnimate()
    {
        StartCoroutine(StartFade());
    }
    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(timeToChange);
        fade.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Cargar nueva escena
            fade.SetActive(true);
            anim.SetBool("InOut", true);
            Invoke("ChangeScene", timeToChange);
        }
    }
    private void ChangeScene()
    {
        FindObjectOfType<PlayerMovement>().nextPlaceName = goToPlaceName;
        SceneManager.LoadScene(newPlaceName);
    }
}
