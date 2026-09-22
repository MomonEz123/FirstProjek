using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private Animate animate;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animate>();
    }

    private void Update()
    {
        // Reset input
        movementInput = Vector2.zero;

        // Input menggunakan Unity Input System
        if (Keyboard.current != null)
        {
            // Kiri
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                movementInput.x = -1f;
            }

            // Kanan
            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
            {
                movementInput.x = 1f;
            }

            // Bawah
            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
            {
                movementInput.y = -1f;
            }

            // Atas
            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
            {
                movementInput.y = 1f;
            }
        }

        // Supaya diagonal tidak lebih cepat
        movementInput = movementInput.normalized;

        // Kirim arah horizontal ke sistem animasi
        if (animate != null)
        {
            animate.horizontal = movementInput.x;
        }
    }

    private void FixedUpdate()
    {
        // Gerakkan player menggunakan Rigidbody2D Unity 6.3
        rb.linearVelocity = movementInput * speed;
    }
}