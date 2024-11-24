using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;


public class MiddleWeaponThird : PlayerWeaponBasic
{
    // Start is called before the first frame update

     public bool isActive = false; // 무기가 활성화되어 있는지 여부

    public void ActivateWeapon()
    {
        isActive = true;
        // 무기를 활성화하는 코드 추가
    }

    public void DeactivateWeapon()
    {
        isActive = false;
        // 무기를 비활성화하는 코드 추가
    }
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
}
