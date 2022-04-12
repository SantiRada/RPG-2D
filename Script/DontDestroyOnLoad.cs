using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    private void Awake()
    {
        if (!PlayerMovement.playerCreated)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
}
