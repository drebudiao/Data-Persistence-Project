using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Make a Singleton Instance
    public static GameData Instance;
    public string playerName;
    public int playerBestScore;
    public string bestPlayerName;
    public int bestPlayerBestScore;


    private string saveFilename = "breakout.dat";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerBestScore;
        public string bestPlayerName;
        public int bestPlayerBestScore;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();

        data.playerName = playerName;
        data.playerBestScore = playerBestScore;
        data.bestPlayerName = bestPlayerName;
        data.bestPlayerBestScore = bestPlayerBestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + saveFilename, json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + saveFilename;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            playerBestScore = data.playerBestScore;
            bestPlayerName = data.bestPlayerName;
            bestPlayerBestScore = data.bestPlayerBestScore;
        }
    }

}
