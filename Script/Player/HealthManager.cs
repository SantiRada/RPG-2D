using UnityEngine;

public class HealthManager : MonoBehaviour {

    [Header("Variable")]
    public string nameCharacter;
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    [SerializeField] private int expWhenDead;

    public bool flashActive;
    public float flashLength;
    private float flashCounter;

    private SpriteRenderer characterRenderer;
    private QuestManager manager;
    private SoundManager managerSound;

    private void Start()
    {
        managerSound = FindObjectOfType<SoundManager>();
        manager = FindObjectOfType<QuestManager>();

        characterRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(currentHealth <= 0)
        {
            Dead();
        }
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;

            if (flashCounter > flashLength * 0.66f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                ToggleColor(true);
            }
            else if (flashCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                ToggleColor(true);
                flashActive = false;
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        if (gameObject.tag == "Player")
            managerSound.PlayerHurt();

        currentHealth -= dmg;

        if(flashLength > 0)
        {
            flashActive = true;
            flashCounter = flashLength;
        }
    }
    private void ToggleColor(bool visible)
    {
        characterRenderer.color = new Color(characterRenderer.color.r, characterRenderer.color.g, characterRenderer.color.b, (visible ? 1.0f : 0.0f));
    }
    private void Dead()
    {
        if (gameObject.tag.Equals("Enemy"))
        {
            managerSound.DeadEnemies();
            manager.enemyKilled = nameCharacter;
            GameObject.Find("Player").GetComponent<CharacterStats>().AddExperience(expWhenDead);
            Destroy(gameObject);
        }
        else if (gameObject.tag.Equals("Player"))
        {
            managerSound.PlayerDead();
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
