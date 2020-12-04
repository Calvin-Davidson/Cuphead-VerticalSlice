using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletObj;
    public float shootCooldown;
    private float _shootCoolDownTimer;
    
    private Camera _cam;
    private Vector3 _direction;
    private Vector3 _previousValidDirection;
    private bool _canShoot = true;
    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (!_canShoot)
        {
            _shootCoolDownTimer += Time.deltaTime;
            if (_shootCoolDownTimer > shootCooldown)
            {
                _canShoot = true;
                _shootCoolDownTimer = 0.0f;
            }
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.y = Input.GetAxisRaw("Vertical");

            if (_direction.Equals(new Vector3(0, -1)))
            {
                _direction = Vector3.zero;
            }

            if (!_direction.Equals(Vector3.zero)) _previousValidDirection = _direction;
        }

        if (!_canShoot) return;
        
        if (Input.GetKeyDown(KeyCode.C) && !_direction.Equals(Vector3.zero))
        {
            GameObject obj = Instantiate(bulletObj, transform.position, Quaternion.LookRotation(_direction, Vector3.up));
            Destroy(obj, 30);
        }

        if (Input.GetKeyDown(KeyCode.C) && _direction.Equals(Vector3.zero) && !_previousValidDirection.Equals(Vector3.zero))
        {
            GameObject obj = Instantiate(bulletObj, transform.position, Quaternion.LookRotation(_previousValidDirection, Vector3.up));
            Destroy(obj, 30);
        }
    }
}