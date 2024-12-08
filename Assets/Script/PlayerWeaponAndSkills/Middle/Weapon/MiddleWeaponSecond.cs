using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;


public class MiddleWeaponSecond : PlayerWeaponBasic
{

    public float[] projectileSlow = new float[4];
    // Start is called before the first frame update
    void Start()
    {
         weaponName = "다크매터";
         weaponExplain = "조준한 방향으로 암흑물질을 발사하고 적 적중 시,\n 반지름 20범위 이내의 적들의 암흑물질의 중심으로 끌어당깁니다\n 적을 끌어 당긴 후 암흑물질은 그 자리에 4초동안 남아 적들의 이동속도를 둔화 시킵니다.";
         currentLevel = 0;
         

        for(int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 25 + i * 5;
            projectileSpeed[i] = 60;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 11;

            //레벨당 슬로우
            projectileSlow[i] = 0.15f + (i * 0.5f);
        }

        GetCameraTransform();
        projectile =  Resources.Load<GameObject>("Middle/MiddleBullet2");
        
    }

     public override void Fire()
    {
        BlackMatter();
    }

    void BlackMatter()
    {
        Vector3 firePos = new Vector3(playerCameraTransform.position.x, playerCameraTransform.position.y, playerCameraTransform.position.z);
        GameObject fireProjectile = Instantiate(projectile, firePos, playerCameraTransform.rotation);
        fireProjectile.GetComponent<PlayerBulletBasic>().Clone(this ,currentLevel);
    }
}
