using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [Header("Variable")]
    [SerializeField] private Vector2 minPos;
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private float delayPos;
    private Vector3 newPos;

    [Header("Object")]
    private Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;

        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        newPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPos, delayPos);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y), transform.position.z);
    }
}
