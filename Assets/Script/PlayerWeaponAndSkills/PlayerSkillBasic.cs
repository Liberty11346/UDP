using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 함선의 스킬을 PlayerCtrl의 PlayerSkillBasic 배열을 이용하여 관리하기 위한 클래스입니다.
// 플레이어 함선들의 스킬을 구현할 땐, 이 클래스를 상속받는 클래스를 새로 만든 후, Activate() 함수를 오버라이딩하여 구현합니다.
public class PlayerSkillBasic : MonoBehaviour
{
    // 이곳의 변수들은 그대로 두고, 이 클래스를 상속 받는 클래스에서 값을 집어 넣어줍니다.
    protected string skillName, // 스킬의 이름
                     skillExplain; // 플레이어에게 보여줄 설명
    protected int currentLevel = -1, // 현재 스킬의 레벨 (-1일 경우 배우지 않은 상태)
                  maxLevel = 1, // 스킬의 최대 레벨
                  maxCoolTime, // 스킬의 재사용 대기시간
                  currentCoolTime; // 현재 남은 재사용 시간

    // PlayerCtrl에서 스킬을 사용할 때, Activate() 함수를 호출합니다.
    // Activate() 함수를 오버라이딩하여 각 플레이어 함선들의 주포를 구현합니다.
    public virtual void Activate() { }

    // 현재 사용 가능한 상태인지 반환하는 함수.
    // PlayerCtrl에서 스킬을 사용하기 전에, 먼저 이 함수를 호출한 후, true라면 스킬을 사용합니다.
    public bool isUseAble()
    {
        // 배우지 않은 상태라면 false를 반환
        if( currentLevel < 0 ) return false;
        
        // 쿨타임 중이라면 false를 반환
        if( currentCoolTime > 0 ) return false;

        // 아무 조건에도 걸리지 않았다면 쿨타임을 채운 후 true를 반환(스킬 사용)
        currentCoolTime = maxCoolTime;
        return true;
    }

    // 1초마다 쿨타임을 줄이는 함수. PlayerCtrl에서 스킬 사용 후 호출
    public IEnumerator CoolDown()
    {
        // 1초간 대기 후 쿨타임을 1 줄인다.
        yield return new WaitForSeconds(1);
        currentCoolTime--;

        // 아직 쿨타임이 0이 아니라면 추가로 호출하여 쿨타임을 계속 줄임 
        if( currentCoolTime > 0 ) StartCoroutine(CoolDown());
    }
}
