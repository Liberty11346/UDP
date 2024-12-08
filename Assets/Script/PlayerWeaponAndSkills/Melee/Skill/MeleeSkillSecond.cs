using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillSecond : PlayerSkillBasic
{
    Renderer playerRenderer;
    Color originalColor; // �÷��̾��� ���� ����
    PlayerCtrl playerSkill;
    public float totalDamage; // ������ ���� ���ط�
    float duration = 5f; // ��ų ���ӽð�
    float timer = 0f; 

    void Start()
    {
        // �÷��̾�� ������ ��ų�� ����
        skillName = "Ÿ������ ��";
        skillExplain = "5�� ���� ������ ������ ���� ���ظ�ŭ �������� ȸ���մϴ�.";

        // ��ų�� ��ġ
        maxCoolTime = 15; // ��ų�� ���� ���ð�

        GameObject player = GameObject.FindWithTag("Player");
        playerRenderer = player.GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;  // ���� ���� ����
        playerSkill = player.GetComponent<PlayerCtrl>();
    }

    public override void Activate()
    {
        totalDamage = 0f; // ������ ���� ���ط� �ʱ�ȭ
        playerSkill.isMeleeSecondSkilled = true;
        playerRenderer.material.color = Color.green; // �÷��̾� ������ �ʷϻ����� ����
        timer = duration;
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        // 5�� ���� ������ ���� ���ط� ����
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        playerRenderer.material.color = originalColor; // ���� �������� ����
        playerSkill.isMeleeSecondSkilled = false;

        // ������ ���� ���ط���ŭ �÷��̾� ������ ȸ��
        if (totalDamage > 0)
        {
            // �÷��̾� ü��ȸ�� ��ũ��Ʈ
        }
    }
}
