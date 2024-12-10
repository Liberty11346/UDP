using System.Collections;
using UnityEngine;

public class MiddleWeaponFourth : PlayerWeaponBasic
{

    int burstCount = 4;
    float fireRate = 0.2f;

    void Start()
    {
        weaponName = "연속 공격";
        weaponExplain = "기본 포탄을 4발 발사합니다";

        for(int i = 0; i < 4; i++)
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
    }


    public override void Fire()
    {
        StartCoroutine(FireBurst()); // 연속 발사 코루틴 실행
    }

    private void BasicWeapon()
    {
        Vector3 firePos = new Vector3(playerCameraTransform.position.x, playerCameraTransform.position.y, playerCameraTransform.position.z);
        GameObject fireProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation);
        fireProjectile.GetComponent<PlayerBulletBasic>().Clone(this ,currentLevel);
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < burstCount; i++) // 설정한 개수만큼 총을 발사
        {
            BasicWeapon(); // 총알 발사
            yield return new WaitForSeconds(fireRate); // 각 발사 간의 간격을 설정
        }

        // 발사 후 쿨타임 적용
        currentCoolTime = maxCoolTime[currentLevel];
    }
}
