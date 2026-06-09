using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float amplitude = 1f;
    public float frequency = 1f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + Vector3.up * offsetY;
    }
}
