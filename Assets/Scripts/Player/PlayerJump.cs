using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
    [SerializeField] private float maxJumpStrength;
    [SerializeField] private BoxCollider2D jumpCollider;

    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float currentJumpStrength;
    private bool _canJump = true;
    private bool _hasSpacePressed;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _hasSpacePressed = Input.GetKey(KeyCode.Space);

        if (_hasSpacePressed && _canJump)
        {
            currentJumpStrength = maxJumpStrength;
        }
        
        if (_hasSpacePressed)
        {
            currentJumpStrength = Mathf.Lerp(currentJumpStrength, 0, 10 * Time.deltaTime);
        }
        else
        {
            currentJumpStrength = 0;
        }

        Vector3 currentVel = _rigidbody2D.velocity;
        currentVel.y += currentJumpStrength;
        _rigidbody2D.velocity = currentVel;
    }

    public void SetCanJump(bool value)
    {
        _canJump = value;
    }
}