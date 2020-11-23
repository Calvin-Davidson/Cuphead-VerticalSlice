using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerribleTulip : BaseEnemy
{
    [SerializeField] private GameObject bulletPref;
    protected override void Attack()
    {
        GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();

        float randomX = Random.Range(0, 2) == 0 ? Random.Range(-0.5f, -0.9f) : Random.Range(0.5f, 0.9f);
        rigidbody2D.velocity = new Vector2(randomX, 8.5f);
        
        Destroy(bullet, 10f);
    }
}
