using UnityEngine;

public class WeaponDamage : MonoBehaviour {

    [Header("Variable")]
    [SerializeField] private int damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            Debug.Log("Damage Enemy");
        }
    }
}
