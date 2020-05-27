using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // score
    public Text Score_UIText;
    public int score;
    // end
    bool gameHasEnded = false;
    public float restartDelay = 3f;
    public GameObject endPanel;


    public void WinGame()
    {
        Debug.Log("WIN WIN WIN");
        endPanel.SetActive(true);
    }

    public void LoseGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("LOSE LOSE LOSE");
            Invoke("Restart", restartDelay);
        }
    }

    private void Restart()
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
        score += 1;
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        Score_UIText.text = "Platica: " + score.ToString();
    }

}

