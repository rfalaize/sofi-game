using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject coinObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Sofi")
        {
            coinObject.SetActive(false);
            gameManager.IncrementScore();
        }
    }
}
