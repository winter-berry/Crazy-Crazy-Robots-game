using UnityEngine;

public class Coin : MonoBehaviour
{
    /* Variables */
    private bool canCollect = false;
    private Vector2 startPos;

    /* Components */
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        /* Newly instantiated coins */
        startPos = transform.position;       
    }

    private void Update()
    {
        StopMovement();      
    }

    private void OnEnable()
    {
        /* Pooled coins */
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollect)
        {           
            if (collision.gameObject.CompareTag("Player"))
            {
                /* Find player manager */
                PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();

                /* Found player manager */
                if (player != null)
                {
                    /* Access player manager */
                    player.AddCoins(Random.Range(12, 18));
                }
            }

            /* Return to pool */
            Collected();
        }
    }

    private void CanCollect(bool status)
    {
        rb.isKinematic = status;
        canCollect = status;
    }

    private void StopMovement()
    {
        if (startPos.y - transform.position.y > Random.Range(0.1f, 0.3f))
        {
            /* Effect ended */
            rb.velocity = new Vector2(0, 0);

            /* Collect after effect */
            CanCollect(true);
        }
    }

    private void Collected()
    {
        PoolManager.SharedInstance.ReturnPooledObject(gameObject, PoolObjectType.Coin);
        CanCollect(false);
    }
}
