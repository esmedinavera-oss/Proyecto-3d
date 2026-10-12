using UnityEngine;
using TMPro;
using System.Collections;

public class Portal : MonoBehaviour
{
    [Header("UI en Pantalla")]
    public TextMeshProUGUI textoMensaje; // Arrastra aquí el TextoMensaje del Canvas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.name.Contains("FirstPersonController"))
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.cristalesRecolectados >= GameManager.Instance.totalCristales)
                {
                    MostrarMensajeEnPantalla("¡VICTORIA! Has escapado del bosque.", 5f);
                }
                else
                {
                    int faltantes = GameManager.Instance.totalCristales - GameManager.Instance.cristalesRecolectados;
                    MostrarMensajeEnPantalla("¡Te faltan cristales! Tienes " + GameManager.Instance.cristalesRecolectados + " / 5.", 3f);
                }
            }
        }
    }

    void MostrarMensajeEnPantalla(string mensaje, float duracion)
    {
        if (textoMensaje != null)
        {
            textoMensaje.text = mensaje;
            StopAllCoroutines();
            StartCoroutine(OcultarMensaje(duracion));
        }
    }

    IEnumerator OcultarMensaje(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        if (textoMensaje != null)
        {
            textoMensaje.text = "";
        }
    }
}