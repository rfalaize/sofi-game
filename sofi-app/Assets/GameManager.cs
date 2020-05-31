using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // score
    public TextMeshProUGUI Score_UIText;
    public int score;
    // audio
    public AudioSource backgroundAudioSource;
    // end
    bool gameHasEnded = false;
    public GameObject winPanel;
    public GameObject losePanel;

    public void WinGame()
    {
        Debug.Log("GAME WON");
        backgroundAudioSource.Stop();
        winPanel.SetActive(true);
    }

    public void LoseGame(string reason)
    {
        if (!gameHasEnded)
        {
            backgroundAudioSource.Stop();
            gameHasEnded = true;
            Debug.Log("GAME LOST");
            TextMeshProUGUI losePanelReason = losePanel.transform.Find("TextGameLostReason").GetComponentInChildren<TextMeshProUGUI>();
            losePanelReason.text = reason;
            losePanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
       
    public void ResetScore()
    {
        score = 0;
        UpdateUIText();
    }

    public void IncrementScore()
    {
        score += 10;
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        Score_UIText.text = score.ToString();
    }

}

