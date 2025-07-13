using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCheck : MonoBehaviour
{
    [Header("UI")]
    public RectTransform pointer;
    public List<RectTransform> greenZonePrefabs;
    public RectTransform greenZoneParent;

    [Header("Config")]
    public float moveSpeed = 100f;
    public float minAngle = -90f;
    public float maxAngle = 90f;
    public int greenZoneCount = 5;
    public float greenZoneWidth = 20f;
    public KeyCode keyInput = KeyCode.Space;

    private bool rotating = false;
    private float currentAngle;
    private float direction = 1f;
    private List<RectTransform> activeGreenZone = new List<RectTransform>();
    private void Start()
    {
        StartCheck(); //test
    }
    private void Update()
    {
        if (!rotating)
            return;

        currentAngle += direction * moveSpeed * Time.deltaTime;

        if(currentAngle >= maxAngle)
        {
            currentAngle = maxAngle;
            direction = -1f;
        }
        else if(currentAngle <= minAngle)
        {
            currentAngle = minAngle;
            direction = 1f;
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
        direction = 1f;
        pointer.transform.eulerAngles = new Vector3(0, 0, minAngle);
        gameObject.SetActive(true);

        foreach(var zone in activeGreenZone) 
        {
            Destroy(zone.gameObject);
        }
        activeGreenZone.Clear();

        RectTransform randomPrefab = greenZonePrefabs[Random.Range(0, greenZonePrefabs.Count)];
        RectTransform newZone = Instantiate(randomPrefab, greenZoneParent);

        activeGreenZone.Add(newZone);
    }
    void CheckResult()
    {
        rotating = false;

        float angle = pointer.eulerAngles.z;
        if (angle > 180f)
        {
            angle -= 360f;
        }

        bool success = false;
        foreach (var zone in activeGreenZone)
        {
            float zoneAngle = zone.eulerAngles.z;

            if (zoneAngle > 180f)
            {
                zoneAngle -= 360f;
            }
            float halfRange = greenZoneWidth / 2;
            float minRange = zoneAngle - halfRange;
            float maxRange = zoneAngle + halfRange;

            if (angle >= minRange && angle <= maxRange)
            {
                success = true;
                break;
            }
        }
        if (success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }

        gameObject.SetActive(false);
    }
}
