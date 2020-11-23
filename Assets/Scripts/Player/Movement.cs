using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float jumpHeight = 4.5f;
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
            this.transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
            this.transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 currentVel = _rigidbody2D.velocity;
            currentVel.y += jumpHeight;
            _rigidbody2D.velocity = currentVel;
        }
    }
}