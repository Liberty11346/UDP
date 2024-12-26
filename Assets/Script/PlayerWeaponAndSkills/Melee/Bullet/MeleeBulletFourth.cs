/*
����                     ����
 
 �ڵ� �ۼ�: 5645866 ������

����                     ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBulletFourth : PlayerBulletBasic
{
    // ������ źȯ�� 2�� �� �� �� �� �����Ͽ�, źȯ ���ط��� 20%/30%/40%/50%��ŭ �߰� ����
    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        StartCoroutine(Explosion(enemy));
    }

    // 2�� �� �߰� ���� ���ظ� �����ϴ� �ڷ�ƾ
    private IEnumerator Explosion(Enemy enemy)
    {
        // 2�� ���
        yield return new WaitForSeconds(2f);

        // ���� ��������� �߰� ���� ����
        if (enemy != null)
        {
            enemy.currentHealth -= attackDamage * explosionDamage();
        }
    }

    // ���� �� ���ط��� ��ȯ�ϴ� �Լ�
    private float explosionDamage()
    {
        switch (currentLevel)
        {
            case 1: return 0.2f; // ���� 1 ���ط�: 20%
            case 2: return 0.3f; // ���� 2 ���ط�: 30%
            case 3: return 0.4f; // ���� 3 ���ط�: 40%
            case 4: return 0.5f; // ���� 4 ���ط�: 50%
            default: return 0.2f; //  �⺻ ���ط�: 20%
        }
    }

}
