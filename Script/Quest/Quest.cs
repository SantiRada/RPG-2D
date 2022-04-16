using UnityEngine;

public class Quest : MonoBehaviour {

    [Header("Data")]
    public int questID;
    public string startText;
    [Space]
    public bool needsItem;
    public string itemNeeded;
    [Space]
    public bool needsEnemy;
    public string enemyName;
    public int numberOfEnemies;
    [HideInInspector] public int enemiesKilled;

    [Header("Calls")]
    private QuestManager manager;

    private void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }
    private void Update()
    {
        if (needsItem)
        {
            if (manager.itemCollected.Equals(itemNeeded))
            {
                manager.itemCollected = null;
                CompleteQuest();
            }
        }
        if (needsEnemy)
        {
            if(manager.enemyKilled != null)
            {
                if (manager.enemyKilled.Equals(enemyName))
                {
                    manager.enemyKilled = null;
                    enemiesKilled++;
                    if (enemiesKilled >= numberOfEnemies)
                    {
                        CompleteQuest();
                    }
                }
            }
        }
    }
    public void StartQuest()
    {
        manager = FindObjectOfType<QuestManager>();
        manager.ShowQuestText(questID, startText);
    }
    public void CompleteQuest()
    {
        manager.activateQuest = false;
        manager.questCompleted[questID] = true;
        gameObject.SetActive(false);
    }
}
