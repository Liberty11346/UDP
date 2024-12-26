/*
����                     ����
 
 �ڵ� �ۼ�: 5645866 ������

����                     ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponSecond : PlayerWeaponBasic
{
    void Start()
    {
        // ���� ����
        weaponName = "�ν�ź";
        weaponExplain = "���� �尩�� �ܺ� ��ȣ ��ġ�� �νĽ�Ű�� źȯ�� �߻��ϴ� �����Դϴ�.\n ������ ������ ���ϴ� ���ط��� 3�� ���� 50% �����մϴ�.";

        // ���� �� ��ġ
        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 2; // ���ط�
            projectileSpeed[i] = 75; // �̵��ӵ�
            projectileAmount[i] = 5; // ��ź ��
            maxCoolTime[i] = 6 - i; // ��Ÿ��
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Melee/MeleeBulletSecond");
    }

    public override void Fire()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        for (int i = 0; i < 4; i++)
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
