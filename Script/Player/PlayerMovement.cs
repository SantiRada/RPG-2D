using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Header("Variable")]
    [SerializeField] private float speed;
    private Vector2 lastMovement;
    private bool attacking;
    private bool walking;

    [Header("Component")]
    private Rigidbody2D rb2d;
    private Animator anim;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        Vector2 movePosition;

        float h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float v = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        movePosition = new Vector2(h, v).normalized;

        if (movePosition.x != 0 || movePosition.y != 0)
        {
            rb2d.velocity = movePosition;
            walking = true;
        }
        else
            rb2d.velocity = Vector2.zero;



        /*if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0));

            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, rb2d.velocity.y);
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            //transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0));
            rb2d.velocity = new Vector2(rb2d.velocity.x, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw("Vertical"));
        }*/
        if (!walking)
            rb2d.velocity = Vector2.zero;
    }
    private void Attack()
    {
        attacking = false;

        if (Input.GetButton("Fire1"))
        {
            attacking = true;
        }
    }
}
