using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

using UnityEngine;

//작성자 : 5702341 김기석

public class MiddleWeaponSecond : PlayerWeaponBasic
{
    private MiddleWeaponThird middleWeapon;  // MiddleWeaponThird에 대한 참조를 저장할 변수

    void Start()
    {
        weaponName = "다크매터";
        weaponExplain = "조준한 방향으로 암흑물질을 발사하고 적 적중 시,\n 반지름 20범위 이내의 적들의 암흑물질의 중심으로 끌어당깁니다\n 적을 끌어 당긴 후 암흑물질은 그 자리에 4초동안 남아 적들의 이동속도를 둔화 시킵니다.";

        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 25 + i * 5;
            projectileSpeed[i] = 15;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 11;
            //레벨당 슬로우
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Middle/MiddleBullet2");

        // MiddleWeaponThird를 찾고, trackedBullet을 설정
        middleWeapon = FindObjectOfType<MiddleWeaponThird>();  // 씬에 있는 MiddleWeaponThird를 찾음

    }

    public override void Fire()
    {
        BlackMatter();
    }

    void BlackMatter()
    {
        Vector3 firePos = new Vector3(playerCameraTransform.position.x + 10, playerCameraTransform.position.y, playerCameraTransform.position.z + 10);
        GameObject fireProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation);
        fireProjectile.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);

        
        if (middleWeapon != null)
        {
            // 여기서 MiddleBullet2 객체를 생성
            MiddleBullet2 middleBullet = fireProjectile.GetComponent<MiddleBullet2>();

            // 생성된 MiddleBullet2 객체를 MiddleWeaponThird에 전달
            middleWeapon.SetTrackedBullet(middleBullet);
        }
        else
        {
           // Debug.LogWarning("MiddleWeaponThird not found in the scene.");
        }
    }
}

