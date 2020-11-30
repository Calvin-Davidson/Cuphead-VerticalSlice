using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCollision : MonoBehaviour
{
    [SerializeField] private PlayerJump playerJump;

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerJump.SetCanJump(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerJump.SetCanJump(false);
    }
}
