using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    [Header("Variable")]
    public int damage;
    [Space]
    [SerializeField] private GameObject damageNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);

            // Instancia texto de la cantidad de daño
            GameObject damageCount = Instantiate(damageNumber, transform.position, Quaternion.Euler(Vector3.zero));
            damageCount.GetComponent<DamageNumber>().ApplyColorText("blue");
            damageCount.GetComponent<DamageNumber>().damagePoints = damage;
        }
    }
}
