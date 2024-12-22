using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;



public class MiddleWeaponThird : PlayerWeaponBasic
{
    private MiddleBullet2 trackedBullet;  // 추적할 MiddleBullet2

    void Start()
    {
        weaponName = "빨간 버튼";
        weaponExplain = "다크 매터와의 연계스킬\n 다크 매터를 폭파하여 적에게 피해를 입힙니다.";

        for (int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 50 + i * 15;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 13;
        }

        GetCameraTransform();

        projectile =  Resources.Load<GameObject>("Middle/Explode");
      
        if(projectile == null)
        {
            //Debug.Log("null뜬다 고쳐라");
        }    

    }
    public override void Fire()
    {
        // 이미 생성된 MiddleBullet2를 추적하여 폭발 시킴
        if (trackedBullet != null)
        {
            StartCoroutine(TriggerExplosion());
        }
        else
        {
            //Debug.LogWarning("No MiddleBullet2 object to track.");
        }
    }

    private IEnumerator TriggerExplosion()
    {
        // 일정 시간 대기 후 폭발 트리거 (0.5초 대기)
        yield return new WaitForSeconds(0.5f);

        // trackedBullet이 존재하면 폭발 발생
        if (trackedBullet != null)
        {
            trackedBullet.MadeExplode(trackedBullet.transform.position);  // 해당 위치에서 폭발 발생
            //Debug.Log("폭발 발생");
        }
        else
        {
            //Debug.LogWarning("MiddleBullet2 has been destroyed before explosion.");
        }
    }

    // 외부에서 MiddleBullet2 객체를 설정할 메서드
    public void SetTrackedBullet(MiddleBullet2 bullet)
    {
        trackedBullet = bullet;
    }
}
