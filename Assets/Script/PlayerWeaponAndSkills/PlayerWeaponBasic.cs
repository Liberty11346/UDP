using System.Collections;
using UnityEngine;

// 작성자: 5702600 이창민
// 플레이어 함선의 무기를 PlayerCtrl의 PlayerWeaponBasic 배열을 이용하여 관리하기 위한 클래스입니다.
// 플레이어 함선들의 무기를 구현할 땐, 이 클래스를 상속받는 클래스를 새로 만든 후, Fire() 함수를 오버라이딩하여 구현합니다.
public class PlayerWeaponBasic : MonoBehaviour
{
    // 이곳의 변수들은 그대로 두고, 이 클래스를 상속 받는 클래스에서 값을 집어 넣어줍니다.
    protected string weaponName, // 무기기의 이름
                     weaponExplain; // 플레이어에게 보여줄 설명
    protected GameObject projectile; // 무기기 발사 시 날아갈 포탄
    public int[] projectileDamage = new int[4], // 레벨 별 투사체의 피해량
                 projectileSpeed = new int[4], // 레벨 별 투사체의 이동속도
                 projectileAmount = new int[4], // 레벨 별 포탄 수
                 maxCoolTime = new int[4]; // 레벨 별 쿨타임
    public int currentCoolTime = 0, // 현재 쿨타임 (남은 쿨타임 시간)
               currentLevel = -1; // 현재 무기기의 레벨 (-1일 경우 배우지 않은 상태)
    protected int maxLevel = 4; // 무기기의 최대 레벨
    protected Transform playerCameraTransform;
    protected PlayerCtrl player;

    // 플레이어 카메라가 바라보는 방향으로 포탄을 발사하기 때문에 필요합니다.
    // 이 클래스를 상속 받는 클래스의 Start()에서 꼭 호출하세요.
    protected void GetCameraTransform()
    {
        // 플레이어 카메라의 transform 정보를 가져온다
        playerCameraTransform = GameObject.Find("PlayerCamera").GetComponent<Transform>();
        
        // 플레이어 스크립트를 참조
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
    }

    // PlayerCtrl에서 무기를 발사할 때, Fire() 함수를 호출합니다.
    // Fire() 함수를 오버라이딩하여 각 플레이어 함선들의 무기를 구현합니다.
    public virtual void Fire() { }

    // 현재 사용 가능한 상태인지 반환하는 함수.
    // PlayerCtrl에서 무기를 발사하기 전에, 먼저 이 함수를 호출한 후, true라면 무기를 발사합니다.
    public bool isUseAble()
    {
        // 배우지 않은 상태라면 false를 반환
        if( currentLevel < 0 ) return false;
        
        // 쿨타임 중이라면 false를 반환
        if( currentCoolTime > 0 ) return false;

        // 아무 조건에도 걸리지 않았다면 쿨타임을 채우고 true 반환(무기 발사)
        // 원거리 함선 두 번째 스킬이 사용된 상태라면 쿨타임을 채우지 않는다.
        if( player.isRangeSecondSkilled == false ) currentCoolTime = maxCoolTime[currentLevel];
        else player.isRangeSecondSkilled = false;
        return true;
    }

    // 1초마다 쿨타임을 줄이는 함수. PlayerCtrl에서 무기 발사 후 호출.
    public IEnumerator CoolDown()
    {
        // 1초간 대기 후 쿨타임을 1 줄인다.
        yield return new WaitForSeconds(1);
        currentCoolTime--;
        
        // 쿨타임이 남아있다면 한 번 더 호출하여 쿨타임을 계속 줄임
        if( currentCoolTime > 0 ) StartCoroutine(CoolDown());
    }
}
