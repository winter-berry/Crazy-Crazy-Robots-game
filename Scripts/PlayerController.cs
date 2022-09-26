using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private float moveSpeed = 50f;
     [SerializeField]
    private char actionKey1 = 'b';
    [SerializeField]
    private PoolObjectType projType;
    [SerializeField]
    private Transform firePoint;

    /* Variables */
    private Vector3 movement;
    private Vector3 moveDelta;
    private bool facingRight = true;

    /* Components */
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();       
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        /* Shoot */
        if (Input.GetKeyDown((KeyCode)actionKey1))
        {
            GameObject proj_gline = PoolManager.SharedInstance.GetPooledObject(projType);
            proj_gline.transform.SetPositionAndRotation(firePoint.transform.position, firePoint.transform.rotation);
            proj_gline.SetActive(true);
            anim.SetTrigger("Shoot");
        }
    }

    private void RotatePlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void PlayerMovement()
    {
        rb.velocity = moveSpeed * Time.fixedDeltaTime * new Vector3(movement.x, movement.y, 0);
        moveDelta = new Vector3(movement.x, movement.y, 0);

        /* Rotation */
        if (moveDelta.x > 0 && !facingRight)
        {
            RotatePlayer();
        }

        else if (moveDelta.x < 0 && facingRight)
        {
            RotatePlayer();
        }

        /* Animation */
        if (moveDelta.x != 0 || moveDelta.y != 0)
        {
            anim.SetBool("IsRunning", true);
        }

        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
}
