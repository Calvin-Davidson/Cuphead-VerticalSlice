using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpStrength = 4.5f;
    [SerializeField] private Animator animator;
    
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) //If both left and right are pressed or neither are pressed
        {
            animator.Play("Cuphead_Idle");
            return;
        }
        
        Vector3 localScale = transform.localScale;
        
        if (Input.GetKey(KeyCode.A)) //Move left
        {
            this.transform.position += new Vector3(-movementSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-Math.Abs(localScale.x), localScale.y, localScale.z);
            //animator.Play("Cuphead_Walk");
        }
        
        if (Input.GetKey(KeyCode.D)) //Move right
        {
            this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(Math.Abs(localScale.x), localScale.y, localScale.z);
            //animator.Play("Cuphead_Walk");
        }

        if (Input.GetKeyDown(KeyCode.Space)) //Jump
        {
            Vector3 currentVel = _rigidbody2D.velocity;
            currentVel.y += jumpStrength;
            _rigidbody2D.velocity = currentVel;
        }
    }
}