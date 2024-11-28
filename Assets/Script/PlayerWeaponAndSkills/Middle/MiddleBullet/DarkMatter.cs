using System.Collections;
using System.Data;
using UnityEngine;

public class DarkMatter : PlayerBulletBasic
{
    private float pullSpeed = 10f; // 적을 끌어당기는 속도
    private float explodeRadius = 12f; // 폭발 반경
    private float explosionForce = 10f; // 폭발력

    private bool isPulled = false; // 적을 끌어당겼는지 여부

    private float destroyDelay = 0.5f;
  
    public override void Update()
    {
        // 기본적으로 직진
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 마우스 왼쪽 버튼 클릭 시 적을 끌어당기고 폭발
        if (Input.GetMouseButtonDown(0) && !isPulled)
        {
            Debug.Log("Mouse button clicked. Finding and pulling targets.");
            FindTargetAndPull();
            Explode();
        }

        // 플레이어와 일정 거리 이상 떨어지면 스스로를 삭제
        if (Vector3.Distance(transform.position, player.transform.position) > 300)
        {
            Debug.Log("DarkMatter is too far from the player, destroying.");
            Destroy(gameObject, destroyDelay);
        }

        
    }

    // 마우스 왼쪽 클릭 시, 주변 적들을 끌어당기고 폭발하는 함수
    void FindTargetAndPull()
    {
        Debug.Log("FindTargetAndPull function is called.");
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 적을 끌어당길 조건 (10 거리 이내)
            if (distance <= 10)
            {
                Debug.Log("Enemy found within range. Pulling enemy.");
                StartCoroutine(PullEnemy(enemy)); // 적을 끌어당기는 코루틴 실행
            }
        }
      
    }
    

    // 적을 끌어당기는 코루틴 함수
    private IEnumerator PullEnemy(GameObject enemy)
    {
        Debug.Log("PullEnemy coroutine started.");
        isPulled = true; // 적을 끌어당겼다고 설정

        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();

        if (enemyRb != null)
        {
            float pullDuration = 1f; // 끌어당기는 시간
            float elapsedTime = 0f;

            // 일정 시간 동안 적을 다크 매터 방향으로 끌어당김
            while (elapsedTime < pullDuration)
            {
                elapsedTime += Time.deltaTime;
                Vector3 pullDirection = (transform.position - enemy.transform.position).normalized; // 끌어당길 방향
                enemyRb.AddForce(pullDirection * pullSpeed, ForceMode.Acceleration); // 힘을 가하여 적을 끌어당김

                yield return null; // 다음 프레임으로 넘어감
            }

        }
        Debug.Log("PullEnemy coroutine completed.");
    }

    // 폭발 처리 함수
    void Explode()
    {
        Debug.Log("Explode function called.");
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyList)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 폭발 범위 내에 있는 적들에게 피해를 주고 밀쳐냄
            if (distance < explodeRadius + currentLevel)
            {
                Debug.Log("Enemy within explosion range. Applying damage and force.");
                enemy.GetComponent<Enemy>().GetDamage(attackDamage);
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    // 폭발력 적용: 폭발의 방향에 따라 밀쳐냄
                    enemyRb.AddForce((enemy.transform.position - transform.position).normalized * explosionForce, ForceMode.Impulse);
                }
            }
        }

        // 폭발 후 스스로를 삭제
        Debug.Log("DarkMatter destroyed after explosion.");
        Destroy(gameObject, destroyDelay);
    }
}
