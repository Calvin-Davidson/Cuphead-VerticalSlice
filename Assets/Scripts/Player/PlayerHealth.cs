using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private Animator _animator;
    [SerializeField] private String _deathAnimationName;

    [SerializeField] private MonoBehaviour[] disableOnDie;
    public void TakeDamage(int dmg)
    {
        Debug.Log("dmg");
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        foreach (var monoBehaviour in disableOnDie)
        {
            monoBehaviour.enabled = false;
        }
        
        print("de speler is dood gegaan");
        _animator.Play(_deathAnimationName);
    }
}
