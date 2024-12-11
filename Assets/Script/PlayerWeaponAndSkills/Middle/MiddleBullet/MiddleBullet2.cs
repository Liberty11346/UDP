using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class MiddleBullet2 : PlayerBulletBasic
{
    public float ForceSpeed = 0.3f,
                 destroyDelay = 4f,
                 minDistance,
                 explodeRadius = 20,
                 explosionForce = 10;

    public GameObject Explosion;
    public GameObject Enemy;

    private bool hasExploded = false;  // 폭발이 발생했는지 체크

    public void Start()
    {
        if (Enemy != null)
        {
            minDistance = Vector3.Distance(Enemy.transform.position, transform.position);
        }
    }

   public override void Update()
{
    // 기본적으로 직진
    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    // 적을 찾고 업데이트
  
    Enemy = FindTarget();  // Enemy가 없다면 찾기

    // 적이 폭발 범위 내에 있을 때 폭발 대신 이동속도 감소
    if (!hasExploded && Enemy != null && Vector3.Distance(transform.position, Enemy.transform.position) < explodeRadius)
    {
        DecreaseEnemySpeed(Enemy);  // 적의 이동속도 감소
        hasExploded = true;  // 폭발이 발생했음을 표시
    }

    // 왼쪽 마우스 클릭 (0) 감지
    if (Input.GetMouseButtonDown(0) && Enemy != null)  // 왼쪽 마우스 클릭 시
    {
        Debug.Log("Left mouse button clicked!");
        StartCoroutine(PullEnemy(Enemy, destroyDelay));
    }

    // 플레이어와 일정 거리 이상 떨어지면 적을 끌어당기고 4초 뒤 사라짐
    if (Vector3.Distance(transform.position, player.transform.position) > 300)
    {
        Debug.Log("DarkMatter is too far from the player, destroying.");
        Enemy = FindTarget();
        if (Enemy != null)
        {
            StartCoroutine(PullEnemy(Enemy, destroyDelay));
        }
        Destroy(gameObject, destroyDelay);
    }
}

    // 적의 이동속도를 20% 감소시키는 함수
    private void DecreaseEnemySpeed(GameObject enemy)
    {
        if (enemy != null)
        {
            // 적의 이동속도를 20% 감소시킴
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.moveSpeed *= 0.8f;  // 20% 감소
                Debug.Log("Enemy's speed reduced by 20%");
            }
        }
    }

    private GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closeEnemy = null;
        float closeDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closeDistance)
            {
                closeDistance = distance;
                closeEnemy = enemy;
            }
        }

        if (closeEnemy != null)
        {
            Debug.Log("Closest enemy found: " + closeEnemy.name);
        }
        else
        {
            Debug.LogWarning("No enemy found within range.");
        }

        return closeEnemy;
    }

    private IEnumerator PullEnemy(GameObject enemy, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration && enemy != null)
        {
            elapsedTime += Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, transform.position, ForceSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Enemy pulled successfully.");
    }

    // 폭발 함수
    public void MadeExplode(Vector3 position)
    {
        // 폭발 효과 생성
        Instantiate(Explosion, position, Quaternion.identity);

        // "폭발" 로그 출력
        Debug.Log("Explosion triggered!");

        // 폭발 후 0.4초 뒤에 삭제
        Destroy(gameObject, 0.4f);  // 폭발 후 오브젝트 삭제

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 폭발 범위 내에 있는 적들에게 피해를 주고 밀쳐냄
            if (distance < explodeRadius + currentLevel)
            {
                Debug.Log("Enemy within explosion range. Applying damage and force.");

                attackDamage = 100;

                enemy.GetComponent<Enemy>().GetDamage(attackDamage);
                Debug.Log("데미지 : " + attackDamage);
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    // 폭발력 적용: 폭발의 방향에 따라 밀쳐냄
                    enemyRb.AddForce((enemy.transform.position - transform.position).normalized * explosionForce, ForceMode.Impulse);
                }
            }
        }
    }
}



