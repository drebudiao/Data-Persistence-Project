using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Make a Singleton Instance
    public static GameData Instance;
    public List<PlayerData> playerList = new();
    public int curPlayerIndx;
    public int bestPlayerIndx;

    private string saveFilename = "breakout.dat";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            curPlayerIndx = 0;
            bestPlayerIndx = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int bestScore;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<PlayerData> playerList;
        public int curPlayerIndx;
        public int bestPlayerIndx;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();

        data.playerList = playerList;
        data.curPlayerIndx = curPlayerIndx;
        data.bestPlayerIndx = bestPlayerIndx;

        string json = JsonUtility.ToJson(data);

        Debug.Log("Save Game Data:" + json);

        File.WriteAllText(Application.persistentDataPath + saveFilename, json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + saveFilename;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerList = data.playerList;
            curPlayerIndx = data.curPlayerIndx;
            bestPlayerIndx = data.bestPlayerIndx;
        }
    }

    public bool IsPlayerExisting(string playerName)
    {
        bool exists = false;

        exists = playerList.Exists(data => data.playerName == playerName);

        return exists;
    }

    public void AddPlayer(string playerName, int bestScore)
    {
        PlayerData pData = new PlayerData();
        pData.playerName = playerName;
        pData.bestScore = bestScore;

        playerList.Add(pData);

        curPlayerIndx = playerList.Count - 1;
    }

    public int GetPlayerIndex(string playerName)
    {
        return playerList.FindIndex(data => data.playerName == playerName);
    }

    public string GetPlayerName(int index)
    {
        if (index >= 0 && index < playerList.Count)
            return playerList[index].playerName;
        else
            return "";
    }

    public int GetPlayerBestScore(int index)
    {
        if (index >= 0 && index < playerList.Count)
            return playerList[index].bestScore;
        else
            return 0;
    }

    public void setCurrentPlayer(int index)
    {
        curPlayerIndx = index;
    }

    public string GetCurrentPlayerName()
    {
        return GetPlayerName(curPlayerIndx);
    }

    public string GetBestPlayerName()
    {
        return GetPlayerName(bestPlayerIndx);
    }

    public int GetBestPlayerBestScore()
    {
        return GetPlayerBestScore(bestPlayerIndx);
    }

    public void UpdatePlayerBestScore(int index, int bestScore)
    {
        if (index >= 0 && index < playerList.Count)
        {
            // Check if current player best score needs to be updated
            if (playerList[index].bestScore < bestScore)
            {
                playerList[index].bestScore = bestScore;
            }
            // Check if Best Player best score needs to be updated
            if (playerList[bestPlayerIndx].bestScore < bestScore)
            {
                bestPlayerIndx = index;
            }
        }
    }
}
