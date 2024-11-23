using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillSecond : PlayerSkillBasic
{
    Renderer playerRenderer;
    Color originalColor; // �÷��̾��� ���� ����
    PlayerBulletBasic PlayerDamage;
    float duration = 5f; // ��ų ���ӽð�
    float timer = 0f; 
    float totalDamage; // ������ ���� ���ط�

    void Start()
    {
        // �÷��̾�� ������ ��ų�� ����
        skillName = "Ÿ������ ��";
        skillExplain = "5�� ���� ������ ������ ���� ���ظ�ŭ �������� ȸ���մϴ�.";

        // ��ų�� ��ġ
        maxCoolTime = 15; // ��ų�� ���� ���ð�

        GameObject player = GameObject.FindWithTag("Player");
        playerRenderer = player.GetComponent<Renderer>();
        PlayerDamage = player.GetComponent<PlayerBulletBasic>();
        originalColor = playerRenderer.material.color;  // ���� ���� ����
    }

    public override void Activate()
    {
        totalDamage = 0f; // ������ ���� ���ط� �ʱ�ȭ
        playerRenderer.material.color = Color.green; // �÷��̾� ������ �ʷϻ����� ����
        timer = duration;
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        // 5�� ���� ������ ���� ���ط� ����
        while (timer > 0f)
        {
            totalDamage += PlayerDamage.attackDamage * Time.deltaTime;
        }
        timer -= Time.deltaTime;
        yield return null;

        playerRenderer.material.color = originalColor; // ���� �������� ����

        // ������ ���� ���ط���ŭ �÷��̾� ������ ȸ��
        if (totalDamage > 0)
        {
            // �÷��̾� ü��ȸ�� ��ũ��Ʈ
        }
    }
}
