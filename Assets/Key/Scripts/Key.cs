using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entering key");
        // Comprobamos que el objeto que entra es el jugador (opcional)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador cogio llave.");
            AnimatorControllerGJ player = other.GetComponent<AnimatorControllerGJ>();

            if (player != null)
            {
                // Cambiar la variable hasKey a true
                player.hasKey = true;
                Debug.Log("El jugador ahora tiene la llave.");
            }
            else
            {
                Debug.LogWarning("No se encontró el componente PlayerController en el objeto con tag 'Player'.");
            }

            // Buscar el GameManager y llamar a su método
            GameManager.Instance.PrintHello();
            Destroy(gameObject);
        }
    }
}
