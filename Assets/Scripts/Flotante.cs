using UnityEngine;

public class Flotante : MonoBehaviour
{
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
}