using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    protected int coinsMin = 3;
    [SerializeField]
    protected int coinsMax = 5;

    /* Variables */
    [SerializeField]
    protected bool collected = false;
    protected List<GameObject> coins;

    /* Components */
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Collect();
                DropLoot();
                OtherBehavior();
            }
        }     
    }

    protected void Collect()
    {
        collected = true;
        anim.SetTrigger("Collect");
    }

    /* Override */
    protected virtual void DropLoot()
    {
        int roll = Random.Range(coinsMin, coinsMax + 1);

        for (int i = 0; i < roll; i++)
        {
            GameObject coin = PoolManager.SharedInstance.GetPooledObject(PoolObjectType.Coin);
            coin.transform.position = gameObject.transform.position;
            coin.SetActive(true);

            /* Splash effect */
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 2f)), ForceMode2D.Impulse);
        }
    }

    /* Override */
    protected virtual void OtherBehavior()
    {
        //
    }
}
