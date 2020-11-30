using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float maxJumpStrength;
    [SerializeField] private float currentJumpStrength;
    [SerializeField] private float lerpSpeed;

    
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private bool _canJump = true;
    private bool _hasSpacePressed;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 currentVel = _rigidbody2D.velocity;
        _hasSpacePressed = Input.GetKey(KeyCode.Space);

        if (_hasSpacePressed && _canJump)
        {
            currentJumpStrength = maxJumpStrength;
            currentVel.y += currentJumpStrength;
        }
        
        if (_hasSpacePressed)
        {
            currentJumpStrength = Mathf.Lerp(currentJumpStrength, 0, lerpSpeed * Time.deltaTime);
        }
        else
        {
            currentJumpStrength = 0;
        }
        currentVel.y += currentJumpStrength;
        _rigidbody2D.velocity = currentVel;
    }

    public void SetCanJump(bool value)
    {
        _canJump = value;
        if (value) currentJumpStrength = 0;
    }
}