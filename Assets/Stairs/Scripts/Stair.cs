using System;
using UnityEngine;
using UnityEngine.Events;


public class Stair : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entering stair");
        // Comprobamos que el objeto que entra es el jugador (opcional)
        if (other.CompareTag("Player"))
        {
            AnimatorControllerGJ player = other.GetComponent<AnimatorControllerGJ>();

            if (player != null)
            {
                player.isOnStairs = true;
                Debug.Log("El jugador ahora tiene la llave.");
            }
            else
            {
                Debug.LogWarning("No se encontró el componente PlayerController en el objeto con tag 'Player'.");
            }


            Debug.Log("Jugador entró en el trigger de la puerta.");

            // Buscar el GameManager y llamar a su método
            GameManager.Instance.PrintHello();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            AnimatorControllerGJ player = other.GetComponent<AnimatorControllerGJ>();

            if (player != null)
            {
                player.isOnStairs = false;
            }
            else
            {
                Debug.LogWarning("No se encontró el componente PlayerController en el objeto con tag 'Player'.");
            }
        }
    }
}
