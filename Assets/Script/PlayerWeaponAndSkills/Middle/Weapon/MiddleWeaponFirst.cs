using UnityEngine;

public class MiddleWeaponFirst : PlayerWeaponBasic
{
    void Start()
    {
        weaponName = "기본공격";
        weaponExplain = "공격시 기본 포탄을 1발 발사합니다.";

        for(int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 25 + i * 10;
            projectileSpeed[i] = 75;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 1;
        }

        GetCameraTransform();
        projectile =  Resources.Load<GameObject>("Middle/MiddleBullet1");
    }

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
