using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TerribleTulipBulletCollision : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject spriteObject;
    
    [SerializeField] Vector3 explosionSpriteOffset;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Floors"))
        {
            animator.SetBool("explode", true);
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

            var position = transform.position;
            spriteObject.transform.position = new Vector3(position.x, position.y + 1.65f, position.z);
            
            rb.freezeRotation = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }

    private void Awake()
    {
        Destroy(this, 25f);
    }
}