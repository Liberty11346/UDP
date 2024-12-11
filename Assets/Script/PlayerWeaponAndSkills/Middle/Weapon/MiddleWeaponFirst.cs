<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
>>>>>>> parent of fb8c72a (Revert "빨간버튼")
using UnityEngine;

public class MiddleWeaponFirst : PlayerWeaponBasic
{
    // Start is called before the first frame update
    void Start()
    {
        weaponName = "기본공격";
        weaponExplain = "공격시 기본 포탄을 1발 발사합니다.";

        currentLevel = 1;

        for(int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 25 + i * 10;
<<<<<<< HEAD
            projectileSpeed[i] = 60;
=======
            projectileSpeed[i] = 75;
>>>>>>> main
            projectileAmount[i] = 1;
            maxCoolTime[i] = 1;
        }

        GetCameraTransform();
        projectile =  Resources.Load<GameObject>("Middle/MiddleBullet1");
    }

    // Update is called once per frame
   public override void Fire()
    {
        BasicWeapon();
    }

    void BasicWeapon()
    {
        Vector3 firePos = new Vector3(playerCameraTransform.position.x, playerCameraTransform.position.y, playerCameraTransform.position.z);
        GameObject fireProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation);
        fireProjectile.GetComponent<PlayerBulletBasic>().Clone(this ,currentLevel);
    }
}
