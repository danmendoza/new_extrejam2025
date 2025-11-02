using UnityEngine;

using UnityEngine;

public class Pants : Flotante
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Busca el componente AnimatorControllerGJ en el jugador
            AnimatorControllerGJ playerAnimator = other.GetComponent<AnimatorControllerGJ>();

            if (playerAnimator != null)
            {
                Debug.Log("Jugador recogió los pantalones → cambiando a StateV1");
                playerAnimator.hasPants = true;
                playerAnimator.transitionAnimation(AnimationState.StateV1);
                Destroy(gameObject);
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

