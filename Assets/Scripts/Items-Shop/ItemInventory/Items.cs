using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemsType
{
    mango, rice, green
}
public class Items : MonoBehaviour
{
    public ItemsType itemstype;

    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] Bullet bullet;
    public bool damageIncrese = false;
    private void Awake()
    {
        playerShooting = FindAnyObjectByType<PlayerShooting>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            StartCoroutine(UseMango());
            Debug.Log("UseMango");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(UseRice());
            Debug.Log("UseRice");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(UseGreen());
            Debug.Log("UseGreen");
        }
    }
    IEnumerator UseMango()
    {
        playerShooting.bulletSpeed = 10 / 0.5f; //10 / 0.5f = 20
        playerShooting.waterReload = 1.25f * 0.5f; // 1.25 * 0.5 = 0.625
        damageIncrese = true;
        yield return new WaitForSeconds(10f);
        playerShooting.bulletSpeed = 10f;
        playerShooting.waterReload = 1.25f;
        damageIncrese = false;
    }
    IEnumerator UseRice()
    {
        playerShooting.bulletSpeed = 10 / 0.5f;
        playerShooting.waterReload = 1.25f * 0.5f;
        yield return new WaitForSeconds(5f);
        playerShooting.bulletSpeed = 10f;
        playerShooting.waterReload = 1.25f;
    }
    IEnumerator UseGreen()
    {
        damageIncrese = true;
        yield return new WaitForSeconds(5f);
        damageIncrese = false;
    }
}
