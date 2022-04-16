using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour {

    [Header("Object")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text playerHealthText;
    [Space]
    [SerializeField] private Slider expBar;
    [SerializeField] private Text playerExpText;

    [Header("Update Level")]
    [Range(0, 2)] [SerializeField] private float timeToUpdateLevel;
    [SerializeField] private GameObject updateLevel;

    [Header("Calls")]
    private HealthManager playerHealth;
    private CharacterStats player;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<HealthManager>();
        player = FindObjectOfType<PlayerMovement>().GetComponent<CharacterStats>();

        updateLevel.SetActive(false);
    }
    private void Update()
    {
        // ---- VIDA ----
        // Stats
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.currentHealth;
        // UI
        playerHealthText.text = playerHealth.currentHealth + " / " + playerHealth.maxHealth;

        // ---- EXPERIENCIA ----
        // Stats
        expBar.maxValue = player.expToLevelUp[player.currentLevel];
        expBar.value = player.currentExp;
        // UI
        playerExpText.text = player.currentExp.ToString() + " / " + player.expToLevelUp[player.currentLevel].ToString();
    }
    public void UpdateLevel()
    {
        StartCoroutine(ChangeLevel());
    }
    private IEnumerator ChangeLevel()
    {
        updateLevel.SetActive(true);
        yield return new WaitForSeconds(timeToUpdateLevel);
        updateLevel.SetActive(false);
    }
}
