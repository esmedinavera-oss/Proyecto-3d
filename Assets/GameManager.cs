using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI textoCristales;
    public TextMeshProUGUI textoTiempo;

    [Header("Ajustes del Juego")]
    public float tiempoRestante = 120f; // 2 minutos
    public int cristalesRecolectados = 0;
    public int totalCristales = 5;

    private bool juegoTerminado = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ActualizarTextoCristales();
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTextoTiempo();
        }
        else
        {
            tiempoRestante = 0;
            ActualizarTextoTiempo();
            Derrota();
        }
    }

    public void SumarCristal()
    {
        if (juegoTerminado) return;

        cristalesRecolectados++;
        ActualizarTextoCristales();

        if (cristalesRecolectados >= totalCristales)
        {
            Debug.Log("¡Todos los cristales recolectados! Ve al portal.");
        }
    }

    void ActualizarTextoCristales()
    {
        if (textoCristales != null)
            textoCristales.text = "Cristales: " + cristalesRecolectados + " / " + totalCristales;
    }

    void ActualizarTextoTiempo()
    {
        if (textoTiempo != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            textoTiempo.text = string.Format("Tiempo: {0:00}:{1:00}", minutos, segundos);
        }
    }

    void Derrota()
    {
        juegoTerminado = true;
        Debug.Log("¡Tiempo agotado! Has perdido.");
    }
}