using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private float moveSpeed = 50f;

    /* Variables */
    private Vector3 movement;

    /* Components */
    private Rigidbody2D rb;

    private void Start()
    {
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
    }

    private void PlayerMovement()
    {
        rb.velocity = moveSpeed * Time.fixedDeltaTime * new Vector3(movement.x, movement.y, 0);
    }
}
