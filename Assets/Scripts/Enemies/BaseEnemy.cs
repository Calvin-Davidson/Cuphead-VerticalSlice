using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
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

    protected virtual void Attack()
    {
        return;
    }
}
