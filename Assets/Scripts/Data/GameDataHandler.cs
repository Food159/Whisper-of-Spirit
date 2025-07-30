using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameDataHandler : MonoBehaviour
{
    [SerializeField] PlayerController playercontroller;
    [SerializeField] PlayerHealth playerhealth;

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
    }
    private void Start()
    {
        
        PlayerGameData gamedata = LoadData();
        if(gamedata != null)
        {
            //if(playercontroller != null) 
            //{
            //    playercontroller.transform.position = gamedata.playerPos;
            //}
            playerhealth.currentHealth = gamedata.playerHp;
            //playerhealth._isPlayerDead = gamedata.playerDied;
            //if(gamedata.playerDied)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //    //gamedata.playerHp = 100;
            //    gamedata.playerDied = false;
            //    //playerhealth.currentHealth = gamedata.playerHp;
            //    //playerhealth._isPlayerDead = gamedata.playerDied;
            //    //playercontroller.transform.position = new Vector2(-5.63f, -1.888795f);
            //}
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
