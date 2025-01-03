using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 작성자: 5702600 이창민
// 적 오브젝트에 탑재될 AI 스크립트
public class Enemy : MonoBehaviour
{
    private GameObject playerObject,
                        goalObject;
    private PlayerCtrl playerScript; // 플레이어 스크립트
    private Quaternion moveTargetRotation; // 이동 시 목표 회전값
    private Vector3 moveTargetVector; // 이동 시 목표 벡터
    private List<Transform> firePos = new List<Transform>(); // 공격 시 투사체가 발사될 위치들의 집합
    public float fireDelay, // 공격 쿨타임. 높을수록 공속이 느리다. (초 단위)
                 attackDamage, // 공격력
                 moveSpeed, // 이동 속도. 높을수록 이동이 빠르다.
                 rotateSpeed, // 회전 속도. 높을수록 회전이 빠르다.
                 maxHealth, // 최대 체력
                 currentHealth, // 현재 체력
                 defense; // 방어력(방어력 1당 피해감소 1%)
    private float playerMaxDistance, // 플레이어와 유지할 최대 거리
                  playerMinDistance; // 플레이어와 유지할 최소 거리

    public GameObject attackProjectile, // 공격 시 발사할 공격 투사체
                      enemyHPGauge, // 체력 게이지 오브젝트
                      onDeath; // 사망 시 발생할 폭발 오브젝트
    private GameObject myHPGauge;
    public GameManager gameManager; // 게임매니저.
    
    void Start()
    {
        // 게임매니저 스크립트에 접근 (튜토리얼 중엔 안함)
        if( SceneManager.GetActiveScene().name == "MainGame" ) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 플레이어 오브젝트와 플레이어 스크립트 클래스에 접근
        playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<PlayerCtrl>();

        goalObject = GameObject.FindWithTag("GoalObject");
        
        // "FirePos"를 태그를 가진 자식 오브젝트의 transform을 가져와 firePos 리스트에 넣는다.
        for( int i = 0 ; i < transform.childCount ; i++ ) if( transform.GetChild(i).tag == "FirePos" ) firePos.Add(transform.GetChild(i).transform);
    
        // 플레이어와 유지할 최소 거리를 산출
        playerMinDistance = Random.Range(30, 50);

        // 최소 거리에 30~100 사이 무작위 값을 더하여 최대 거리를 산출
        playerMaxDistance = playerMinDistance + Random.Range(30, 100);

        // 기본적으로 플레이어의 이동 방향과 동일한 방향을 이동 목표 방향으로 잡는다.
        moveTargetRotation = Quaternion.LookRotation(playerObject.transform.forward);

        GenerateHPUI(); // 캔버스에 자신의 체력 게이지 생성

        // 플레이어 공격 시작
        StartCoroutine(Attack());
    }

    void Update()
    {
        // 기본적으로 직진만 한다
        transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime );

        // 이동 목표를 계산
        CalMoveTargetPos();        
        
