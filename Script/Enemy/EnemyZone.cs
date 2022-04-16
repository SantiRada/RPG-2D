using UnityEngine;

public class EnemyZone : MonoBehaviour {

    private float breakTime;

    [Header("Calls")]
    private EnemyController manager;

    private void Start()
    {
        manager = GetComponentInParent<EnemyController>();
    }
    private void Update()
    {
        if(breakTime >= 0)
        {
            breakTime -= Time.deltaTime;
            manager.attacking = true;
        }
        else
            manager.attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 distance = new Vector3(manager.transform.position.x - collision.gameObject.transform.position.x, manager.transform.position.y - collision.gameObject.transform.position.y, manager.transform.position.z);
            distance = distance.normalized;

            manager.direction = -distance;

            manager.Stop();
            breakTime = 10000;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            breakTime = 1;
    }
}
