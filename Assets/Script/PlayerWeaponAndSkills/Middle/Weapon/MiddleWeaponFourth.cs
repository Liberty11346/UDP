using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class MiddleWeaponFourth : PlayerWeaponBasic
{

    int burstCount = 4;
    float fireRate = 0.2f;

     
    // Start is called before the first frame update
    void Start()
    {
        weaponName = "연속 공격";
        weaponExplain = "기본 포탄을 4발 발사합니다";

        for(int i= 0; i < 4; i++)
        {
            projectileDamage[i] = 25 + i * 10;
            if(currentLevel == 2)
            {
                projectileDamage[3] = 25 + i * 5;
            }
            projectileSpeed[i] = 60;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 10;
        }

        GetCameraTransform();
        projectile =  Resources.Load<GameObject>("Middle/MiddleBullet1");
       
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

    // Update is called once per frame
    private IEnumerator FireBurst()
    {
        for(int j = 0; j < burstCount; j++){
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < burstCount; i++) // 설정한 개수만큼 총을 발사
            {
                BasicWeapon(); // 총알 발사
                yield return new WaitForSeconds(fireRate); // 각 발사 간의 간격을 설정
            }
        }
    }
   
}
