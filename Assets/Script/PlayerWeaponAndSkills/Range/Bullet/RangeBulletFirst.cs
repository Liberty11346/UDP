using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBulletFirst : PlayerBulletBasic
{
    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.defense = -20;
        StartCoroutine(enemy.RecoverDefense(4));
    }
}