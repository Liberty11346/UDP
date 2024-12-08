using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;


public class MiddleWeaponThird : PlayerWeaponBasic
{
    // Start is called before the first frame update
    public MiddleBullet2 middleBullet2;
     
    void Start()
    {
        weaponName = "빨간 버튼";
        weaponExplain = "다크 매터와의 연계스킬\n 다크 매터를 폭파하여 적에게 피해를 입힙니다.";

         for(int i = 0; i < 4; i++)
        {
            projectileDamage[i] = 50 + i * 15;
            projectileAmount[i] = 1;
            maxCoolTime[i] = 13;
        }
       

        GetCameraTransform();
        currentLevel = 3;
    }

    public override void Fire()
    {
       

        
        middleBullet2.MadeExplode();
        
    }

}
