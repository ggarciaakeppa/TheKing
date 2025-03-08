using UnityEngine;

public class Moneda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.RecogerMoneda(gameObject); // Llama a la función en GameManager
            }
        }
    }
}
