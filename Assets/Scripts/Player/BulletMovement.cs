using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 _forward;

    private void Awake()
    {
        _forward = transform.forward;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }

    void Update()
    {
        transform.position += _forward * (Time.deltaTime * 5);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
            return;
        }
        
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
        
        Destroy(this.gameObject);
    }
}
