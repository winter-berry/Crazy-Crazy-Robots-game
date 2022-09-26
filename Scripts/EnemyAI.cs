using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float attackSpeed = 1.5f;
    [SerializeField]
    private float stopDist = 0.6f;
    [SerializeField]
    private PoolObjectType projType;
    [SerializeField]
    private Transform firePoint;

    /* Variables */
    private bool facingRight = true;
    private Vector3 directionToTarget;
    private float attackDelay = 0f;
    private GameObject projectile;

    /* Components */
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        EnemyBehavior();
    }

    private void EnemyMove()
    {
        directionToTarget = (target.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToTarget.x * speed, directionToTarget.y * speed);
        anim.SetBool("IsRunning", true);
    }

    private void EnemyStop()
    {
        rb.velocity = Vector3.zero;
        anim.SetBool("IsRunning", false);
    }

    private void EnemyRotate()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void EnemyAttack()
    {
        attackDelay += Time.deltaTime;

        if (attackDelay >= attackSpeed)
        {

            projectile = PoolManager.SharedInstance.GetPooledObject(projType);
            projectile.transform.SetPositionAndRotation(firePoint.transform.position, firePoint.transform.rotation);
            projectile.SetActive(true);
            anim.SetTrigger("Shoot");          
            attackDelay = 0f;
        }
    }

    private void EnemyBehavior()
    {
        float dist = Vector2.Distance(transform.position, target.transform.position);

        /* Movement */
        if (dist <= stopDist)
        {
            EnemyStop();
            EnemyAttack();
        }

        else
        {
            EnemyMove();
        }

        /* Rotation */
        if (transform.position.x - target.transform.position.x > 0 && facingRight)
        {
            /* Target is on the left */
            EnemyRotate();
        }

        else if (transform.position.x - target.transform.position.x < 0 && !facingRight)
        {
            /* Target is on the right */
            EnemyRotate();
        }
    }
}
