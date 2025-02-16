using UnityEngine;
using UnityEngine.SceneManagement; 

public class BallExplosion : MonoBehaviour
{
    public GameObject explosionEffect; // Effet d'explosion à assigner dans l'Inspector
    public float explosionForce = 500f; // Force de l'explosion
    public float explosionRadius = 5f;  // Rayon de l'explosion

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Cube"))
            {
                Explode();
                Debug.Log("PERDU");
                Destroy(gameObject);
                EndGame();
            }
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
