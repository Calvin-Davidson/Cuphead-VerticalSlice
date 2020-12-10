using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerribleTulip : BaseEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    protected override void Attack()
    {
        animator.Play("TerribleTulip_Shoot");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); //Spawn bullet at own position and using own (identity) rotation
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>(); //Get rigidbody from the bullet prefab
        float randomX = Random.Range(0, 2) == 0 ? Random.Range(-0.5f, -0.9f) : Random.Range(0.5f, 0.9f); //Get a random value that determines at what speed the bullet flies left, or right
        rigidbody2D.velocity = new Vector2(randomX, 8.5f); //Apply random horizontal speed and 8.5f vertical speed to the bullet
        Destroy(bullet, 10f); //Destroy the bullet after 10 seconds to prevent infinite loops / memory leakage
    }
}
