using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public Text playerNameText;
    public Text bestScoreText;
    private string playerName;
    private string bestPlayerName;
    private int bestPlayerBestScore;

    
    // Start is called before the first frame update
    void Start()
    {
        // Load Game Data from Save file
        LoadGameData();

        // Initialze Game variables
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        CheckBestScore();
    }

    private void LoadGameData()
    {
        // GameData.Instance.LoadGameData();
        playerName = GameData.Instance.playerName;
        playerNameText.text = "Player: " + playerName;

        UpdateBestPlayer(GameData.Instance.bestPlayerName, GameData.Instance.bestPlayerBestScore);
    }

    private void UpdateBestPlayer(string name, int score)
    {
        bestPlayerName = name;
        bestPlayerBestScore = score;
        bestScoreText.text = "Best Score: " + bestPlayerName + " - " + bestPlayerBestScore.ToString("000");
    }

    private void SaveGameData()
    {
        CheckBestScore();
    }

    private void CheckBestScore() 
    {
        bool isChanged = false;

        if (m_Points > GameData.Instance.playerBestScore)
        {
            GameData.Instance.playerBestScore = m_Points;
            isChanged = true;
        }

        if (m_Points > GameData.Instance.bestPlayerBestScore)
        {
            GameData.Instance.bestPlayerName = playerName;
            GameData.Instance.bestPlayerBestScore = m_Points;
            isChanged = true;
        }

        if (isChanged)
            GameData.Instance.SaveGameData(); 
    }

}
