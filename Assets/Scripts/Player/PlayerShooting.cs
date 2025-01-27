using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private float bulletSpeed = 10f;

    private bool _isFacingRight = true;
    SoundManager soundmanager;
    private void Awake()
    {
        soundmanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            _isFacingRight = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _isFacingRight = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shooting();
        }
    }
    private void shooting()
    {
        soundmanager.PlaySfx(soundmanager.Shoot);
        float rotationZ;
        if (_isFacingRight)
        {
            rotationZ = -90f;
        }
        else
        {
            rotationZ = 90f;
        }
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.Euler(0, 0, rotationZ));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float direction;
        {
            if(_isFacingRight)
            {
                direction = 1f;
            }
            else
            {
                direction = -1f;
            }
        }

        Vector2 bulletScale = bullet.transform.localScale;
        bulletScale.x = Mathf.Abs(bulletScale.x) * direction;
        bullet.transform.localScale = bulletScale;

        if (bulletRb != null) 
        {
            bulletRb.velocity = new Vector2(direction * bulletSpeed, 0f);
        }
        Destroy(bullet, 2f);
    }

}
