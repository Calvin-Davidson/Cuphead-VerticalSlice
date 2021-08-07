using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player") || other.gameObject.name == "Player")
        {
            OnPickup();
        }
    }

    public virtual void OnPickup()
    {
        
    }
}
