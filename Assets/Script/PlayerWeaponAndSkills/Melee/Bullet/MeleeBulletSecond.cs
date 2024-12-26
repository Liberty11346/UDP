/*
����                     ����
 
 �ڵ� �ۼ�: 5645866 ������

����                     ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBulletSecond : PlayerBulletBasic
{
    // ������ ������ 3�ʵ��� ���ϴ� ���ط��� 50% ����
    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.defense = -50;
        StartCoroutine(enemy.RecoverDefense(3));
    }
}
