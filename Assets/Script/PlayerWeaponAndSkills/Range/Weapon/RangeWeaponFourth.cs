using System.Collections;
using UnityEngine;

// 작성자: 5702600 이창민
public class RangeWeaponFourth : PlayerWeaponBasic
{
    void Start()
    {
        // 플레이어에게 보여질 무기의 정보를 입력
        weaponName = "플라즈마 광선";
        weaponExplain = "적들을 관통하며 큰 피해를 입히는 강력한 광선을 발사합니다.\n발사 명령 후 실제로 광선이 발사되기까지 2초동안의 충전 시간이 있습니다.";
        
        // 무기의 레벨 별 수치를 입력
        for( int i = 0 ; i < 4; i++ )
        {
            projectileDamage[i] = 48 + i*12; // 피해량
            projectileSpeed[i] = 0; // 이동속도
            projectileAmount[i] = 1; // 포탄 수
            maxCoolTime[i] = 12; // 쿨타임
        }

        GetCameraTransform();
        projectile = Resources.Load<GameObject>("Range/RangeBulletFourth");
    }

    // 2초 후 바라보는 방향으로 Ray를 쏴서 충돌한 모든 대상에게 피해를 준다
    public override void Fire()
    {
        StartCoroutine(RayLaunch());
    }

    IEnumerator RayLaunch()
    {
        // 현재 카메라의 각도를 저장
        Vector3 launchAngle = playerCameraTransform.rotation.eulerAngles;

        // 2초간 대기
        yield return new WaitForSeconds(2);

        // 저장해둔 각도로 플라즈마 포 발사
        GameObject ray = Instantiate(projectile, player.transform.position, Quaternion.Euler(launchAngle));
        ray.GetComponent<PlayerBulletBasic>().Clone(this, currentLevel);
    }
}