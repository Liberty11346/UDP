using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBulletThird : PlayerBulletBasic
{
    // 적중한 적에게 플레이어가 60의 속도로 이동
    public override void ActivateWhenHit(Collider other)
    {
        // 플레이어 오브젝트와 스크립트
        GameObject playerObject = GameObject.FindWithTag("Player");
        PlayerCtrl player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        // 적 오브젝트와 스크립트
        GameObject enemyObject = GameObject.FindWithTag("Enemy");
        Enemy enemy = other.GetComponent<Enemy>();

        // 총알에 맞은 적 위치
        Vector3 targetPos = other.transform.position;

        // 원래 플레이어의 이동 속도
        float originalSpeed = player.currentSpeed;

        // 적에게 이동하는 속도
        player.currentSpeed = 60f;

        // 플레이어 위치 이동 코루틴 실행
        player.StartCoroutine(Move(playerObject, targetPos, 3f, originalSpeed));
    }

    
    // 적에게 플레이어가 60의 속도로 이동하는 코루틴
    private IEnumerator Move(GameObject playerObject, Vector3 targetPos, float duration, float originalSpeed)
    {
        float timeElapsed = 0f; // 경과된 시간
        Vector3 startPos = playerObject.transform.position; // 플레이어의 현재 위치

        while (timeElapsed < duration)
        {
            // 플레이어를 총알에 맞은 적 위치로 부드럽게 이동
            playerObject.transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 원래의 플레이어 이동 속도로 복원
        playerObject.GetComponent<PlayerCtrl>().currentSpeed = originalSpeed;
    }
}