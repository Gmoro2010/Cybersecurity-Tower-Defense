using UnityEngine;

public class SeguirCamino : MonoBehaviour
{
    [Header("Configuración del Camino")]
    public Transform[] puntosDelCamino; 
    public float velocidad = 3f;
    
    private int indiceSiguientePunto = 0; 
    private float distanciaMargen = 0.01f; 

    void Update()
    {
        if (puntosDelCamino.Length == 0) return;

        Transform puntoObjetivo = puntosDelCamino[indiceSiguientePunto];

        transform.position = Vector2.MoveTowards(
            transform.position, 
            puntoObjetivo.position, 
            velocidad * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, puntoObjetivo.position) < distanciaMargen)
        {
            indiceSiguientePunto++;

            // Si el índice llegó al límite, significa que llegó al final de la ruta
            if (indiceSiguientePunto >= puntosDelCamino.Length)
            {
                // Buscamos si el último punto del camino (el nexo) tiene el sistema de vida
                // Usamos 'indiceSiguientePunto - 1' porque arriba acabamos de sumarle 1 al contador
                Transform ultimoPunto = puntosDelCamino[indiceSiguientePunto - 1];
                NucleoVida nucleo = ultimoPunto.GetComponent<NucleoVida>();

                if (nucleo != null)
                {
                    nucleo.DanarNucleo(1f); // Le quita 1 vida al núcleo
                }

                // Destruye al enemigo
                Destroy(gameObject); 
            }
        }
    }
}
