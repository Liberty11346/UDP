using System.Collections;
using UnityEngine;

public class RangeWeaponFourth : PlayerWeaponBasic
{
    void Start()
    {
        // 플레이어에게 보여질 주포의 정보를 입력
        weaponName = "플라즈마 광선";
        weaponExplain = "적들을 관통하며 큰 피해를 입히는 강력한 광선을 발사합니다.\n발사 명령 후 실제로 광선이 발사되기까지 2초동안의 충전 시간이 있습니다.";
        
        // 주포의 레벨 별 수치를 입력
        for( int i = 0 ; i < 4; i++ )
        {
            projectileDamage[i] = 48 + i*12; // 피해량
            projectileSpeed[i] = 0; // 이동속도
            projectileAmount[i] = 1; // 포탄 수
            maxCoolTime[i] = 12; // 쿨타임
        }

        GetCameraTransform();
        currentLevel = 3; // 테스트용으로 레벨업 해둔 것
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

        // 저장해둔 각도로 Ray 발사
        Ray ray = new Ray(playerCameraTransform.transform.position, launchAngle);
        RaycastHit[] other = Physics.RaycastAll(ray.origin, ray.direction);

        // Ray에 맞은 대상이 있는 경우에만 피해를 입힘 
        if( other != null )
        {
            foreach( RaycastHit target in other )
            {
                // 자기 자신도 반환하기 때문에 태그로 걸러줌
                if( target.collider.tag == "Enemy" )
                {
                    // 맞은 적에게 피해를 입힌다
                    target.collider.GetComponent<Enemy>().GetDamage(projectileDamage[currentLevel]);
                }
            }
        }

        // TODO: Ray의 궤적에 맞게 레이저 파티클을 출력하는 코드 추가 필요
    }
}