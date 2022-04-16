using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    [Header("Variable")]
    public string placeName;

    [Header("Component")]
    [SerializeField] private Vector2 facingDirection = new Vector2(0, 1);

    [Header("Calls")]
    private PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (!player.nextPlaceName.Equals(placeName))
        {
            return;
        }

        player.transform.position = transform.position;
        player.lastMovement = facingDirection;
    }
}
