using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button gameStartButton;
    
    void Start()
    {
        gameStartButton.onClick.AddListener(OnGameStartButtonClicked);
    }

    // Update is called once per frame
    private void OnGameStartButtonClicked()
    {
        string playerName = inputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("플레이어 이름을 입력하세요.");
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Debug.Log("플레이어 이름 저장 됨: " + playerName);

        SceneManager.LoadScene("Level_1");
    }
}
