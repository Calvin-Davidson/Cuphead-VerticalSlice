using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    private float _attackTimer;
    private void Update()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer > attackSpeed)
        {
            Attack();
            _attackTimer = 0;
        }
    }

    protected abstract void Attack();
}
