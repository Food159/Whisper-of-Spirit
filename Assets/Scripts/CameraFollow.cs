using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    private float followSpeed = 2.5f;
    private float yOffset = 0.3f; //0.3
    private float xOffset = 5f; //5
    private float initialY;


    public Transform target;
    public PlayerController player;

    
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if(player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.Log("player not found");
        }
        initialY = transform.position.y;
    }
    private void Update()
    {
        if (target == null && player == null)
            return;

        float targetY = transform.position.y;
        if (player.isOnPlatform)
        {
            targetY = target.position.y + yOffset;
        }
        else
        {
            targetY = initialY;
        }

        Vector3 newPos = new Vector3(target.position.x + xOffset, targetY, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
