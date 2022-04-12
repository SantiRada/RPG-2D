using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Header("Variable")]
    [HideInInspector] public string nextPlaceName;
    [SerializeField] private float speed;
    [HideInInspector] public Vector2 lastMovement;
    private Vector2 movePosition;

    [Header("System Attack")]
    [SerializeField] private float attackTime;
    private float attackTimeCounter;
    private bool attacking;
    private bool walking;

    [Header("Component")]
    private Rigidbody2D rb2d;
    private Animator anim;

    public static bool playerCreated;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        attackTimeCounter = attackTime;
    }
    private void Update()
    {
        Move();

        Attack();

        #region Animator
        // ------ ANIMATORs ------
        anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("LastHorizontal", lastMovement.x);
        anim.SetFloat("LastVertical", lastMovement.y);
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Walking", walking);
        #endregion
    }
    private void Move()
    {
        walking = false;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movePosition = new Vector2(h, v);

        rb2d.velocity = movePosition.normalized * speed * Time.deltaTime;

        if (movePosition.x != 0 || movePosition.y != 0)
        {
            if (!attacking)
            {
                lastMovement = new Vector2(h, v);
                walking = true;
            }
        }
        else
            rb2d.velocity = Vector2.zero;

        if (!walking)
            rb2d.velocity = Vector2.zero;
    }
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !walking)
        {
            attackTimeCounter = attackTime;
            attacking = true;
        }

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;

            if(attackTimeCounter <= 0)
            {
                attacking = false;
            }
        }
    }
}
