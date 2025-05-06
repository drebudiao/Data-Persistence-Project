using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuHandler : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI errorMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClick()
    {
        if (playerNameInput.text == "")
        {
            errorMessage.text = "Please input a new or existing player name.";
            errorMessage.gameObject.SetActive(true);
        }
        else
        {
            errorMessage.text = "";
            errorMessage.gameObject.SetActive(false);

            // Load the Scene
            SceneManager.LoadScene(1);
        }
    }

    public void QuitButtonClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
