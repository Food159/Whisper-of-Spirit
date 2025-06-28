using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public int poolSize = 5;

    private List<GameObject> pool;
    private void Start()
    {
        pool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
    }
    public GameObject GetObject()
    {
        foreach(GameObject bullet in pool)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }
}
