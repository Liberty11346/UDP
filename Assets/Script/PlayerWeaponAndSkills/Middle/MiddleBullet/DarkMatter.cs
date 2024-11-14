using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class DarkMatter : PlayerBulletBasic
{
    // Start is called before the first frame update
    Transform bulletPosition; 

    Rigidbody _rb;

    Collider coll;

    FirstWeapon_2 firstWeapon;

    RedButton redButton;

    private float maxRange = 30f;
    private float distance; 
    private float gravityRadian;

    private float explodeRadius = 20f;
    private float explosionForce = 20f;

    void Start()
    {
        bulletPosition = this.transform;
    }

    public override void Update()
    {
        distance = Vector3.Distance(Vector3.zero, bulletPosition.position);
    }

    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            StartCoroutine(PullEnemy(enemy));
            CalculateRadian(enemy);
            ExplosionDarkMatter();
            Destroy(gameObject, 4f);
        }

        if(enemy == null && distance == maxRange)
        {
            StartCoroutine(PullEnemy(enemy));
            CalculateRadian(enemy);
            ExplosionDarkMatter();
            Destroy(gameObject, 4f);
        }

    }

    private void CalculateRadian(Enemy enemy)
    {

         if(coll != null && firstWeapon != null)
        {
            float originSpeed = enemy.moveSpeed;

            gravityRadian = coll.bounds.extents.magnitude;

            if(Vector3.Distance(enemy.transform.position, transform.position) <= gravityRadian)
            {   
                for(int i = 0; i < 4; i++)
                {
                float slowSpeed = Mathf.RoundToInt(enemy.moveSpeed * (1-firstWeapon.projectileSlow[i]));
                enemy.moveSpeed = slowSpeed;
                }
            }
        }
    }

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    private IEnumerable PullEnemy(Enemy enemy)
    {
        _rb = enemy.GetComponent<Rigidbody>();

        if(_rb != null)
        {
            float pullDuration = 0.5f;
            float elapsedTime = 0f;

            while(elapsedTime < pullDuration)
            {
                elapsedTime += Time.deltaTime;
                Vector3 pullDirection = (transform.position - enemy.transform.position).normalized;
                _rb.AddForce(pullDirection * 10f, ForceMode.Acceleration);

                 yield return null;
            } 

           
        }
    }
    private void ExplosionDarkMatter()
    {
        float doubleClickTime = 4f;
        float lastClickTime = 0f;

        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time - lastClickTime <= doubleClickTime)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
          Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        // 폭발력 적용
                        Vector3 direction = collider.transform.position - transform.position;
                        rb.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
                    }
                }
            }
        }

        // 다크매터 객체를 폭발 후 삭제 (필요시)
        Destroy(gameObject); // 또는 다른 처리
    }
}
