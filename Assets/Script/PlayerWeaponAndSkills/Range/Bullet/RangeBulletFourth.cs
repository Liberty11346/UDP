using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBulletFourth : PlayerBulletBasic
{
    void Start()
    {
        // 레이저형 포탄이기 때문에 움직이지 않고, 그 자리에서 4초 후 삭제됨
        Invoke("DestroySelf", 4);
    }

    public override void Update()
    {
           
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Enemy" )
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.GetDamage(attackDamage); // 맞은 적에게 피해를 입힌다
        }
    }
}