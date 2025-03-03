﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private float bulletSpeed = 10f;
    public int waterammo;

    private bool _isFacingRight = true;
    SoundManager soundmanager;
    PauseMenu pausemenu;
    PlayerHealth status;
    PlayerWater playerwater;
    private void Awake()
    {
        playerwater = GetComponent<PlayerWater>();
        status = GetComponent<PlayerHealth>();
        //soundmanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>(); เอาเเพิ่มด้วยยยยยยยยยยยยยยยยย
    }
    private void Start()
    {
        StartCoroutine(WaterReload());
    }
    void Update()
    {
        if (status._isPlayerDead)
            return;
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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                shooting();
            }
        }
    }
    IEnumerator WaterReload()
    {
        while(!status._isPlayerDead) 
        {
            if(playerwater.currentWater < playerwater.maxWater)
            {
                playerwater.currentWater++;
                playerwater.waterbar.SetWater(playerwater.currentWater);
            }
            yield return new WaitForSeconds(2f);
        }
    }
    private void shooting()
    {
        //soundmanager.PlaySfx(soundmanager.Shoot); เพื่มด้วยยย
        if(playerwater.currentWater > 0)
        {
            playerwater.shoot(1);
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            float direction;
            {
                if (_isFacingRight)
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

}
