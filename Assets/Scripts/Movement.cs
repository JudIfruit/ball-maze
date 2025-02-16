using UnityEngine;

public class MapTiltController : MonoBehaviour
{
    public float tiltSpeed = 150f; // Vitesse d'inclinaison
    public float maxTiltAngle = 50f; // Angle maximal d'inclinaison

    private Quaternion initialRotation; // Rotation initiale

    void Start()
    {
        // Sauvegarde de la rotation initiale
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Récupérer les entrées utilisateur
        float horizontalInput = Input.GetAxis("Horizontal"); // Gauche/Droite
        float verticalInput = Input.GetAxis("Vertical");     // Haut/Bas

        // Calcul des nouveaux angles en fonction des entrées
        float tiltX = verticalInput * tiltSpeed * Time.deltaTime;
        float tiltZ = -horizontalInput * tiltSpeed * Time.deltaTime;

        // Obtenir la rotation actuelle en termes d'angles locaux
        Vector3 currentEulerAngles = transform.localEulerAngles;

        // Convertir les angles pour un espace cohérent (éviter les sauts)
        currentEulerAngles.x = NormalizeAngle(currentEulerAngles.x);
        currentEulerAngles.z = NormalizeAngle(currentEulerAngles.z);

        // Appliquer les changements d'inclinaison tout en respectant les limites
        float newTiltX = Mathf.Clamp(currentEulerAngles.x + tiltX, -maxTiltAngle, maxTiltAngle);
        float newTiltZ = Mathf.Clamp(currentEulerAngles.z + tiltZ, -maxTiltAngle, maxTiltAngle);

        // Calculer la nouvelle rotation
        Quaternion targetRotation = Quaternion.Euler(newTiltX, 0f, newTiltZ);

        // Interpolation fluide entre la rotation actuelle et la rotation cible
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 150f);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        if (angle < -180f) angle += 360f;
        return angle;
    }
}
