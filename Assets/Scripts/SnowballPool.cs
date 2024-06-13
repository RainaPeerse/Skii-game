using System.Collections.Generic;
using UnityEngine;

public class SnowballPool : MonoBehaviour
{
    public static SnowballPool SharedInstance;
    public List<GameObject> pooledSnowballs;
    public GameObject snowballToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledSnowballs = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(snowballToPool);
            temp.SetActive(false);
            pooledSnowballs.Add(temp);
        }
    }

    public GameObject GetPooledSnowball()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledSnowballs[i].activeInHierarchy)
            {
                return pooledSnowballs[i];
            }
        }
        return null;
    }
}