/*
����                     ����
 
 �ڵ� �ۼ�: 5645866 ������

����                     ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponFourth : PlayerWeaponBasic
{
    void Start()
    {
        // ���� ����
        weaponName = "����ź";
        weaponExplain = "������ �� ���� �������� �ִ� źȯ�� �߻��ϴ� �����Դϴ�.\n ������ źȯ�� 2�� �� �� �� �� �����Ͽ�,\nźȯ ���ط��� 20%/30%/40%/50%��ŭ �߰� ���ظ� �����ϴ�.";

        // ���� �� ��ġ
        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 5 + i; // ���ط�
            projectileSpeed[i] = 60; // �̵��ӵ�
            projectileAmount[i] = 4 + i; // ��ź ��
            maxCoolTime[i] = 8; // ��Ÿ��
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Melee/MeleeBulletFourth");
    }

    public override void Fire()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        int numProjectile = currentLevel + 3;
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
