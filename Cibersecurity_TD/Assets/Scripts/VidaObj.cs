using UnityEngine;
using UnityEngine.UI;

public class VidaObj : MonoBehaviour
{
    // --- VARIABLES DEL SISTEMA DE VIDA ---
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private Slider barraDeVida; // Arrastra la barra de vida aquí
    private float vidaActual;

    // --- VARIABLES DE LA HITBOX ---

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
        Debug.Log("Vida: " + vidaActual);
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaActual;
        }

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }
}
