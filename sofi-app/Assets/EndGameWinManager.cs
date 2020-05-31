using UnityEngine;

public class EndGameWinManager : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.WinGame();
        }
    }

}
