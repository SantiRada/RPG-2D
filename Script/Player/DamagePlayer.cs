using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    [Header("Variable")]
    [SerializeField] private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        }
    }
}
