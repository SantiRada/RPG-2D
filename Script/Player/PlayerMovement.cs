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

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0));
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0));
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw("Vertical"));
        }
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
