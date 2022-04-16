using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    [Header("Variables")]
    public bool[] questCompleted;
    public string itemCollected;
    public string enemyKilled;
    private string questText;
    private int currentQuest;
    [HideInInspector] public bool activateQuest;
    [Space]
    public Quest[] quests;

    [Header("GUI")]
    [SerializeField] private GameObject missionZone;
    [SerializeField] private Text missionText;
    [SerializeField] private Toggle missionComplete;

    [Header("Component")]
    private Animator anim;

    private void Start()
    {
        anim = missionZone.GetComponent<Animator>();

        questCompleted = new bool[quests.Length];
        missionZone.SetActive(false);
    }
    private void Update()
    {
        missionComplete.isOn = questCompleted[currentQuest];

        if (missionComplete.isOn)
        {
            anim.SetInteger("Move", 1);
        }
        if (currentQuest > 0)
        {
            if (quests[currentQuest].needsEnemy)
                missionText.text = questText + "\n" + quests[currentQuest].enemiesKilled.ToString() + "/" + quests[currentQuest].numberOfEnemies.ToString();
        }
    }
    public void ShowQuestText(int questID, string questT)
    {
        questText = questT;
        currentQuest = questID;

        anim.SetInteger("Move", 0);
        missionZone.SetActive(true);

        missionText.text = questText;
    }
}
