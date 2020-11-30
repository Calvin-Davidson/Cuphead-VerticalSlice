using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollisionOnPlayerPos : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider; //Collider to affect
    [SerializeField] private Transform playerFeetTransform; //What transform to use
    [SerializeField] private float minYOffset; //At what minimum height to enable the collider
    private float _minY;
    private Vector3 _playerFeetPos; //Position of the transform component

    private void Start()
    {
        _minY = boxCollider.transform.position.y + boxCollider.offset.y + boxCollider.size.y + minYOffset; //Initialize the height based on object position, collider position, collider size and an offset
    }

    void Update()
    {
        _playerFeetPos = playerFeetTransform.position; //Update the position
        Debug.Log(_playerFeetPos.y);
        boxCollider.enabled = (_playerFeetPos.y > _minY); //Collider is enabled if the player's y is >= minY

        if (Input.GetKey(KeyCode.S))
        {
            boxCollider.enabled = false; //Disable collider collision when pressing down
        }
    }
}
