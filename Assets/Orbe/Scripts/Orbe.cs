using UnityEngine;

public class Orbe : Flotante
{
    /*
    [Header("Movimiento flotante")]
    public float amplitude = 0.2f;   // qué tanto sube y baja (altura)
    public float frequency = 2f;     // velocidad del movimiento

    private Vector3 startPos;

    void Start()
    {
        // Guardamos la posición inicial del orbe
        startPos = transform.position;
    }

    void Update()
    {
        // Calculamos una nueva posición usando una onda senoidal
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Busca el componente AnimatorControllerGJ en el jugador
            PlayerMovementGJ playerMovement = other.GetComponent<PlayerMovementGJ>();

            if (playerMovement != null)
            {
                //playerMovement.ApplyBounce();
                // o directamente (más limpio):
                // playerAnimator.transitionAnimation(AnimationState.StateV1);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente AnimatorControllerGJ.");
            }


            // (Opcional) Si quieres que el asset desaparezca al recogerlo:
            // Destroy(gameObject);
        }
    }
}
