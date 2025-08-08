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

    [Space]
    [Header("Enemy Teen")]
    public GameObject[] teenBulletsPrefabs;
    public int teenPoolSize = 1;
    private List<GameObject> teenPool;
    [SerializeField] private TeenController teencontroller;

    //[Space]
    //[Header("Teen Colour")]
    //public GameObject[] teenColourPrefabs;
    //public int colourPoolSize = 1;
    //private List<GameObject> colourPool;
    private void Awake()
    {
        kidcontroller = FindObjectOfType<KidController>();
        teencontroller = FindObjectOfType<TeenController>();
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
        if (teencontroller != null && teenBulletsPrefabs.Length > 0)
        {
            teenPool = new List<GameObject>(); //if have teen
            for (int k = 0; k < teenPoolSize; k++)
            {
                for(int u = 0; u < teenBulletsPrefabs.Length; u++)
                {
                    GameObject teenBullet = Instantiate(teenBulletsPrefabs[u]);
                    teenBullet.SetActive(false);
                    teenPool.Add(teenBullet);
                }
            }
            //colourPool = new List<GameObject>(); //if have teen
            //for (int p = 0; p < colourPoolSize; p++)
            //{
            //    for (int r = 0; r < teenColourPrefabs.Length; r++)
            //    {
            //        GameObject teenColour = Instantiate(teenColourPrefabs[r]);
            //        teenColour.SetActive(false);
            //        colourPool.Add(teenColour);
            //    }
            //}
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
    public GameObject GetTeenObject()
    {
        int randomIndex = Random.Range(0, teenBulletsPrefabs.Length);
        GameObject prefabUse = teenBulletsPrefabs[randomIndex];

        foreach (GameObject bulletteen in teenPool)
        {
            if (!bulletteen.activeInHierarchy && bulletteen.name.Contains(prefabUse.name))
            {
                bulletteen.SetActive(true);
                return bulletteen;
            }
        }

        //int randomColourIndex = Random.Range(0, teenColourPrefabs.Length);
        //GameObject prefabColourUse = teenColourPrefabs[randomColourIndex];

        //foreach (GameObject colourteen in colourPool)
        //{
        //    if (!colourteen.activeInHierarchy && colourteen.name.Contains(prefabColourUse.name))
        //    {
        //        colourteen.SetActive(true);
        //        return colourteen;
        //    }
        //}
        return null;
    }
}
