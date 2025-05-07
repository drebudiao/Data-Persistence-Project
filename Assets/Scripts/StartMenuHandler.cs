using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            if (GameData.Instance.IsPlayerExisting(playerNameInput.text))
            {
                // Set the Current Player
                int index = GameData.Instance.GetPlayerIndex(playerNameInput.text);
                if (index != -1)
                {
                    GameData.Instance.setCurrentPlayer(index);
                }
            }
            else
            {
                // Add a new player
                GameData.Instance.AddPlayer(playerNameInput.text, 0);
            }

            // Save the Game Data
            GameData.Instance.SaveGameData();

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
        string playerName = playerNameInput.text;

        if (GameData.Instance.IsPlayerExisting(playerName))
        {
            int index = GameData.Instance.GetPlayerIndex(playerName);
            if (index != -1)
            {
                GameData.Instance.setCurrentPlayer(index);
                messageText.text = "Best Score: " + GameData.Instance.GetPlayerBestScore(index).ToString("000");
            }
            else
            {
                messageText.text = "New Player";
            }
        }
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
