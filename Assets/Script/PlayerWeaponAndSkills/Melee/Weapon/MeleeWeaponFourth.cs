/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponFourth : PlayerWeaponBasic
{
    void Start()
    {
        // 무기 정보
        weaponName = "폭발탄";
        weaponExplain = "적에게 두 번의 데미지를 주는 탄환을 발사하는 주포입니다.\n 적중한 탄환은 2초 후 한 번 더 폭발하여,\n탄환 피해량의 20%/30%/40%/50%만큼 추가 피해를 입힙니다.";

        // 레벨 별 수치
        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 5 + i; // 피해량
            projectileSpeed[i] = 60; // 이동속도
            projectileAmount[i] = 4 + i; // 포탄 수
            maxCoolTime[i] = 8; // 쿨타임
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
