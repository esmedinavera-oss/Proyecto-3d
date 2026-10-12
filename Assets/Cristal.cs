using UnityEngine;

public class Cristal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                // Asegúrate de que aquí diga SumarCristal()
                GameManager.Instance.SumarCristal();
            }
            // Destruye el cristal de la escena
            Destroy(gameObject);
        }
    }
}