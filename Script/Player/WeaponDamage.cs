using UnityEngine;

public class WeaponDamage : MonoBehaviour {

    [Header("Variable")]
    public int damage;

    [SerializeField] private GameObject hurtAnimation;
    [SerializeField] private GameObject hitPoint;
    [SerializeField] private GameObject damageNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && FindObjectOfType<PlayerMovement>().attacking)
        {
            // Instancia partículas de sangre
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            GameObject newObject = Instantiate(hurtAnimation, hitPoint.transform.position, hitPoint.transform.rotation);

            Destroy(newObject, 1);

            // Instancia texto de la cantidad de daño
            GameObject damageCount = Instantiate(damageNumber, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
            damageCount.GetComponent<DamageNumber>().ApplyColorText("red");
            damageCount.GetComponent<DamageNumber>().damagePoints = damage;
        }
    }
}
