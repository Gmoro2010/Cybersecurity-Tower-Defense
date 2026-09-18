using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Configuración de Búsqueda")]
    public string targetTag = "Enemigo"; // El Tag de los enemigos
    public float range = 10f;          // Rango máximo de visión
    public float updateRate = 0.2f;    // Frecuencia de búsqueda (segundos)

    [Header("Ajustes de Disparo")]
    public GameObject bulletPrefab;    // Prefab de la bala
    public string firePointTag = "FirePoint"; // Tag del punto de disparo
    public float fireRate = 1f;        // Balas por segundo

    [Header("Ajustes de Rotación")]
    public float rotationOffset = -90f;
    private Transform firePoint;
    private Transform target;
    private float fireCountdown = 0f;

    void Start()
    {
        // Buscar el objeto del punto de disparo globalmente usando FindWithTag
        GameObject fpObj = GameObject.FindWithTag(firePointTag);
        if (fpObj != null)
        {
            firePoint = fpObj.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún GameObject con el Tag '" + firePointTag + "'.");
        }

        // Ejecutar la búsqueda de enemigos periódicamente
        InvokeRepeating(nameof(UpdateTarget), 0f, updateRate);
    }

    void Update()
    {
        if (target == null)
            return;

        // Apuntar hacia el objetivo actual
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Disparar solo cuando hay un objetivo asignado (dentro del rango)
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        Instantiate(bulletPrefab, transform.position, transform.rotation + rotationOffset);
    }

    void UpdateTarget()
    {
        // Búsqueda de enemigos utilizando FindGameObjectsWithTag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}