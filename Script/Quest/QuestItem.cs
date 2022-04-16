using UnityEngine;

public class QuestItem : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private int questID;
    public string itemName;

    [Header("Calls")]
    private QuestManager manager;

    private void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(manager.quests[questID].gameObject.activeInHierarchy && !manager.questCompleted[questID])
            {
                manager.itemCollected = itemName;
                gameObject.SetActive(true);
            }
        }
    }
}