        // 이동 목표 지점을 바라보도록 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, moveTargetRotation, rotateSpeed * Time.deltaTime);
    }

    // 플레이어의 이동을 기반으로 이동 목표를 설정
    // 이동할 지점을 구하고, 해당 지점을 바라보는 회전 값을 구한다.
    void CalMoveTargetPos()
    {
        // 자기 자신과 플레이어와의 거리를 계산한다.
        float playerCurrentDistance = Vector3.Distance(transform.position, playerObject.transform.position);

        // 플레이어와의 거리가 최소 거리보다 짧다면 플레이어를 등지는 방향을 목표로 잡는다.
        if( playerCurrentDistance < playerMinDistance )
        {
            moveTargetRotation = Quaternion.LookRotation(transform.position - playerObject.transform.position);
        }
        // 플레이어와의 거리가 최대 거리보다 멀다면 플레이어를 바라보는 방향을 목표로 잡는다.
        else if( playerCurrentDistance > playerMaxDistance )
        {
            moveTargetRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
        }
        // 그 외의 경우 현재 이동 방향을 유지한다.
    }

    // 공격 쿨타임마다 플레이어를 공격하는 코루틴.
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(fireDelay);
        Fire();
        StartCoroutine(Attack());
    }

    // 공격 목표를 향해 공격 투사체 발사
    // CalAttackTargetPos()에서 계산한 값을 사용
    void Fire()
    {
        // 플레이어의 예상 위치를 계산하여 공격 위치를 설정
        Vector3 attackTarget = CalAttackTargetPos();
        
        // 각각의 주포 위치에서 공격 투사체가 발사된다.
        foreach( Transform pos in firePos )
        {
            // 공격 투사체를 생성하고 공격 위치를 바라보게 함.
            GameObject projectile = Instantiate(attackProjectile, pos.position, Quaternion.identity);

            // 투사체의 공격력을 자신의 공격력에 맞춰 수정
            projectile.GetComponent<EnemyAttackProjectile>().damage = attackDamage;

            projectile.transform.LookAt(attackTarget);
        }
    }

    // 플레이어의 움직임을 기반으로 예상 위치를 계산
    Vector3 CalAttackTargetPos()
    {
        float attackMoveSpeed = attackProjectile.GetComponent<EnemyAttackProjectile>().moveSpeed;

        Vector3 playerVelocity = playerObject.transform.forward * playerScript.currentSpeed; // 플레이어의 이동 벡터 계산

        // 공격이 도달해야 할 플레이어의 예측 위치를 계산
        Vector3 relativePosition1 = playerObject.transform.position - transform.position; // 현재 위치에서 플레이어의 위치로 향하는 벡터
        float timeToHit1 = relativePosition1.magnitude / attackMoveSpeed; // 공격이 목표에 도달할 시간 계산
        Vector3 predictedPosition1 = playerObject.transform.position + playerVelocity * timeToHit1; // timeToHit 이후 플레이어의 위치를 계산

        // 예상 위치를 한 번 더 계산하여 명중률을 높인다.
        Vector3 relativePosition2 = predictedPosition1 - transform.position;
        float timeToHit2 = relativePosition2.magnitude / attackMoveSpeed;
        Vector3 predictedPosition2 = playerObject.transform.position + playerVelocity * timeToHit2;

        // 예상 위치를 두 번 더 계산하여 명중률을 더욱 높인다.
        Vector3 relativePosition3 = predictedPosition2 - transform.position;
        float timeToHit3 = relativePosition3.magnitude / attackMoveSpeed;
        Vector3 predictedPosition3 = playerObject.transform.position + playerVelocity * timeToHit3;

        // 예상 위치를 세 번 더 계산하여 명중률을 더더욱 높인다.
        Vector3 relativePosition4 = predictedPosition3 - transform.position;
        float timeToHit4 = relativePosition4.magnitude / attackMoveSpeed;
        Vector3 predictedPosition4 = playerObject.transform.position + playerVelocity * timeToHit4;

        return predictedPosition4;
    }

    // 플레이어에게 공격 받을 경우 호출
    public void GetDamage(float damage)
    {
        // 플레이어로부터 입은 피해량만큼 자신의 체력을 깎는다.
        float realDamage = damage * (1 + defense / 100f);
        currentHealth -= realDamage;
        
        // 체력을 깎은 후 남은 체력이 1 미만이라면 사망
        if( currentHealth < 1 ) Die();   
    }

    // 일정 시간 후 방어력을 초기화 하는 함수
    public IEnumerator RecoverDefense(int time)
    {
        yield return new WaitForSeconds(time);
        defense = 0;
    }

    // 사망 시 호출
    private void Die()
    {
        int experienceGained = 100; // 플레이어에게 지급할 경험치
        playerScript.GainExperience(experienceGained); // 플레이어에게 경험치 지급
        Instantiate(onDeath, transform.position, Quaternion.identity); // 사망 시 폭발 이펙트 생성
        if( gameManager != null ) gameManager.currentMonsterCount--; // 현재 적의 수를 1 줄인다. (튜토리얼에선 작동하지 않음)
        Destroy(myHPGauge); // 자신의 체력을 표시하는 UI를 제거
        Destroy(gameObject); // 스스로를 파괴
    }

    // 캔버스에 자신의 체력 게이지 UI 생성
    private void GenerateHPUI()
    {
        // 체력 게이지 UI를 띄울 캔버스를 탐색
        GameObject canvas = GameObject.Find("PlayerUI");

        // 자기 위치에 체력 게이지 UI를 생성 후 코드를 참조
        GameObject UIObj = Instantiate(enemyHPGauge, transform.position, Quaternion.identity);
        EnemyHP UIScript = UIObj.GetComponent<EnemyHP>();

        // 체력 게이지 UI를 캔버스의 자식으로 편입하여 캔버스 위에 그려지게
        UIObj.transform.SetParent(canvas.GetComponent<RectTransform>());

        // 체력 게이지 UI가 자기 자신을 참조하여 현재 체력을 알 수 있도록 함
        UIScript.myEnemyObj = gameObject;
        UIScript.myEnemyScript = gameObject.GetComponent<Enemy>();

        // 작업이 끝나면 체력 게이지 UI를 활성화
        UIScript.isReady = true;

        // 사망 시 함께 제거할 수 있도록 이렇게 만든 체력 게이지 UI를 참조
        myHPGauge = UIObj;
    }

    // 현재 플레이어와 목표 지점 사이의 거리에 따라 능력치를 보정
    public void SetStatus(float currentDistance, float maxDistance)
    {
        // 현재 플레이어어와 목표 지점 사이의 거리에 따라 가중치를 계산 (0 ~ 1)
        float difficultyValue;
        if( currentDistance > maxDistance ) difficultyValue = 0;
        else difficultyValue = currentDistance / maxDistance;

        // 체력과 공격력에 가중치 적용
        // 체력은 시간에 따라 최대 네 배까지 증가
        // 공격력은 난이도에 따라 최대 두 배까지 증가
        float elapsedTime = Mathf.Clamp(Time.time / 600f, 0f, 1f);
        maxHealth = Mathf.Lerp(50f, 200f, elapsedTime);
        attackDamage += attackDamage * difficultyValue;

        // 이동 속도와 공격 속도는 무작위로 결정
        moveSpeed = Random.Range(21,25);
        fireDelay = Random.Range(7,12);

        // 현재 체력 초기화
        currentHealth = maxHealth;
    }
}
