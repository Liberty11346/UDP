using UnityEngine;

public class RangeWeaponFirst : PlayerWeaponBasic
{
    void Start()
    {
        // 플레이어에게 보여질 주포의 정보를 입력
        weaponName = "철갑소이탄";
        weaponExplain = "0.2초 간격으로 2연장 함포를 두 번 발사합니다.\n 적중한 적은 이후 4초동안 받는 피해량이 20% 증가합니다.";
        
        // 주포의 레벨 별 수치를 입력
        for( int i = 0 ; i < 4; i++ )
        {
            projectileDamage[i] = 6 + i*2; // 피해량
            projectileSpeed[i] = 25; // 이동속도
            projectileAmount[i] = 4; // 포탄 수
            maxCoolTime[i] = 2; // 쿨타임
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Range/RangeBulletFirst");
    }

    // 0.2초 간격으로 2연장 함포를 두 번 발사한다.
    public override void Fire()
    { 
        // 첫번째 함포 발사
        FireProjectile();

        // 0.2초 후 두번째 함포 발사
        Invoke("FireProjectile", 0.2f);   
    }

    // 2연장 함포를 한 번 발사하는 함수
    void FireProjectile()
    {
        for( int i = -1; i < 2 ; i += 2 )
        {
            // 투사체 생성 후, 투사체에게 주포의 정보를 전달하여 피해량과 이동속도 조절
            Vector3 firePos = new Vector3( playerCameraTransform.position.x +i, playerCameraTransform.position.y, playerCameraTransform.position.z);
            GameObject firedProjectile = Instantiate( projectile, firePos, playerCameraTransform.rotation );
            firedProjectile.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);
        }
    }
}