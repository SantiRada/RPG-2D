using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour {

    [Header("Colors")]
    [SerializeField] private Color damageEnemy;
    [SerializeField] private Color damagePlayer;
    [SerializeField] private Color healthPlayer;

    [Header("Variables")]
    [SerializeField] private float damageSpeed;
    [Range(0,3)] [SerializeField] private float timeToDestroy;
    [HideInInspector] public float damagePoints;

    [Header("Objects")]
    [SerializeField] private Text damageText;

    private void Start()
    {
        damageText = transform.GetComponentInChildren<Text>();

        Destroy(gameObject, timeToDestroy);
    }
    private void Update()
    {
        damageText.text = damagePoints.ToString();
        // Se mueve ligeramente hacia arriba
        transform.position = new Vector3(transform.position.x, transform.position.y + damageSpeed * Time.deltaTime, transform.position.z);
    }
    public void ApplyColorText(string color = "white")
    {
        color.ToLower();

        switch (color)
        {
            case "red": damageText.color = damageEnemy; break;
            case "blue": damageText.color = damagePlayer; break;
            case "green": damageText.color = healthPlayer; break;
            case "white": damageText.color = Color.white; break;
        }
    }
}
