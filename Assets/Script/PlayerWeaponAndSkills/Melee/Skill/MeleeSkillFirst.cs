using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MeleeSkillFirst : PlayerSkillBasic
{
    public GameObject BarrierPrefab; // �� ������

    void Start()
    {
        // �÷��̾�� ������ ��ų�� ����
        skillName = "��";
        skillExplain = "3�� ���� ���� ������ �����ִ� ���� �����մϴ�.";

        // ��ų�� ��ġ
        maxCoolTime = 12; // ��ų�� ���� ���ð�
    }

    public override void Activate()
    {
        if (BarrierPrefab != null)
        {
            // �踮� �����ϰ� Ȱ��ȭ
            GameObject barrier = Instantiate(BarrierPrefab, transform.position, Quaternion.identity);
            barrier.SetActive(true);
        }
    }
}
