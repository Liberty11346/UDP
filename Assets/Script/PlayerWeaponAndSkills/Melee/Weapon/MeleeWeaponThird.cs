using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponThird : PlayerWeaponBasic
{
    void Start()
    {
        // 무기 정보
        weaponName = "초강력 끈끈이";
        weaponExplain = "플레이어가 바라보는 방향으로 20/30/40/50의 속도로 초강력 끈끈이를 던지며,\n 적중 시 플레이어가 60의 속도로 적중한 적에게 이동합니다.";

        // 레벨 별 수치
        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 10; // 피해량
            projectileSpeed[i] = 60 + (i * 30); // 이동속도
            projectileAmount[i] = 1; // 포탄 수
            maxCoolTime[i] = 6 - i; // 쿨타임
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
