using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : PlayerBulletBasic
{
    float destroyDelay = 0.5f,
          explodeRadius = 20,
          explosionForce = 10;
    bool buttonDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

        Explode();
        
    }

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
