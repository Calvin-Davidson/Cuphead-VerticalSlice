using System.Security.Cryptography;
using UnityEngine;

public class TerribleTulipBulletCollision : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject spriteObject;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Floors"))
        {
            animator.SetBool("explode", true);
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

            var position = transform.position;
            spriteObject.transform.position = new Vector3(position.x, position.y + 1.65f, position.z);

            rb.freezeRotation = true;
            gameObject.GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }
}