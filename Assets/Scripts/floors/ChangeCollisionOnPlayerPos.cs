using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollisionOnPlayerPos : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform playerFeetTransform;
    [SerializeField] private Vector3 playerFeetPos;
    [SerializeField] float yHeight;
    
    void Update()
    {
        playerFeetPos = playerFeetTransform.position;
        boxCollider.enabled = !(playerFeetPos.y < yHeight);

        if (Input.GetKey(KeyCode.S))
        {
            boxCollider.enabled = false;
        }
    }
}
