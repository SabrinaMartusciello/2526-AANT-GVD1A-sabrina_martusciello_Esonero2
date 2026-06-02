using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Velocità")]
    public float moveSpeed = 10f;
    public float turnSpeed = 45f;

    [Header("Effetto onde")]
    public float waveHeight = 0.15f;
    public float waveFrequency = 1.2f;

    private Rigidbody _rb;
    private float _startY;

    void Start()
    {
        // Prende il riferimento al Rigidbody
        _rb = GetComponent<Rigidbody>();
        _startY = transform.position.y;
    }

    void FixedUpdate()
    {
        // FixedUpdate è il posto giusto per la fisica

        // --- MOVIMENTO AVANTI / INDIETRO ---
        // Input.GetAxis("Vertical") restituisce:
        //   +1 quando premi W o freccia Su
        //   -1 quando premi S o freccia Giù
        float moveInput = Input.GetAxis("Vertical");

        Vector3 forwardForce = transform.forward
                               * moveInput
                               * moveSpeed;

        _rb.AddForce(forwardForce, ForceMode.Acceleration);

        // --- ROTAZIONE SINISTRA / DESTRA ---
        // Input.GetAxis("Horizontal") restituisce:
        //   +1 quando premi D o freccia Destra
        //   -1 quando premi A o freccia Sinistra
        float turnInput = Input.GetAxis("Horizontal");

        // Ruota solo se la barca si sta muovendo
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            float turn = turnInput
                         * turnSpeed
                         * Time.fixedDeltaTime;

            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            _rb.MoveRotation(_rb.rotation * turnRotation);
        }

        // --- EFFETTO ONDE ---
        // Fa oscillare dolcemente la barca sull'asse Y
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveHeight;
        Vector3 pos = _rb.position;
        pos.y = _startY + wave;
        _rb.MovePosition(new Vector3(pos.x, pos.y, pos.z));
    }
}