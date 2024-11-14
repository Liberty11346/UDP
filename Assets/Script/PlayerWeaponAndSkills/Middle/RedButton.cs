using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : PlayerWeaponBasic
{

    private float doubleClickTime = 4f;
    private float lastClickTime = 0f;
    private bool ActiveFirstWeapon  = false;
    // Start is called before the first frame update
    void Start()
    {
        weaponName = "빨간 버튼";
        weaponExplain = "다크 매터와의 연계스킬\n 다크 매터를 폭파하여 적에게 피해를 입힙니다.";

         for(int i = 1; i <= 4; i++)
        {
            projectileDamage[i] = 50 + i * 15;
            if(i == 3)
            {
                projectileDamage[4] = projectileDamage[3] + i * 15;
            }
            projectileAmount[i] = 1;
            maxCoolTime[i] = 13;
        }
        GetCameraTransform();
        currentLevel = 3;
    }
}
