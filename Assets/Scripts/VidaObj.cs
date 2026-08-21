using UnityEngine;
using UnityEngine.UI;

public class VidaObj : MonoBehaviour
{
    // --- VARIABLES DEL SISTEMA DE VIDA ---
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private Slider barraDeVida; // Arrastra la barra de vida aquí
    private float vidaActual;

    // --- VARIABLES DE LA HITBOX ---
    [SerializeField] private float danoDeAtaque = 25f;

    void Start()
    {
        vidaActual = vidaMaxima;
        
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaMaxima;
        }
    }

    // --- FUNCIÓN DE DAÑO (Para el objeto que recibe el golpe) ---
    public void RecibirDano(float cantidad)
    {
        vidaActual -= cantidad; // CORREGIDO: Ahora usa 'cantidad' en español
        
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaActual;
        }

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    // --- FUNCIÓN DE GOLPE (Para el objeto que choca y hace daño) ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Busca si el objeto con el que chocamos tiene este mismo script de vida
        VidaObj sistemaVidaEnemigo = other.GetComponent<VidaObj>();

        if (sistemaVidaEnemigo != null)
        {
            sistemaVidaEnemigo.RecibirDano(danoDeAtaque);
        }
    }
}
