using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [Header("Variables")]
    [HideInInspector] public bool inPause;

    [Header("Object")]
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        pauseMenu.SetActive(inPause);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = true;
            //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Continue()
    {
        inPause = false;
    }
}
