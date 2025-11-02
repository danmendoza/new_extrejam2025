using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public GameObject target; // El jugador
    public float zOffset = -10f; // La cámara suele estar detrás en el eje Z

    void Start()
    {
        AssignTarget(target);
    }
    void LateUpdate()
    {
        if (target != null)
        {
            // La cámara sigue exactamente al jugador
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, zOffset);
        }
    }

    public void AssignTarget(GameObject newTarget)
    {
        if (newTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            target = player;
        }
        else
        {
            target = newTarget;
        }

    }
}