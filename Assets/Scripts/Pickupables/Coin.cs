using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickupable
{
    private readonly int _pickUp = Animator.StringToHash("pickedUp");

    
    [SerializeField] private Vector3 newScale;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private CapsuleCollider2D collider2d;
    public override void OnPickup()
    {
        Destroy(this.gameObject);
        print("pickedUp");
        collider2d.enabled = false;
        GetComponent<Animator>().SetBool(_pickUp, true);
        var trans = transform;
        trans.localScale = newScale;
        trans.position += positionOffset;
    }
}
