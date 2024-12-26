/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillSecond : PlayerSkillBasic
{
    PlayerCtrl playerCtrl;
    Renderer playerRenderer;

    Color originalColor; // �÷��̾��� ���� ����
    float playerDamage;
    public float totalDamage; // ������ ���� ���ط�
    float duration = 5f; // ��ų ���ӽð�
    float timer = 0f; 

    void Start()
    {
        // 플레이어에게 보여질 스킬의 정보
        skillName = "타오르는 힘";
        skillExplain = "5초 동안 주포로 적에게 입힌 피해만큼 내구도를 회복합니다.";

        // 스킬의 수치
        maxCoolTime = 15; // 스킬의 재사용 대기시간

        GameObject player = GameObject.FindWithTag("Player");
        playerRenderer = player.GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;  // 원래 색상 저장
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    public override void Activate()
    {
        totalDamage = 0f; // 적에게 입힌 피해량 초기화
        playerCtrl.isMeleeSecondSkilled = true;
        playerRenderer.material.color = Color.green; // 플레이어 색상을 초록색으로 변경
        timer = duration;
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        // 5초 동안 적에게 입힌 피해량 누적
        while (timer > 0f)
        {
            totalDamage += PlayerBulletBasic.totalDamageDealt;
            timer -= Time.deltaTime;
            yield return null;
        }
        
        playerRenderer.material.color = originalColor; // ���� �������� ����
        playerCtrl.isMeleeSecondSkilled = false;

        // totalDamage�� 100 �̻��̶�� 100���� �����ϰ� ������ ���� ���ط���ŭ �÷��̾��� ������ ȸ��
        if (totalDamage > 0)
        {
            playerCtrl.GainHealth(totalDamage);
        }
    }
}
