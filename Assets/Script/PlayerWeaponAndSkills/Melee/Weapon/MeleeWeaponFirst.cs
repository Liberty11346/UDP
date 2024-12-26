/*
����                     ����
 
 �ڵ� �ۼ�: 5645866 ������

����                     ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponFirst : PlayerWeaponBasic
{
    void Start()
    {
        // ���� ����
        weaponName = "�⺻ź";
        weaponExplain = "�⺻�� �Ǵ� ��ź ������ �����Դϴ�.\n ������ ���������� ���ط��� �߻�Ǵ� ��ź�� ���� �����մϴ�.";

        // ���� �� ��ġ
        for(int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 3 + i; // ���ط�
            projectileSpeed[i] = 60; // �̵��ӵ�
            projectileAmount[i] = 5 + i; // ��ź ��
            maxCoolTime[i] = 3; // ��Ÿ��
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Melee/MeleeBulletFirst");
    }

    public override void Fire()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        int numProjectile = currentLevel + 4;
        for (int i = 0; i < numProjectile; i++)
        {
            // ��ź�� ���� ������ ��ġ�� ���� ����
            float randomPos = Random.Range(-5, 5);
            Vector3 randomRot = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            // ������ �������� �������� �߻�
            Vector3 firePos = new Vector3(playerCameraTransform.position.x + randomPos, playerCameraTransform.position.y + randomPos, playerCameraTransform.position.z);
            GameObject firedProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation * Quaternion.Euler(randomRot));
            firedProjectile.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);
        }
    }
}

