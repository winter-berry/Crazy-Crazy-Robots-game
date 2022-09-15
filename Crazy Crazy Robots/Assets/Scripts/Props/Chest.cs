using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private int coinsMin = 3;
    [SerializeField]
    private int coinsMax = 5;

    /* Variables */
    [SerializeField]
    private bool collected = false;

    /* Components*/
    private Animator anim;

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
        for (int i = 0; i < Random.Range(coinsMin, coinsMax + 1); i++)
        {
            GameObject coin = PoolManager.SharedInstance.GetPooledObject(PoolObjectType.Coin);
            coin.transform.position = gameObject.transform.position;
            coin.SetActive(true);

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
