using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;

public class FirstWeapon_1 : PlayerWeaponBasic
{
    // Start is called before the first frame update
    void Start()
    {
        weaponName = "기본공격";
        weaponExplain = "공격시 기본 포탄을 1발 발사합니다.";

        for(int i = 1; i <= 4; i++)
        {
            projectileDamage[i] = 25 + i * 10;
            if(i == 2)
            {
                projectileDamage[3] = 25 + i * 5;
            }
            projectileSpeed[i] = 25;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 1;
        }

        GetCameraTransform();
        projectile =  Resources.Load<GameObject>("PlayerTestBullet");
        currentLevel = 3;
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
