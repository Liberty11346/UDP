/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBulletFourth : PlayerBulletBasic
{
    // 적중한 탄환이 2초 후 한 번 더 폭발하여, 탄환 피해량의 20%/30%/40%/50%만큼 추가 피해
    public override void ActivateWhenHit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        StartCoroutine(Explosion(enemy));
    }

    // 2초 후 추가 폭발 피해를 적용하는 코루틴
    private IEnumerator Explosion(Enemy enemy)
    {
        // 2초 대기
        yield return new WaitForSeconds(2f);

        // 적이 살아있으면 추가 피해 적용
        if (enemy != null)
        {
            enemy.currentHealth -= attackDamage * explosionDamage();
        }
    }

    // 레벨 별 피해량을 반환하는 함수
    private float explosionDamage()
    {
        switch (currentLevel)
        {
            case 1: return 0.2f; // 레벨 1 피해량: 20%
            case 2: return 0.3f; // 레벨 2 피해량: 30%
            case 3: return 0.4f; // 레벨 3 피해량: 40%
            case 4: return 0.5f; // 레벨 4 피해량: 50%
            default: return 0.2f; //  기본 피해량: 20%
        }
    }

}
