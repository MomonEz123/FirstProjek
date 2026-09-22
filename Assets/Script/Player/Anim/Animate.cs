using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator animator;

    public float horizontal;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
    }

}
