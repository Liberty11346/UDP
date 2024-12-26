/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponSecond : PlayerWeaponBasic
{
    void Start()
    {
        // 무기 정보
        weaponName = "부식탄";
        weaponExplain = "적의 장갑과 외부 보호 장치를 부식시키는 탄환을 발사하는 주포입니다.\n 적중한 적에게 가하는 피해량이 3초 동안 50% 증가합니다.";

        // 레벨 별 수치
        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 2; // 피해량
            projectileSpeed[i] = 75; // 이동속도
            projectileAmount[i] = 5; // 포탄 수
            maxCoolTime[i] = 6 - i; // 쿨타임
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
            // 산탄을 위한 랜덤한 위치와 각도 생성
            float randomPos = Random.Range(-5, 5);
            Vector3 randomRot = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            // 생성한 랜덤값을 바탕으로 발사
            Vector3 firePos = new Vector3(playerCameraTransform.position.x + randomPos, playerCameraTransform.position.y + randomPos, playerCameraTransform.position.z);
            GameObject firedProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation * Quaternion.Euler(randomRot));
            firedProjectile.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);
        }
    }
}
