using UnityEngine;

public class Gema : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.RecogerGema(gameObject); // Llama a la función en GameManager
            }
        }
    }
}
