using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Make a Singleton Instance
    public static GameData Instance;
    public string playerName;
    public int bestScore;

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
        public int bestScore;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();

        data.playerName = playerName;
        data.bestScore = bestScore;

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
            bestScore = data.bestScore;
        }
    }

}
