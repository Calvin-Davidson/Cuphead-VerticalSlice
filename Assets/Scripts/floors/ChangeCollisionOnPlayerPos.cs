using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollisionOnPlayerPos : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Transform playerFeetTransform;
    [SerializeField] private Vector3 playerFeetPos;
    [SerializeField] float yHeight;
    
    // Update is called once per frame
    void Update()
    {
        playerFeetPos = playerFeetTransform.position;
        collider.enabled = !(playerFeetPos.y < yHeight);

        if (Input.GetKey(KeyCode.S))
        {
            collider.enabled = false;
        }
    }
}
