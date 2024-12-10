using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponThird : PlayerWeaponBasic
{
    void Start()
    {
        // ���� ����
        weaponName = "�ʰ��� ������";
        weaponExplain = "�÷��̾ �ٶ󺸴� �������� 20/30/40/50�� �ӵ��� �ʰ��� �����̸� ������,\n ���� �� �÷��̾ 60�� �ӵ��� ������ ������ �̵��մϴ�.";

        // ���� �� ��ġ
        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 10; // ���ط�
            projectileSpeed[i] = 60 + (i * 30); // �̵��ӵ�
            projectileAmount[i] = 1; // ��ź ��
            maxCoolTime[i] = 6 - i; // ��Ÿ��
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Melee/MeleeBulletThird");
    }

    public override void Fire()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        Vector3 firePos = new Vector3(playerCameraTransform.position.x, playerCameraTransform.position.y, playerCameraTransform.position.z);
        GameObject firedProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation);
        firedProjectile.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);
    }
}
