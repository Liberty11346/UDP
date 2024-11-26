using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleSkill0 : PlayerSkillBasic
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
        player.currentHealth += (int)(player.maxHealth * 0.2f);
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
        }
    }


}
