using UnityEngine;

public class ZoneMusic : MonoBehaviour {

    [Header("Calls")]
    private SoundManager manager;

    private void Start()
    {
        manager = GetComponentInParent<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Forest")
            {
                manager.inForest = true;
                manager.inHouse = false;
            }
            if (gameObject.tag == "House")
            {
                manager.inHouse = true;
                manager.inForest = false;
            }

            manager.ChangeMusic();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            manager.StopMusic();
    }
}
