using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float followSpeed = 3f;
    //private float yOffset = 1.7f;
    private float xOffset = 5f;
    public Transform target;
    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, transform.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
