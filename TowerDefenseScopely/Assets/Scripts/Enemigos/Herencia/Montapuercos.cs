using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Montapuercos : Enemy
{
    protected void MoveTowardsTarget()
    {
        if (currentTarget != null)
        {
            base.MoveTowardsTarget(currentTarget.position);
        }
    }
}
