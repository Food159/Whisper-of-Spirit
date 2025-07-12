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
    private List<RectTransform> activeGreenZone = new List<RectTransform>();
    private void Start()
    {
        StartCheck(); //test
    }
    private void Update()
    {
        if (!rotating)
            return;

        float angleDiff = Mathf.DeltaAngle(minAngle, maxAngle);
        float direction = Mathf.Sign(angleDiff);
        currentAngle += direction * moveSpeed * Time.deltaTime;

        if((direction > 0 && currentAngle > maxAngle) || (direction < 0 && currentAngle < maxAngle))
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

        foreach(var zone in activeGreenZone) 
        {
            Destroy(zone.gameObject);
        }
        activeGreenZone.Clear();
        for (int i = 0; i < greenZoneCount; i++)
        {
            float randomAngle = Random.Range(minAngle, maxAngle);
            RectTransform randomPrefab = greenZonePrefabs[Random.Range(0, greenZonePrefabs.Count)];
            RectTransform newZone = Instantiate(randomPrefab, greenZoneParent);
            newZone.localEulerAngles = new Vector3(greenZoneWidth, newZone.sizeDelta.y);
            activeGreenZone.Add(newZone);
        }
    }
    void CheckResult()
    {
        rotating = false;

        float angle = pointer.localEulerAngles.z;
        if (angle > 180f)
        {
            angle -= 360f;
        }

        bool success = false;
        foreach (var zone in activeGreenZone)
        {
            float zoneAngle = zone.localEulerAngles.z;

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
