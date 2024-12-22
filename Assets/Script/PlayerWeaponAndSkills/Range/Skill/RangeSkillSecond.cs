using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용 시 다음에 발사하는 주포의 쿨타임을 없앤다.
public class RangeSkillSecond : PlayerSkillBasic
{
    PlayerCtrl player;
    void Start()
    {
        // 플레이어에게 보여질 스킬의 정보를 입력
        skillName = "비상 발전";
        skillExplain = "다음에 사용하는 무장의 대기시간을 1회 제거합니다.";

        // 스킬의 수치를 입력
        maxCoolTime = 12; // 스킬의 재사용 대기시간

        // 플레이어의 스크립트를 참조
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
    }

    void Update()
    {
    }

    public override void Activate()
    {
        player.isRangeSecondSkilled = true;   
    }
}