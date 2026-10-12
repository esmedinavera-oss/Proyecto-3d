using UnityEngine;

public class FlechaGuia : MonoBehaviour
{
    private Transform objetivoActual;

    void Update()
    {
        ActualizarObjetivoMasCercano();

        if (objetivoActual != null)
        {
            // Calculamos la dirección hacia el objetivo
            Vector3 direccion = objetivoActual.position - transform.position;
            
            // Opcional: si quieres que la flecha solo rote en el plano horizontal (como una brújula)
            direccion.y = 0; 

            if (direccion != Vector3.zero)
            {
                Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
                // Suavizamos el giro de la flecha
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 10f);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void ActualizarObjetivoMasCercano()
    {
        Cristal[] cristales = FindObjectsByType<Cristal>(FindObjectsSortMode.None);
        
        if (cristales.Length == 0)
        {
            objetivoActual = null;
            return;
        }

        Transform masCercano = null;
        float distanciaMinima = Mathf.Infinity;
        Vector3 posicionActual = transform.position;

        foreach (Cristal cristal in cristales)
        {
            float distancia = Vector3.Distance(posicionActual, cristal.transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                masCercano = cristal.transform;
            }
        }

        objetivoActual = masCercano;
    }
}