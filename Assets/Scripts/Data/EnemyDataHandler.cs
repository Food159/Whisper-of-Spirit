using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyDataHandler : MonoBehaviour
{
    [SerializeField] LungController lungcontroller;
    [SerializeField] EnemyHealth enemyhealth;

    public static EnemyDataHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        EnemyData enemydata = LoadDataEnemy();
        if (enemydata != null)
        {
            if (lungcontroller != null)
            {
                lungcontroller.transform.position = enemydata.enemyPos;
            }
            enemyhealth.currentHealth = enemydata.enemyHp;
            enemyhealth._isDead = enemydata.enemyDied;
            if (enemydata.enemyDied)
            {
                enemydata.enemyHp = 100;
                enemydata.enemyDied = false;
                enemyhealth.currentHealth = enemydata.enemyHp;
                enemyhealth._isDead = enemydata.enemyDied;
                lungcontroller.transform.position = new Vector2(-5.63f, -1.888795f);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveDataEnemy();
            //CMDebug.TextPopup("save", 5, this.transform.position, 2);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ClearDataEnemy();
            //CMDebug.TextPopupMouse("Clear save data");
        }
    }
    public void SaveDataEnemy()
    {
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        EnemyData enemydata = new EnemyData();
        enemydata.enemyPos = lungcontroller.transform.position;
        enemydata.enemyHp = enemyhealth.currentHealth;
        enemydata.enemyDied = enemyhealth._isDead;

        string enemyDataJson = JsonUtility.ToJson(enemydata);
        File.WriteAllText(Application.dataPath + "/enemyData.json", enemyDataJson);
        Debug.Log(enemydata);
    }
    public EnemyData LoadDataEnemy()
    {
        if (File.Exists(Application.dataPath + "/enemyData.json") == false)
        {
            return null;
        }
        string loadedEnemyDataToJson = File.ReadAllText(Application.dataPath + "/enemyData.json");
        EnemyData loadedEnemyData = JsonUtility.FromJson<EnemyData>(loadedEnemyDataToJson);
        return loadedEnemyData;
    }
    public void ClearDataEnemy()
    {
        string path = Application.dataPath + "/enemyData.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("deleted save");
        }
        else
        {
            Debug.Log("No data to delete");
        }
    }
    //public void OnApplicationQuit()
    //{
    //    SaveDataEnemy();
    //}
}
