using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    void Update()
    {
        var transform1 = transform;
        transform1.position += transform1.forward * (Time.deltaTime * 5);
    }
}
