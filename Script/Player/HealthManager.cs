using UnityEngine;

public class HealthManager : MonoBehaviour {

    [Header("Variable")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        maxHealth = PlayerPrefs.GetInt("MaxHealth", 100);
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(currentHealth <= 0)
        {
            Dead();
        }
    }
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }
    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }
    private void Dead()
    {
        if (gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}
