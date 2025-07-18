using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCheck : MonoBehaviour
{
    [Header("UI")]
    public RectTransform pointer;
    public List<RectTransform> greenZonesInScene;
    public RectTransform greenZoneParent;
    private RectTransform currentGreenZone;

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
    private Dictionary<string, Vector2> greenZoneSuccessAngles = new Dictionary<string, Vector2>()
{
    { "Green1", new Vector2(-10f, 10f) }, //
    { "Green2", new Vector2(30f, 50f) }, //
    { "Green3", new Vector2(-75f, -60f) }, //
    { "Green4", new Vector2(80f, 90f) }, //
    { "Green5", new Vector2(-45f, -30f) }, //
};
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

        // ปิดทุก GreenZone ก่อน
        foreach (var zone in greenZonesInScene)
        {
            zone.gameObject.SetActive(false);
        }

        // สุ่มเปิด 1 GreenZone จากที่วางไว้
        int index = Random.Range(0, greenZonesInScene.Count);
        currentGreenZone = greenZonesInScene[index];
        currentGreenZone.gameObject.SetActive(true);
    }
    void CheckResult()
    {
        rotating = false;

        float angle = pointer.eulerAngles.z;
        if (angle > 180f) angle -= 360f;

        bool success = false;

        if (currentGreenZone != null)
        {
            string zoneName = currentGreenZone.name;

            // เช็กว่า Dictionary มี zone นี้ไหม
            if (greenZoneSuccessAngles.ContainsKey(zoneName))
            {
                Vector2 range = greenZoneSuccessAngles[zoneName];
                float minRange = range.x;
                float maxRange = range.y;

                Debug.Log($"Pointer Angle: {angle} | {zoneName} | Range: {minRange} ถึง {maxRange}");
                Debug.Log($"{zoneName} ใน Dictionary!");

                if (angle >= minRange && angle <= maxRange)
                {
                    success = true;
                }
            }
            else
            {
                Debug.LogWarning($"❗ ไม่พบชื่อ {zoneName} ใน Dictionary!");
            }
        }


        if (success)
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }

        gameObject.SetActive(false);
    }
}
