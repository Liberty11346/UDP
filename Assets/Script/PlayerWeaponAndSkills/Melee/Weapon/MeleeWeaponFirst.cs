/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponFirst : PlayerWeaponBasic
{
    void Start()
    {
        // 무기 정보
        weaponName = "기본탄";
        weaponExplain = "기본이 되는 산탄 형태의 주포입니다.\n 레벨이 높아질수록 피해량과 발사되는 포탄의 수가 증가합니다.";

        // 레벨 별 수치
        for(int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 3 + i; // 피해량
            projectileSpeed[i] = 60; // 이동속도
            projectileAmount[i] = 5 + i; // 포탄 수
            maxCoolTime[i] = 3; // 쿨타임
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

