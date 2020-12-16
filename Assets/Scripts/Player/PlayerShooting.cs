using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private static readonly int Schiet = Animator.StringToHash("schiet");
    private static readonly int SchietDirectionX = Animator.StringToHash("SchietDirectionX");
    private static readonly int SchietDirectionY = Animator.StringToHash("SchietDirectionY");

    public GameObject bulletObj;
    public float shootCooldown;
    [SerializeField] private Animator playerAnimator;

    private float _shootCoolDownTimer;

    private Vector3 _direction;
    private Vector3 _previousValidDirection;
    private bool _canShoot = true;

    private float _lastSchot = 0f;

    private Vector3 _previousPosition;

    private void Update()
    {
        _lastSchot += Time.deltaTime;

        if (_lastSchot > shootCooldown * 2)
        {
            playerAnimator.SetBool(Schiet, false);
        }

        Vector3 prevPos = _previousPosition;
        _previousPosition = transform.position;

        if (prevPos != transform.position)
        {
            return;
        }

        // Simple anti spam timer
        if (!_canShoot)
        {
            _shootCoolDownTimer += Time.deltaTime;
            if (_shootCoolDownTimer > shootCooldown * 2)
            {
                _canShoot = true;
                _shootCoolDownTimer = 0.0f;
            }
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.y = Input.GetAxisRaw("Vertical");

            playerAnimator.SetInteger(SchietDirectionX, (int) _direction.x);
            playerAnimator.SetInteger(SchietDirectionY, (int) _direction.y);

            if (_direction.Equals(new Vector3(0, -1)))
            {
                _direction = Vector3.zero;
            }

            if (!_direction.Equals(Vector3.zero)) _previousValidDirection = _direction;
        }

        if (!_canShoot) return;

        if (Input.GetKeyDown(KeyCode.C) && !_direction.Equals(Vector3.zero))
        {
            Shoot(_direction);
            return;
        }

        if (Input.GetKeyDown(KeyCode.C) && _direction.Equals(Vector3.zero) &&
            !_previousValidDirection.Equals(Vector3.zero))
        {
            Shoot(_previousValidDirection);
            return;
        }
    }

    private void Shoot(Vector3 dir)
    {
        _lastSchot = 0;
        playerAnimator.SetBool(Schiet, true);
        Vector3 spawnPos = transform.position;
        GameObject obj = Instantiate(bulletObj, spawnPos, Quaternion.LookRotation(dir, Vector3.up));
        Destroy(obj, 30);

        // rotate player to the same direction as the bullet.
        Vector3 localScale = transform.localScale;
        if ((int) _direction.x == -1)
        {
            transform.localScale = new Vector3(-Math.Abs(localScale.x), localScale.y, localScale.z);
        }

        if ((int) _direction.x == 1)
        {
            transform.localScale = new Vector3(Math.Abs(localScale.x), localScale.y, localScale.z);
        }
    }
}