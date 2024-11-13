using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBulletFirst : PlayerBulletBasic
{
    // 적중한 적의 방어력을 4초동안 20% 감소
    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.defense = -20;
        StartCoroutine(enemy.RecoverDefense(4));
    }
}