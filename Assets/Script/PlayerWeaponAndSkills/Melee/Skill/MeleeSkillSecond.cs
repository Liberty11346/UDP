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
        // �÷��̾�� ������ ��ų�� ����
        skillName = "Ÿ������ ��";
        skillExplain = "5�� ���� ������ ������ ���� ���ظ�ŭ �������� ȸ���մϴ�.";

        // ��ų�� ��ġ
        maxCoolTime = 15; // ��ų�� ���� ���ð�

        GameObject player = GameObject.FindWithTag("Player");
        playerRenderer = player.GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;  // ���� ���� ����
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    public override void Activate()
    {
        totalDamage = 0f; // ������ ���� ���ط� �ʱ�ȭ
        playerCtrl.isMeleeSecondSkilled = true;
        playerRenderer.material.color = Color.green; // �÷��̾� ������ �ʷϻ����� ����
        timer = duration;
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        // 5�� ���� ������ ���� ���ط� ����
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
