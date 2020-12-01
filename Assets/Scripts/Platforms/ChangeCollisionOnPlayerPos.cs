using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollisionOnPlayerPos : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform playerFeetTransform;
    [SerializeField] private float minYOffset;
    private float _minY;
    private Vector3 _playerFeetPos;

    private void Start()
    {
        // Initialize the height based on object position, collider position, collider size and an offset
        _minY = boxCollider.transform.position.y + boxCollider.offset.y + boxCollider.size.y + minYOffset;
    }

    void Update()
    {
        _playerFeetPos = playerFeetTransform.position;
        boxCollider.enabled = (_playerFeetPos.y > _minY);
        if (Input.GetKey(KeyCode.S))
        {
            boxCollider.enabled = false;
        }
    }
}
