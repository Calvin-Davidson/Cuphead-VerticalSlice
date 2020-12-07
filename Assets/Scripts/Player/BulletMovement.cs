using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 _forward;

    private void Awake()
    {
        _forward = transform.forward;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }

    void Update()
    {
        transform.position += _forward * (Time.deltaTime * 5);
    }
}
