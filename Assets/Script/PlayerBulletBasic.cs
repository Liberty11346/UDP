using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 플레이어가 발사하는 공격 투사체에 들어가는 클래스.
// 투사체마다 다양한 효과가 있는 경우, 이 클래스를 상속 받는 새로운 클래스를 만든 후 ActivateWhenHit()을 오버라이딩하여 구현하세요.
public class PlayerBulletBasic : MonoBehaviour
{
    public float moveSpeed, attackDamage, currentLevel;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public virtual void Update()
    {
        // 플레이어의 공격 투사체는 기본적으로 직진만 한다.
        transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime );

        // 플레이어와 일정 거리 이상 떨어지면 스스로를 삭제
        if( Vector3.Distance(transform.position, player.transform.position) > 300 ) Destroy(gameObject);
    }

    // 이 투사체를 발사한 주포의 정보에 맞추어 피해량과 이동속도 조절
    public void Clone(PlayerWeaponBasic weapon, int level)
    {
        currentLevel = level; // 현재 자신의 레벨을 가져옴
        attackDamage = weapon.projectileDamage[level];
        moveSpeed = weapon.projectileSpeed[level];
    }

    // 적에게 적중 시 발동할 추가 효과가 있다면 이 함수를 오버라이딩하여 구현
    public virtual void ActivateWhenHit(Collider other) {}

    void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Enemy" )
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.GetDamage(attackDamage); // 맞은 적에게 피해를 입힌다
            ActivateWhenHit(other); // 오버라이딩 된 추가 효과가 있다면 발동
            Destroy(gameObject); // 스스로를 삭제
        }
    }
}