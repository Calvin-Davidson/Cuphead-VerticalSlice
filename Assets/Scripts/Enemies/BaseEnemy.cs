using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float attackSpeed; //Delay between attacks
    [SerializeField] protected bool canAttack = true;
    [SerializeField] protected bool attackAnimationIsDone = true;
    [SerializeField] private bool useAttackRange = false;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject player;
    private float _attackTimer;

    private void Update()
    {
        if(player != null && useAttackRange)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            canAttack = distance <= attackRange;
        }
        if (canAttack && attackAnimationIsDone)
        {
            Debug.Log("attacking");
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= attackSpeed)
            {
                Attack();
                _attackTimer -= attackSpeed; //Subtracts attackspeed from the attack timer instead of setting it to 0 to ensure consistent firerate when frame stutters happen
            }
        }
    }

    protected abstract void Attack();
}
