using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private int cantLevelTotal;
    public int currentLevel;
    public int currentExp;
    public List<int> expToLevelUp = new List<int>();

    private void Start()
    {
        ExpToLevelList();
    }
    private void Update()
    {
        if(currentLevel >= cantLevelTotal)
        {
            return;
        }
        if(currentExp >= expToLevelUp[currentLevel])
        {
            UpdateLevel();
        }
    }
    public void AddExperience(int exp)
    {
        currentExp += exp;
    }
    private void UpdateLevel()
    {
        currentLevel++;
        currentExp = 0;

        if (gameObject.tag.Equals("Player") && currentLevel > 1)
        {
            FindObjectOfType<PlayerMovement>().UpdateLevel();
            FindObjectOfType<ManagerUI>().UpdateLevel();
        }
        else if (gameObject.tag.Equals("Enemy"))
            FindObjectOfType<EnemyController>().UpdateLevel();
    }
    private void ExpToLevelList()
    {
        int maxExp = 10;
        expToLevelUp.Add(0);
        expToLevelUp.Add(maxExp);

        for (int i = 1; i < cantLevelTotal; i++)
        {
            // Modelo Potencial
            int exp = (int)(maxExp * 1.4f);
            expToLevelUp.Add(exp);
            maxExp = exp;
        }
    }
}
