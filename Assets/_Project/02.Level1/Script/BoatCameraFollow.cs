using UnityEngine;

public class BoatCameraFollow : MonoBehaviour
{
    [Header("Target da seguire")]
    public Transform target;

    [Header("Distanza dalla barca")]
    public float distance = 12f;
    public float height = 6f;

    [Header("Sensibilità mouse")]
    public float mouseSensitivity = 3f;

    [Header("Limiti verticali (gradi)")]
    public float minVerticalAngle = 5f;
    public float maxVerticalAngle = 80f;

    [Header("Fluidità")]
    public float smoothing = 5f;

    private float _yaw;
    private float _pitch;

    void Start()
    {
        if (target == null) return;

        // Parte con la rotazione della barca
        _yaw = target.eulerAngles.y;
        _pitch = 20f;

        // *** SNAP INIZIALE ***
        // Calcola subito la posizione corretta e teletrasporta la camera lì
        // senza aspettare lo smoothing — risolve il problema della posizione lontana
        Quaternion startRotation = Quaternion.Euler(_pitch, _yaw, 0f);
        Vector3 startPosition = target.position
                                - startRotation * Vector3.forward * distance
                                + Vector3.up * height;

        transform.position = startPosition;
        transform.rotation = startRotation;

        // Cursore
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        _yaw   += Input.GetAxis("Mouse X") * mouseSensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _pitch  = Mathf.Clamp(_pitch, minVerticalAngle, maxVerticalAngle);

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        Vector3 desiredPosition = target.position
                                  - rotation * Vector3.forward * distance
                                  + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position, desiredPosition,
            smoothing * Time.deltaTime
        );
        transform.rotation = Quaternion.Slerp(
            transform.rotation, rotation,
            smoothing * Time.deltaTime
        );

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}