using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private float speed;

    [SerializeField] private float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [SerializeField] private float timeToMakeStep;
    [HideInInspector] public float timeToMakeStepCounter;
    [HideInInspector] public Vector2 direction;

    [HideInInspector] public bool attacking;
    [HideInInspector] public bool isMoving;

    [Header("Limits for Player")]
    [SerializeField] private float limitX;
    [SerializeField] private float limitY;
    private Vector3 distancePlayer;

    [Header("Component")]
    private Rigidbody2D rb2d;
    private Animator anim;

    [Header("Object")]
    private PlayerMovement player;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();

        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }
    private void Update()
    {
        VerificateDistance();

        #region Animator
        anim.SetBool("InMove", isMoving);
        anim.SetBool("Attacking", attacking);
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
        #endregion
    }
    private void VerificateDistance()
    {
        distancePlayer = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
        distancePlayer = new Vector3(Mathf.Abs(distancePlayer.x), Mathf.Abs(distancePlayer.y), 0);

        if(distancePlayer.x <= limitX && distancePlayer.y <= limitY)
        {
            MoveEnemy();
        }
    }
    private void MoveEnemy()
    {
        if (!attacking && FindObjectOfType<PauseMenu>().inPause == false)
        {
            if (isMoving)
            {
                timeToMakeStepCounter -= Time.deltaTime;
                rb2d.velocity = direction;

                if (timeToMakeStepCounter < 0)
                {
                    isMoving = false;
                    timeBetweenStepsCounter = timeBetweenSteps;
                    rb2d.velocity = Vector2.zero;
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeToMakeStep;

                    direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * speed;
                }
            }
        }
    }
    public void Stop()
    {
        isMoving = false;
        timeToMakeStepCounter = 0;
        rb2d.velocity = Vector2.zero;
    }
    public void UpdateLevel()
    {
        if (GetComponent<CharacterStats>().currentLevel > 1)
        {
            speed = speed * 1.1f;

            HealthManager hm = GetComponent<HealthManager>();
            hm.maxHealth = (int)(hm.maxHealth * 1.35f);
            hm.currentHealth = hm.maxHealth;

            GetComponent<DamagePlayer>().damage = (int)(GetComponent<DamagePlayer>().damage * 1.15f);
        }
    }
}
