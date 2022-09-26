using UnityEngine;

public class Projectile_GLine: MonoBehaviour
{
    public enum Entity
    {
        Player,
        Enemy,
    };

    /* Customizable */
    [SerializeField]
    private Entity[] targets;
    [SerializeField]
    private float projectileSpeed = 5f;
    [SerializeField]
    private float projectileDamage = 5f;
    [SerializeField]
    private float despawnTime = 1.5f;

    /* Variables */
    private float timeSinceSpawn;

    private void Update()
    {
        CheckStatus();
        MoveProjectile();
    }

    private void CheckStatus()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= despawnTime)
        {
            if (gameObject.activeInHierarchy)
            {
                timeSinceSpawn = 0f;
                PoolManager.SharedInstance.ReturnPooledObject(gameObject, PoolObjectType.Proj_GLine);
            }
        }
    }

    private void MoveProjectile()
    {
        transform.position += projectileSpeed * Time.deltaTime * transform.right;
    }

    private void ReturnProjectile()
    {
        PoolManager.SharedInstance.ReturnPooledObject(gameObject, PoolObjectType.Proj_GLine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerManager>();
        var enemy = collision.gameObject.GetComponent<EnemyManager>();

        foreach (var target in targets)
        {
            if (target.Equals(Entity.Player) && player != null)
            {
                player.TakeDamage(projectileDamage);
                ReturnProjectile();
            }

            if (target.Equals(Entity.Enemy) && enemy != null)
            {
                enemy.TakeDamage(projectileDamage);
                ReturnProjectile();
            }
        }
    }
}
