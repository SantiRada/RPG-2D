using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private void Start()
    {
        InitialValues();
    }
    private void InitialValues()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();

        if (player != null)
            Destroy(player.gameObject);

        DontDestroyOnLoad[] ds = FindObjectsOfType<DontDestroyOnLoad>();

        for (int i = 0; i < ds.Length; i++)
        {
            Destroy(ds[i].gameObject);
        }
    }
    public void ChangeScene(string scene = "MainScene")
    {
        SceneManager.LoadScene(scene);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
