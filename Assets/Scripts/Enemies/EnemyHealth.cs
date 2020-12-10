using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private String deathAnimationName = "none";
    
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
        animator.Play(deathAnimationName);
    }
}
