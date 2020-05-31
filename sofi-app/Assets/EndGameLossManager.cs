using UnityEngine;

public class EndGameLossManager : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.LoseGame("Se cayó al agua y la comió un pescado ...");
        }
    }

}
