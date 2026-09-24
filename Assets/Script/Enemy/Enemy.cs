
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform targetDestination;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;

    private GameObject targetGameObject;
    private Rigidbody2D rgdb2d;

    private void Awake()
    {
        rgdb2d = GetComponent<Rigidbody2D>();

        if (targetDestination != null)
        {
            targetGameObject = targetDestination.gameObject;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdb2d.linearVelocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking The MC");
    }
}
