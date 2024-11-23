using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MeleeSkillFirst : PlayerSkillBasic
{
    public GameObject BarrierPrefab; // 방어막 프리팹

    void Start()
    {
        // 플레이어에게 보여질 스킬의 정보
        skillName = "방어막";
        skillExplain = "3초 동안 적의 공격을 막아주는 방어막을 생성합니다.";

        // 스킬의 수치
        maxCoolTime = 12; // 스킬의 재사용 대기시간
    }

    public override void Activate()
    {
        if (BarrierPrefab != null)
        {
            // 배리어를 생성하고 활성화
            GameObject barrier = Instantiate(BarrierPrefab, transform.position, Quaternion.identity);
            barrier.SetActive(true);
        }
    }
}
