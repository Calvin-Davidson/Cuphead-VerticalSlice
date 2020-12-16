using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestoryer : MonoBehaviour
{
    public void DestroyWithParent()
    {    
        print("destroy");
        Destroy(this.transform.parent.gameObject);
    }
}