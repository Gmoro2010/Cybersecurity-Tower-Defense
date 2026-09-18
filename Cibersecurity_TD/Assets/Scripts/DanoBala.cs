using UnityEngine;
using UnityEngine.UI;

public class DanoBala : MonoBehaviour
{
    [SerializeField] private float danoDeAtaque = 25f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        VidaObj sistemaVidaEnemigo = other.GetComponent<VidaObj>();

        if (sistemaVidaEnemigo != null)
        {
            sistemaVidaEnemigo.RecibirDano(danoDeAtaque);
            Destroy(gameObject);
        }
    }
}
