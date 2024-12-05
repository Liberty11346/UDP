using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;
using System;


public class MiddleWeaponThird : PlayerWeaponBasic
{
    // Start is called before the first frame update

    GameObject DarkMatter;
    Vector3 DarkMatterPos;


    void Start()
    {
         // 씬에서 "MiddleBullet2"라는 이름을 가진 오브젝트를 찾음
        DarkMatter = GameObject.Find("MiddleBullet2");

        DarkMatterPos = DarkMatter.transform.position;
        if(DarkMatterPos == null)
        {
            Debug.Log("null");
        }

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

        projectile =  Resources.Load<GameObject>("Middle/Explode");
      
        if(projectile == null)
        {
            Debug.Log("null뜬다 고쳐라");
        }    

    }
    public override void Fire()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
        CreateExplode();
        }

    }

    void CreateExplode()
    {
         GameObject fireProjectile = Instantiate(projectile, DarkMatterPos, DarkMatter.transform.rotation);
         fireProjectile.GetComponent<PlayerBulletBasic>().Clone(this ,currentLevel);
         Debug.Log("CreateExplode가 실행");

        
        if(DarkMatter != null)
        {
            
            Destroy(DarkMatter.gameObject);
            DarkMatter = null;
            Debug.Log("다크 메터가 제거 됨");

        }
        
         
    }

    
   
}
