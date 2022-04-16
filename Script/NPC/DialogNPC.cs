using UnityEngine;

public class DialogNPC : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private string[] dialog;
    private bool playerInTheZone;

    [Header("Calls")]
    private DialogManager manager;
    private MovementNPC npc;

    private void Start()
    {
        manager = FindObjectOfType<DialogManager>();
        npc = GetComponentInParent<MovementNPC>();
    }
    private void Update()
    {
        if(playerInTheZone && Input.GetKeyDown(KeyCode.E))
        {
            manager.ShowDialog(dialog, npc);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerInTheZone = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            playerInTheZone = false;
    }
}
