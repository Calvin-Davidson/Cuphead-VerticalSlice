using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 currentVel = _rigidbody2D.velocity;
            currentVel.y += 4.5f;
            _rigidbody2D.velocity = currentVel;
        }
    }
}