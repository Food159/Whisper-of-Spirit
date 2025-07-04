using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCheck : MonoBehaviour
{
    [Header("UI")]
    public RectTransform pointer;
    public RectTransform greenZone;

    [Header("Config")]
    public float moveSpeed = 100f;
    public float minAngle = -90f;
    public float maxAngle = 90f;
    public KeyCode keyInput = KeyCode.Space;

    private bool rotating = false;
    private float currentAngle;
    private void Start()
    {
        StartCheck(); //test
        currentAngle = minAngle;
        pointer.transform.eulerAngles = new Vector3(0, 0, minAngle);
    }
    private void Update()
    {
        if (!rotating)
            return;
        currentAngle += moveSpeed * Time.deltaTime;
        if(currentAngle > maxAngle)
        {
            currentAngle = minAngle;
        }
        pointer.transform.eulerAngles = new Vector3(0, 0, currentAngle);
        if(Input.GetKeyDown(keyInput)) 
        {
            CheckResult();
        }
    }
    public void StartCheck()
    {
        rotating = true;
        currentAngle = minAngle;
        pointer.transform.eulerAngles = new Vector3(0, 0, minAngle);
        gameObject.SetActive(true);
    }
    void CheckResult()
    {
        rotating = false;

        float angle = pointer.localEulerAngles.z;
        if(angle > 180f) 
        {
            angle -= 360f;
        }

        float greenAngle = pointer.localEulerAngles.z;
        if (greenAngle > 180f)
        {
            greenAngle -= 360f;
        }

        float greenRange = greenZone.rect.width;

        float greenMin = greenAngle - greenRange / 2f;
        float greenMax = greenAngle + greenRange / 2f;

        if (angle >= greenMin && angle <= greenMax)
        {
            Debug.Log("Success!!!");
        }
        else
        {
            Debug.Log("Fail");
        }
        gameObject.SetActive(false);
    }
}
