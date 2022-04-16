using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Header("Variable")]
    [HideInInspector] public string nextPlaceName;
    [SerializeField] private float speed;
    [HideInInspector] public Vector2 lastMovement;
    private Vector2 movePosition;
    [HideInInspector] public bool canMove;

    [Header("System Attack")]
    [SerializeField] private float attackTime;
    private float attackTimeCounter;
    [HideInInspector] public bool attacking;
    private bool walking;

    [Header("Component")]
    private Rigidbody2D rb2d;
    private Animator anim;

    [Header("Calls")]
    private SoundManager managerSound;
    private PauseMenu pause;
    public static bool playerCreated;

    private void Start()
    {
        managerSound = FindObjectOfType<SoundManager>();

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
        lastMovement = new Vector2(0, -1);
    }
    private void Update()
    {

        Move();

        Attack();

        #region Animator
        if (canMove)
        {
            anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
            anim.SetBool("Attacking", attacking);
            anim.SetBool("Walking", walking);
        }
        anim.SetFloat("LastHorizontal", lastMovement.x);
        anim.SetFloat("LastVertical", lastMovement.y);
        #endregion
    }
    private void Move()
    {
        if (canMove && FindObjectOfType<PauseMenu>().inPause == false)
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
        else
            rb2d.velocity = Vector2.zero;
    }
    private void Attack()
    {
        if (Input.GetButton("Fire1") && !walking && FindObjectOfType<PauseMenu>().inPause == false)
        {
            attackTimeCounter = attackTime;
            attacking = true;
            rb2d.velocity = Vector2.zero;
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
    public void SoundAttack()
    {
        managerSound.PlayerAttack();
    }
    public void UpdateLevel()
    {
        if(GetComponent<CharacterStats>().currentLevel > 1)
        {
            speed = speed * 1.1f;

            HealthManager hm = GetComponent<HealthManager>();
            hm.maxHealth = (int)(hm.maxHealth * 1.35f);
            hm.currentHealth = hm.maxHealth;

            GetComponentInChildren<WeaponDamage>().damage = (int)(GetComponentInChildren<WeaponDamage>().damage * 1.15f);
        }
    }
}
