/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBulletSecond : PlayerBulletBasic
{
    // 적중한 적에게 3초동안 가하는 피해량이 50% 증가
    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.defense = -50;
        StartCoroutine(enemy.RecoverDefense(3));
    }
}
