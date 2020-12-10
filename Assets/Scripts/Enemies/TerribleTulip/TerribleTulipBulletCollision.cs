using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TerribleTulipBulletCollision : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Floors"))
        {
            GetComponent<Animator>().SetBool("explode", true);
            this.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            bool b = (GetComponent<Rigidbody2D>().constraints & RigidbodyConstraints2D.FreezeAll) !=
                RigidbodyConstraints2D.FreezeAll;
        }
    }
}
