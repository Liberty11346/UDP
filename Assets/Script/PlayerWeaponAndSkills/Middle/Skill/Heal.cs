using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PlayerSkillBasic
{
    PlayerCtrl player;

    void Start()
    {
        skillName = "긴급 수리";
        skillExplain = "최대 체력의 20%를 회복합니다.";
    }

    public override void Activate()
    {
        if(player != null){
        player.currentHp += player.maxHp * 0.2f;
        player.currentHp = Mathf.Clamp(player.currentHp, 0, player.maxHp);
        }
    }


}
