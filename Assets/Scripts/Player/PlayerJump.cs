using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float currentJumpStrength;
    [SerializeField] private float primaryJumpStrength;
    [SerializeField] private float maxSecondaryJumpStrength;
    [SerializeField] private float secondaryJumpFalloffSpeed;
    [SerializeField] private bool _canJump = true;
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 currentVel = _rigidbody2D.velocity;
        if (Input.GetKey(KeyCode.Space))
        {
            if (_canJump)
            {
                currentJumpStrength = maxSecondaryJumpStrength;
                currentVel.y += primaryJumpStrength;
                _canJump = false;
            }
            else
            {
                currentJumpStrength = Mathf.Lerp(currentJumpStrength, 0, secondaryJumpFalloffSpeed * Time.deltaTime);
                currentJumpStrength = Mathf.Round(currentJumpStrength * 1000f) / 1000f;
            }
        }
        else
        {
            currentJumpStrength = 0;
        }
        currentVel.y += currentJumpStrength * Time.deltaTime * 144; //account for 144FPS
        _rigidbody2D.velocity = currentVel;
    }

    public void SetCanJump(bool value)
    {
        _canJump = value;
        if (value) currentJumpStrength = 0;
    }
}