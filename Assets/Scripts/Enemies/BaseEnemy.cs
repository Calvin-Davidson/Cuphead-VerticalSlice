using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float attackSpeed; //Delay between attacks
    private float _attackTimer;
    private void Update()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= attackSpeed)
        {
            Attack();
            _attackTimer -= attackSpeed; //Subtracts attackspeed from the attack timer instead of setting it to 0 to ensure consistent firerate when frame stutters happen
        }
    }

    protected abstract void Attack();
}
