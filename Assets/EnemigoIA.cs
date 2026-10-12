using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoIA : MonoBehaviour
{
    [Header("Referencias")]
    public Transform jugador;
    public Transform[] puntosPatrulla;
    
    [Header("Ajustes de Movimiento")]
    public float velocidadPatrulla = 3f;      
    public float velocidadPersecucion = 6f;   
    public float distanciaDeteccion = 8f;   
    public float distanciaOlvido = 14f;
    public float distanciaToque = 1.5f;       
    
    private int indicePuntoActual = 0;
    private bool persiguiendo = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Desactiva las físicas directas para que el movimiento por coordenadas fluya sin trabas
            rb.isKinematic = true;
        }

        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) jugador = playerObj.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= distanciaToque)
        {
            PerderPartida();
            return;
        }

        if (distanciaAlJugador <= distanciaDeteccion)
        {
            persiguiendo = true;
        }
        else if (distanciaAlJugador > distanciaOlvido)
        {
            persiguiendo = false;
        }

        if (persiguiendo)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }
    }

    void Patrullar()
    {
        if (puntosPatrulla.Length == 0 || puntosPatrulla[indicePuntoActual] == null) return;

        Transform puntoObjetivo = puntosPatrulla[indicePuntoActual];
        
        // Movimiento directo y rápido
        transform.position = Vector3.MoveTowards(transform.position, puntoObjetivo.position, velocidadPatrulla * Time.deltaTime);
        transform.LookAt(new Vector3(puntoObjetivo.position.x, transform.position.y, puntoObjetivo.position.z));

        float distanciaAlPunto = Vector3.Distance(transform.position, puntoObjetivo.position);

        if (distanciaAlPunto < 0.5f)
        {
            indicePuntoActual = (indicePuntoActual + 1) % puntosPatrulla.Length;
        }
    }

    void PerseguirJugador()
    {
        transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidadPersecucion * Time.deltaTime);
        transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
    }

    void PerderPartida()
    {
        Debug.Log("¡El esqueleto te atrapó! Perdiste.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}