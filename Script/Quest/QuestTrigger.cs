using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class QuestTrigger : MonoBehaviour {

    [Header("Variables")]
    public int questID;
    public bool startPoint;
    public bool endPoint;

    [Header("Calls")]
    private QuestManager manager;

    private void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!manager.questCompleted[questID])
            {
                if(startPoint && !manager.quests[questID].gameObject.activeInHierarchy)
                {
                    if (!manager.activateQuest)
                    {
                        manager.activateQuest = true;
                        manager.quests[questID].gameObject.SetActive(true);
                        manager.quests[questID].StartQuest();
                    }
                }

                if(endPoint && manager.quests[questID].gameObject.activeInHierarchy)
                {
                    manager.quests[questID].CompleteQuest();
                }
            }
        }
    }
}
