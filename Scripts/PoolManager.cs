using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Coin,
    Proj_GLine,
    Proj_BLine,
    Proj_GCircle,
    Proj_BCircle
}

[System.Serializable]
public class PoolInfo
{
    /* Customizable */
    [HideInInspector]
    public List<GameObject> objectpool = new List<GameObject>();
    public PoolObjectType objectType;
    public GameObject objectToPool;
    public int amountToPool;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager SharedInstance;

    [SerializeField]
    List<PoolInfo> listOfPools;

    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        for (int i = 0; i < listOfPools.Count; i++)
        {
            FillPool(listOfPools[i]);
        }
    }

    private void FillPool(PoolInfo pool)
    {
        for (int i = 0; i < pool.amountToPool; i++)
        {
            GameObject tmp;
            tmp = Instantiate(pool.objectToPool);
            tmp.SetActive(false);
            pool.objectpool.Add(tmp);
        }
    }

    public GameObject GetPooledObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.objectpool;

        GameObject tmp = null;
        if (pool.Count > 0)
        {
            tmp = pool[^1];
            pool.Remove(tmp);
        }

        /* Insufficient pooled objects */
        else tmp = Instantiate(selected.objectToPool);

        return tmp;
    }

    public void ReturnPooledObject(GameObject obj, PoolObjectType type)
    {
        obj.SetActive(false);
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.objectpool;

        if (!pool.Contains(obj))
        {
            pool.Add(obj);
        }
    }

    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        for (int i = 0; i < listOfPools.Count; i++)
        {
            if (type == listOfPools[i].objectType)
            {
                return listOfPools[i];
            }
        }

        return null;
    }
}
