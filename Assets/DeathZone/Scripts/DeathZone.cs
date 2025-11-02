using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Busca el componente AnimatorControllerGJ en el jugador
            AnimatorControllerGJ playerAnimator = other.GetComponent<AnimatorControllerGJ>();

            if (playerAnimator != null)
            {
                Debug.Log("Jugador recogió los guantes → cambiando a StateV2");
                playerAnimator.isDead = true;
                playerAnimator.transitionAnimation(AnimationState.StateDead);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente AnimatorControllerGJ.");
            }


        }
    }
}
