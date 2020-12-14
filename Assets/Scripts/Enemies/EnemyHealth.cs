using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private String deathAnimationBooleanVariable = "none";
    
    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (deathAnimationBooleanVariable.Equals("none")) return;
        animator.SetBool("die", true);
    }
}
