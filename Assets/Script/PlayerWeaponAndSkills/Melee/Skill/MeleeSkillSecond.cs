using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillSecond : PlayerSkillBasic
{
    Renderer playerRenderer;
    Color originalColor; // 플레이어의 원래 색상
    PlayerBulletBasic PlayerDamage;
    float duration = 5f; // 스킬 지속시간
    float timer = 0f; 
    float totalDamage; // 적에게 입힌 피해량

    void Start()
    {
        // 플레이어에게 보여질 스킬의 정보
        skillName = "타오르는 힘";
        skillExplain = "5초 동안 주포로 적에게 입힌 피해만큼 내구도를 회복합니다.";

        // 스킬의 수치
        maxCoolTime = 15; // 스킬의 재사용 대기시간

        GameObject player = GameObject.FindWithTag("Player");
        playerRenderer = player.GetComponent<Renderer>();
        //playerDamage = ;
        originalColor = playerRenderer.material.color;  // 원래 색상 저장
    }

    public override void Activate()
    {
        totalDamage = 0f; // 적에게 입힌 피해량 초기화
        playerRenderer.material.color = Color.green; // 플레이어 색상을 초록색으로 변경
        timer = duration;
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        // 5초 동안 적에게 입힌 피해량 누적
        while (timer > 0f)
        {
            //totalDamage +=  * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }

        playerRenderer.material.color = originalColor; // 원래 색상으로 변경

        // 적에게 입힌 피해량만큼 플레이어 내구도 회복
        if (totalDamage > 0)
        {
            // 플레이어 체력회복 스크립트
        }
    }
}
