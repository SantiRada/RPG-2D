using UnityEngine;

public class MovementNPC : MonoBehaviour {

    [Header("Limits")]
    [Min(0)] [SerializeField] private float limitX;
    [Min(0)] [SerializeField] private float limitY;

    [Header("Stats To Move")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float walkTime = 1.5f;
    [SerializeField] private float waitTime = 3;
    [SerializeField] private BoxCollider2D villagerZone;
    private float walkCounter;
    private float waitCounter;
    private Vector2 direction;
    private bool isWalking;
    private Vector2[] directionPosibility = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    [HideInInspector] public bool canMove;

    [Header("Component")]
    private Rigidbody2D rb2d;
    private Animator anim;

    [Header("Object")]
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // -- Initial Values ----
        waitCounter = waitTime;
        walkCounter = walkTime;
        canMove = true;
    }
    private void Update()
    {
        if(VerificationPos())
            Move();

        VerificationPosToMove();

        #region Animator
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
        anim.SetBool("InMove", isWalking);
        #endregion
    }
    private void VerificationPosToMove()
    {
        if(villagerZone != null)
        {
            if(transform.position.x < villagerZone.bounds.min.x || transform.position.x > villagerZone.bounds.max.x || transform.position.y < villagerZone.bounds.min.y || transform.position.y > villagerZone.bounds.max.y)
            {
                Stop();
            }
        }
    }
    private void Stop()
    {
        // Detenerse y pasar a esperar
        isWalking = false;
        waitCounter = waitTime;
        rb2d.velocity = Vector2.zero;
    }
    private bool VerificationPos()
    {
        Vector3 distance = new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, 0);
        distance = new Vector3(Mathf.Abs(distance.x), Mathf.Abs(distance.y), 0);

        if (distance.x <= limitX && distance.y <= limitY)
        {
            return true;
        }

        return false;
    }
    public void CheckViewingDirection()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        float distanceY = (player.transform.position.y - transform.position.y);

        if(distanceY > 0)
            direction = directionPosibility[2];

        if(distanceY < 0)
            direction = directionPosibility[3];
    }
    private void Move()
    {
        if (canMove && FindObjectOfType<PauseMenu>().inPause == false)
        {
            if (isWalking)
            {
                // Moverse por un tiempo determinado
                walkCounter -= Time.deltaTime;
                rb2d.velocity = direction;

                if (walkCounter <= 0)
                {
                    // Detenerse y pasar a esperar
                    isWalking = false;
                    waitCounter = waitTime;
                    rb2d.velocity = Vector2.zero;
                }
            }
            else
            {
                // Esperar un tiempo antes de moverse
                waitCounter -= Time.deltaTime;

                if (waitCounter <= 0)
                {
                    // Establecer parámetros para empezar a moverse;
                    isWalking = true;
                    walkCounter = walkTime;

                    direction = directionPosibility[Random.Range(0, directionPosibility.Length)] * speed;
                }
            }
        }
        else
            rb2d.velocity = Vector2.zero;
    }
}
