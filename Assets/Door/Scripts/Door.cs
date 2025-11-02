using System;
using UnityEngine;
using UnityEngine.Events;


public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entering door");
        // Comprobamos que el objeto que entra es el jugador (opcional)
        if (other.CompareTag("Player"))
        {
            AnimatorControllerGJ player = other.GetComponent<AnimatorControllerGJ>();

            if (player != null)
            {
                if (player.hasKey)
                {
                    GameManager.Instance.ExitGame();
                    Debug.Log("El jugador ahora tiene la llave.");
                }
            }
            else
            {
                Debug.LogWarning("No se encontró el componente PlayerController en el objeto con tag 'Player'.");
            }


            Debug.Log("Jugador entró en el trigger de la puerta.");

        }
    }
}
