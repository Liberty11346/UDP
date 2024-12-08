using System;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
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
    public void Start()
    {
        minDistance = Vector3.Distance(Enemy.transform.position, transform.position);
    }
  
    public override void Update()
    {
        // 기본적으로 직진
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        Enemy = FindTarget();

        if(minDistance < 10 && Input.GetMouseButtonDown(0))
        {
        StartCoroutine(PullEnemy(Enemy, destroyDelay));
        }

    
        //적을 끌어 당긴 후 4초 뒤 사라짐
        Destroy(gameObject, destroyDelay);

        // 플레이어와 일정 거리 이상 떨어지면 적을 끌어당기고 4초 뒤 사라짐
        if (Vector3.Distance(transform.position, player.transform.position) > 300)
        {
            Debug.Log("DarkMatter is too far from the player, destroying.");
            Enemy = FindTarget();
            StartCoroutine(PullEnemy(Enemy, destroyDelay));
            Destroy(gameObject, destroyDelay);
        }

        
    }

    //적이 다크메터 근처에 있는 지 확인 

    private GameObject FindTarget()
    {
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closeEnemy = null;
        float closeDistance = Mathf.Infinity;
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < closeDistance)
            {
                closeDistance = distance;
                closeEnemy = enemy;

            }
        }
         Debug.Log("적 탐지 재대로 실행됨");
    
        return closeEnemy;
        }

       

    
   private IEnumerator PullEnemy(GameObject Enemy, float duration)
   {
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, transform.position, ForceSpeed * Time.deltaTime);

            yield return null;
        }
        Debug.Log("땡기기 재대로 실행됨");
   }


    public void MadeExplode()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);

        destroyDelay = 0.4f;

        Destroy(this, destroyDelay);

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
    }

}
