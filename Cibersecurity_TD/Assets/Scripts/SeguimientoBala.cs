using System.Collections;
using UnityEngine;

public class BalaRastreadoraConstante : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float velocidadRotacion = 200f;
    [SerializeField] private float tiempoDeVida = 5f;

    [SerializeField] private float rango = 5f;

    [Header("Búsqueda del Objetivo")]
    [SerializeField] private string tagEnemigo = "Enemigo";
    [SerializeField] private float intervaloBusqueda = 0.1f; // Reevalúa el objetivo 10 veces por segundo

    private Transform objetivo;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tiempoDeVida);

        // Inicia la búsqueda continua en segundo plano
        StartCoroutine(RutinaBuscarEnemigo());
    }

    private void FixedUpdate()
    {
        // Si el objetivo es destruido o no hay ninguno, avanza hacia adelante
        if (objetivo == null)
        {
            rb.linearVelocity = transform.up * velocidad;
            return;
        }

        // Rotación y movimiento hacia el objetivo activo
        Vector2 direccion = (Vector2)objetivo.position - rb.position;
        direccion.Normalize();

        float anguloGiro = Vector3.Cross(direccion, transform.up).z;

        rb.angularVelocity = -anguloGiro * velocidadRotacion;
        rb.linearVelocity = transform.up * velocidad;
    }

    // Búsqueda en bucle con delay
    private IEnumerator RutinaBuscarEnemigo()
    {
        while (true)
        {
            BuscarEnemigoMasCercano();
            yield return new WaitForSeconds(intervaloBusqueda); // Espera antes de volver a escanear
        }
    }

    private void BuscarEnemigoMasCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigo);
        float distanciaMasCorta = Mathf.Infinity;
        GameObject enemigoCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distanciaAlEnemigo = Vector2.Distance(transform.position, enemigo.transform.position);

            if (distanciaAlEnemigo < distanciaMasCorta)
            {
                distanciaMasCorta = distanciaAlEnemigo;
                enemigoCercano = enemigo;
            }
        }
        if(distanciaMasCorta > rango)
        {
            enemigoCercano = null;
        }

        if (enemigoCercano != null)
        {
            objetivo = enemigoCercano.transform;
        }
    }
}