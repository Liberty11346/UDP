using UnityEngine;

public class RangeWeaponSecond : PlayerWeaponBasic
{
    void Start()
    {
        // 플레이어에게 보여질 주포의 정보를 입력
        weaponName = "근접 유도탄";
        weaponExplain = "이동 중 가까운 적을 탐색하여 추적하는 포탄을 둘 발사합니다.";
        
        // 주포의 레벨 별 수치를 입력
        for( int i = 0 ; i < 4; i++ )
        {
            projectileDamage[i] = 12 + i*4; // 피해량
            projectileSpeed[i] = 90; // 이동속도
            projectileAmount[i] = 2; // 포탄 수
            maxCoolTime[i] = 6; // 쿨타임
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Range/RangeBulletSecond");
    }

    // 2연장 어뢰를 발사한다.
    public override void Fire()
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