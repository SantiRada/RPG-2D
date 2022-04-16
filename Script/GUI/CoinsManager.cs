using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private int currentGold;

    [Header("Object")]
    [SerializeField] private Text moneyText;

    [Header("PlayerPrefs")]
    private const string goldkey = "CurrentGold";

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        currentGold = PlayerPrefs.GetInt(goldkey, 0);
        moneyText.text = currentGold.ToString();
    }
    public void AddMoney(int moneyCollected)
    {
        currentGold += moneyCollected;
        PlayerPrefs.SetInt(goldkey, currentGold);
        moneyText.text = currentGold.ToString();
    }
}
