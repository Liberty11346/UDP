using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// 적 오브젝트에 탑재될 AI 스크립트
public class Enemy : MonoBehaviour
{
    private GameObject playerObject; // 플레이어 오브젝트
    private Player playerScript; // 플레이어 스크립트
    private Transform moveTargetPos; // 이동 목표 위치
    private List<Transform> firePos; // 공격 시 투사체가 발사될 위치들의 집합
    public float fireTime; // 공격 속도. 높을수록 느리다.
    public GameObject attackProjectile; // 공격 시 발사할 공격 투사체
    void Start()
    {
        // 플레이어 오브젝트와 플레이어 스크립트 클래스에 접근
        playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();
        
        // 이름에 "FirePos"를 포함하는 자식 오브젝트의 transform을 가져와 firePos 리스트에 넣는다.
        for( int i = 0 ; i < transform.childCount ; i++ ) if( transform.GetChild(i).name.Contains("FirePos") ) firePos.Add(transform.GetChild(i).transform);
    

    }

    void Update()
    {
        
    }

    // moveTargetPos를 바라보도록 함선을 회전
    void Rotate()
    {

    }

    // 공격 목표를 향해 공격 투사체 발사
    // CalAttackTargetPos()에서 계산한 공격 목표 위치를 이용
    void Attack()
    {
        // 플레이어의 예상 위치를 계산하여 공격 위치를 설정 
        Vector3 attackTarget = CalAttackTargetPos();
        
        // 각각의 주포 위치에서 공격 투사체가 발사된다.
        foreach( Transform pos in firePos )
        {
            // 공격 투사체를 생성하고 공격 위치를 바라보게 함.
            GameObject projectile = Instantiate(attackProjectile, pos.position, Quaternion.identity);
            projectile.transform.LookAt(attackTarget);
        }
    }

    // 플레이어의 이동 위치를 예상하여 공격 목표를 설정
    Vector3 CalAttackTargetPos()
    {
        float projectileSpeed = attackProjectile.GetComponent<EnemyAttackProjectile>().moveSpeed; // 공격 투사체의 이동 속도
        float playerMoveSpeed = playerScript.currentMoveSpeed; // 플레이어의 현재 속도
        Vector3 playerMoveDirection = playerScript.currentMoveDirection; // 플레이어의 현재 이동 방향
        Vector3 predictedPlayerPos; // 플레이어의 예상 위치

        // 1. 플레이어와 NPC 사이의 현재 거리를 계산
        float currentDistance = Vector3.Distance(transform.position, playerObject.transform.position);

        // 2. 공격 투사체가 플레이어 위치에 도달하기까지 시간을 계산
        float attackTime = currentDistance/projectileSpeed;

        // 3. 플레이어의 예상 위치를 1차 계산
        predictedPlayerPos = ( playerObject.transform.position + playerMoveDirection ) * playerMoveSpeed * attackTime;

        // 플레이어의 예상 위치를 여러 번 계산하여 정확도를 높이기 위한 for루프
        for( int i = 0 ; i < 3 ; i++ )
        {
            // 4. 1차 계산한 플레이어의 예상 위치와 NPC 사이의 거리를 계산
            currentDistance = Vector3.Distance(transform.position, predictedPlayerPos);

            // 5. 공격 투사체가 플레이어의 예상 위치에 도달하기까지 시간을 계산
            attackTime = currentDistance/projectileSpeed;

            // 6. 플레이어 예상 위치를 2차 계산
            predictedPlayerPos = ( playerObject.transform.position + playerMoveDirection ) * playerMoveSpeed * attackTime;
        }

        // 최종적으로 계산된 플레이어 예상 위치를 반환
        return predictedPlayerPos;
    }
}
