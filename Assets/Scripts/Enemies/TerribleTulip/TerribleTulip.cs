using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerribleTulip : BaseEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector3 bulletOffset;
    protected override void Attack()
    {
        animator.SetBool("shoot", true);
        GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity); 
        
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>(); 
        
        float randomX = Random.Range(0, 2) == 0 ? Random.Range(-0.5f, -0.9f) : Random.Range(0.5f, 0.9f);
        rigidbody2D.velocity = new Vector2(randomX, 8.5f); 
        
        Destroy(bullet, 10f);
    }


    public void setCanAttackTrue()
    {
        animator.SetBool("shoot", false);
        this.attackAnimationIsDone = true;
    }
}
