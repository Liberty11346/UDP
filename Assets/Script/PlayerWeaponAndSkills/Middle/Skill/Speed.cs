using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleSkill1 : PlayerSkillBasic
{
    PlayerCtrl player;
    
    // Start is called before the first frame update
    void Start()
    {
        
        skillName = "긴급 탈출";
        skillExplain = "이동 속도가 5초동안 최고 속도가 됩니다.\n 긴급 탈출을 사용하는 동안 연료를 사용하지 않습니다.";
    }

    // Update is called once per frame
    public override void Activate()
    {
        if(player != null)
        {
            StartCoroutine(AccelerationSpeed());
        }
    }

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    public IEnumerable AccelerationSpeed()
    {
        player.currentSpeed = player.maxSpeed;
        yield return new WaitForSeconds(5f);
        player.currentSpeed = 10f;

    }
}
