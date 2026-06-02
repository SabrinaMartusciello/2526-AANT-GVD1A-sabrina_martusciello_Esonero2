using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Velocità")]
    public float moveSpeed = 5f;
    public float turnSpeed = 60f;

    [Header("Effetto onde")]
    public float waveHeight = 0.15f;
    public float waveFrequency = 1.2f;

    private Rigidbody _rb;
    private float _startY;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startY = transform.position.y;

        // Blocca la rotazione su X e Z così non si impenna
        _rb.constraints = RigidbodyConstraints.FreezeRotationX
                        | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // --- MOVIMENTO AVANTI / INDIETRO ---
        float moveInput = Input.GetAxis("Vertical");
        Vector3 targetVelocity = transform.forward * moveInput * moveSpeed;

        // Mantieni la velocità Y attuale (gravità)
        targetVelocity.y = _rb.linearVelocity.y;

        // Imposta direttamente la velocità (no accumulo di forza)
        _rb.linearVelocity = targetVelocity;

        // --- ROTAZIONE SINISTRA / DESTRA ---
        float turnInput = Input.GetAxis("Horizontal");
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);

        // --- EFFETTO ONDE ---
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveHeight;
        Vector3 pos = _rb.position;
        pos.y = _startY + wave;
        _rb.MovePosition(new Vector3(pos.x, pos.y, pos.z));
    }
}