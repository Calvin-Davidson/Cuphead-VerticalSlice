using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private Animator animator;
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) //If both left and right are pressed or neither are pressed
        {
            animator.Play("Cuphead_Idle");
            return;
        }
        
        animator.Play("Cuphead_Walk");

        Vector3 localScale = transform.localScale;
        
        if (Input.GetKey(KeyCode.A)) //Move left
        {
            this.transform.position += new Vector3(-movementSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-Math.Abs(localScale.x), localScale.y, localScale.z);
        }
        
        if (Input.GetKey(KeyCode.D)) //Move right
        {
            this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(Math.Abs(localScale.x), localScale.y, localScale.z);
        }
    }
}