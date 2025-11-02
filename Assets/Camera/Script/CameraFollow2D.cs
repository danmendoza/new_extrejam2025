using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // El jugador
    public float zOffset = -10f; // La cámara suele estar detrás en el eje Z

    void LateUpdate()
    {
        if (target != null)
        {
            // La cámara sigue exactamente al jugador
            transform.position = new Vector3(target.position.x, target.position.y, zOffset);
        }
    }
}