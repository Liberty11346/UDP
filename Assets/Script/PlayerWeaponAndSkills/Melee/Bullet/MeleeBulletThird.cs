using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBulletThird : PlayerBulletBasic
{
    // ������ ������ �÷��̾ 60�� �ӵ��� �̵�
    public override void ActivateWhenHit(Collider other)
    {
        // �÷��̾� ������Ʈ�� ��ũ��Ʈ
        GameObject playerObject = GameObject.FindWithTag("Player");
        PlayerCtrl player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        // �� ������Ʈ�� ��ũ��Ʈ
        GameObject enemyObject = GameObject.FindWithTag("Enemy");
        Enemy enemy = other.GetComponent<Enemy>();

        // �Ѿ˿� ���� �� ��ġ
        Vector3 targetPos = other.transform.position;

        // ���� �÷��̾��� �̵� �ӵ�
        float originalSpeed = player.moveSpeed;

        // ������ �̵��ϴ� �ӵ�
        player.moveSpeed = 60f;

        // �÷��̾� ��ġ �̵� �ڷ�ƾ ����
        player.StartCoroutine(Move(playerObject, targetPos, 3f, originalSpeed));
    }

    
    // ������ �÷��̾ 60�� �ӵ��� �̵��ϴ� �ڷ�ƾ
    private IEnumerator Move(GameObject playerObject, Vector3 targetPos, float duration, float originalSpeed)
    {
        float timeElapsed = 0f; // ����� �ð�
        Vector3 startPos = playerObject.transform.position; // �÷��̾��� ���� ��ġ

        while (timeElapsed < duration)
        {
            // �÷��̾ �Ѿ˿� ���� �� ��ġ�� �ε巴�� �̵�
            playerObject.transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ������ �÷��̾� �̵� �ӵ��� ����
        playerObject.GetComponent<PlayerCtrl>().moveSpeed = originalSpeed;
    }
}