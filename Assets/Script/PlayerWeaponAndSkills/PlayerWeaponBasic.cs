using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 함선의 주포를 PlayerCtrl의 PlayerWeaponBasic 배열을 이용하여 관리하기 위한 클래스입니다.
// 플레이어 함선들의 주포를 구현할 땐, 이 클래스를 상속받는 클래스를 새로 만든 후, Fire() 함수를 오버라이딩하여 구현합니다.
public class PlayerWeaponBasic : MonoBehaviour
{
    // 이곳의 변수들은 그대로 두고, 이 클래스를 상속 받는 클래스에서 값을 집어 넣어줍니다.
    protected string weaponName, // 주포의 이름
                     weaponExplain; // 플레이어에게 보여줄 설명
    protected GameObject projectile; // 주포 발사 시 날아갈 포탄
    protected int[] projectileDamage = new int[4], // 레벨 별 투사체의 피해량
                    projectileSpeed = new int[4], // 레벨 별 투사체의 이동속도
                    projectileAmount = new int[4], // 레벨 별 포탄 수
                    maxCoolTime = new int[4]; // 레벨 별 쿨타임
    public int currentCoolTime; // 현재 쿨타임 (남은 쿨타임 시간)
    protected int currentLevel = 0, // 현재 주포의 레벨 (0일 경우 배우지 않은 상태)
                  maxLevel = 4; // 주포의 최대 레벨

    // PlayerCtrl에서 주포를 발사할 때, Fire() 함수를 호출합니다.
    // Fire() 함수를 오버라이딩하여 각 플레이어 함선들의 주포를 구현합니다.
    public virtual void Fire() { }

    // 현재 사용 가능한 상태인지 반환하는 함수.
    // PlayerCtrl에서 주포를 발사하기 전에, 먼저 이 함수를 호출한 후, true라면 주포를 발사합니다.
    public bool isUseAble()
    {
        // 배우지 않은 상태라면 false를 반환
        if( currentLevel <= 0 ) return false;
        
        // 쿨타임 중이라면 false를 반환
        if( currentCoolTime > 0 ) return false;

        // 아무 조건에도 걸리지 않았다면 true를 반환
        return true;
    }
}
