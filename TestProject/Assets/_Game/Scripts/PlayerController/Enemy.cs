using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Death()
    {
        animator.SetBool("Death", true);
    }
}
