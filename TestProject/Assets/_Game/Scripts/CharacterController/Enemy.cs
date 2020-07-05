using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Unit
{
    [SerializeField] private CharacterStats stats;

    Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        Setup();
        Health.DieEvent += EnemyDeath;
    }

    private void Setup()
    {
        Health.Health = stats.Health;
        Health.Protection = stats.Protection;
    }

    private  void EnemyDeath()
    {
        Destroy(GetComponent<BoxCollider>());
        animator.SetBool("Death", true);
    }
}
