using UnityEngine;

public class RangeWeaponThird : PlayerWeaponBasic
{
    void Start()
    {
        // 플레이어에게 보여질 주포의 정보를 입력
        weaponName = "EMP 자폭탄";
        weaponExplain = "적에게 근접하면 자폭하여 피해를 주는 포탄을 발사합니다.\n폭발에 휘말린 적들은 폭발에 의해 밀쳐집니다.";
        
        // 주포의 레벨 별 수치를 입력
        for( int i = 0 ; i < 4; i++ )
        {
            projectileDamage[i] = 10 + i; // 피해량
            projectileSpeed[i] = 20; // 이동속도
            projectileAmount[i] = 1; // 포탄 수
            maxCoolTime[i] = 12 - i; // 쿨타임
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Range/RangeBulletThird");
        currentLevel = 3; // 테스트용으로 레벨업 해둔 것
    }

    // 포탄 발사
    public override void Fire()
    {
        // 투사체 생성 후, 투사체에게 주포의 정보를 전달하여 피해량과 이동속도 조절
        Vector3 firePos = new Vector3( playerCameraTransform.position.x, playerCameraTransform.position.y, playerCameraTransform.position.z);
        GameObject firedProjectile = Instantiate( projectile, firePos, playerCameraTransform.rotation );
        firedProjectile.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);
    }
}