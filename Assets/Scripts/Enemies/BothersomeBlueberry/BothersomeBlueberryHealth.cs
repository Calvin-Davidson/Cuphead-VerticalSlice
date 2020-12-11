using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothersomeBlueberryHealth : EnemyHealth
{
    protected override void Die()
    {
        Destroy(this.gameObject);
    }
}
