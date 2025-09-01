using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameDataHandler : MonoBehaviour
{
    [SerializeField] PlayerController playercontroller;
    [SerializeField] PlayerHealth playerhealth;
    [SerializeField] ScoreManager scoreManager;

    public static GameDataHandler instance;
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
        playercontroller = FindAnyObjectByType<PlayerController>();
        playerhealth = FindAnyObjectByType<PlayerHealth>();
        scoreManager= FindAnyObjectByType<ScoreManager>();
    }
    private void Start()
    {
        
        PlayerGameData gamedata = LoadData();
        if(gamedata != null)
        {
            playerhealth.currentHealth = gamedata.playerHp;
            scoreManager.score = gamedata.playerScore;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ClearData();
        }
    }
    public void SaveData()
    {
        if(Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        PlayerGameData gamedata = new PlayerGameData();
        //gamedata.playerPos = playercontroller.transform.position;
        gamedata.playerHp = playerhealth.currentHealth;
        gamedata.playerScore = scoreManager.score;
        
        //gamedata.playerDied = playerhealth._isPlayerDead;

        string gameDataJson = JsonUtility.ToJson(gamedata);
        File.WriteAllText(Application.dataPath + "/gameData.json", gameDataJson);
        Debug.Log("Save game data");
    }
    public PlayerGameData LoadData()
    {
        if (File.Exists(Application.dataPath + "/gameData.json") == false)
        {
            return null;
        }
        string loadedGameDataToJson = File.ReadAllText(Application.dataPath + "/gameData.json");
        PlayerGameData loadedGameData = JsonUtility.FromJson<PlayerGameData>(loadedGameDataToJson);
        Debug.Log("LoadData");
        return loadedGameData;
    }
    public void ClearData()
    {
        string path = Application.dataPath + "/gameData.json";
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
    //    SaveData();
    //}
}
