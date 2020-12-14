using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCollision : MonoBehaviour
{
    [SerializeField] private PlayerJump playerJump;

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerJump.SetCanJump(true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        playerJump.SetCanJump(false);
    }
}
