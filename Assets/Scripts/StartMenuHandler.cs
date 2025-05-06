using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class StartMenuHandler : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI messageText;
    public Button startButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Load Saved Game Data
        GameData.Instance.LoadGameData();

        // Add On Submit listner to Player Name Input
        playerNameInput.onSubmit.AddListener(playerNameInputSubmit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButtonClick()
    {
        if (playerNameInput.text == "")
        {
            DisplayErrorMessage("Please input a new or existing player name.");
        }
        else
        {
            if (playerNameInput.text != GameData.Instance.playerName)
            {
                // Save the PlayerName
                GameData.Instance.playerName = playerNameInput.text;
                GameData.Instance.playerBestScore = 0;
                GameData.Instance.SaveGameData();
            }

            // Load the Scene
            SceneManager.LoadScene(1);
        }
    }

    private void DisplayErrorMessage(string errorMsg)
    {
        messageText.text = errorMsg;
        messageText.color = Color.red;
        messageText.gameObject.SetActive(true);
    }
    private void DisplayBestScore()
    {
        if (playerNameInput.text == GameData.Instance.playerName)
            messageText.text = "Best Score: " + GameData.Instance.playerBestScore.ToString("000");
        else
            messageText.text = "New Player";

        messageText.color = Color.black;
        messageText.gameObject.SetActive(true);
    }

    private void HideMessage()
    {
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }

    public void QuitButtonClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    private void playerNameInputSubmit(string text)
    {
        if (text == "")
        {
            DisplayErrorMessage("Please input a new or existing player name.");
        }
        else
        {
            DisplayBestScore();
        }
    }

}
