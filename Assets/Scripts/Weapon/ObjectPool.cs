using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("PLayer")]
    public GameObject bulletPrefabs;
    public int poolSize = 5;
    private List<GameObject> pool;

    [Space]
    [Header("Enemy Kid")]
    public GameObject kidBulletsPrefabs;
    public int kidPoolSize = 5;
    private List<GameObject> kidPool;
    [SerializeField] private KidController kidcontroller;
    private void Awake()
    {
        kidcontroller = FindObjectOfType<KidController>();
    }
    private void Start()
    {
        pool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
        if(kidcontroller != null)
        {
            kidPool = new List<GameObject>(); //if have kid
            for (int j = 0; j < kidPoolSize; j++)
            {
                GameObject kidBullet = Instantiate(kidBulletsPrefabs);
                kidBullet.SetActive(false);
                kidPool.Add(kidBullet);
            }
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
    public GameObject GetKidObject()
    {
        foreach(GameObject bulletKid in kidPool)
        {
            if(!bulletKid.activeInHierarchy)
            {
                bulletKid.SetActive(true);
                return bulletKid;
            }
        }
        return null;
    }
}
