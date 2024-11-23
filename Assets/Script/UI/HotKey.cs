using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.MPE;
using UnityEngine;

public class HotKey : MonoBehaviour
{
    private PlayerCtrl player;
    private GameManager gameManager;
    private PlayerWeaponBasic myWeapon; // 자신이 주포 슬롯인 경우, 자신이 담당할 주포
    private PlayerSkillBasic mySkill; // 자신이 스킬 슬롯인 경우, 자신이 담당할 스킬
    private TextMeshProUGUI cooltimeText,
                            levelText;
    private string myType; // 자신의 슬롯 타입
    void Start()
    {
        // 플레이어와 게임매니저를 찾는다.
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 자신의 이름을 통해 자신이 스킬 슬롯인지, 주포 슬롯인지 확인
        myType = gameObject.name.Contains("Weapon") ? "Weapon" : "Skill";

        // 자신의 이름을 통해 자신이 담당할 스킬/주포의 배열 내 인덱스를 확인
        int myIndex = int.Parse(gameObject.name.Substring(0, 1));

        // 자신의 슬롯 타입과 인덱스를 토대로 자신이 담당할 주포/스킬에 접근
        if( myType == "Weapon" ) myWeapon = player.playerWeapon[myIndex];
        else mySkill = player.playerSkill[myIndex];

        // 자신의 컴포넌트에 접근
        cooltimeText = transform.Find("CooltimeText").GetComponent<TextMeshProUGUI>();
        if( myType == "Weapon" ) levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if( myType == "Weapon" ) DisplayWeaponCooltime();
        else DisplaySkillCooltime();
    }

    // 자신이 담당한 주포의 쿨타임을 표시
    void DisplayWeaponCooltime()
    {
        cooltimeText.text = myWeapon.currentCoolTime.ToString();
        if( myWeapon.currentCoolTime < 1 ) cooltimeText.text = " "; 
    }

    // 자신이 담당한 스킬의 쿨타임을 표시
    void DisplaySkillCooltime()
    {
        cooltimeText.text = mySkill.currentCoolTime.ToString();
        if( mySkill.currentCoolTime < 1 ) cooltimeText.text = " ";
    }

    // 현재 주포와 스킬의 레벨에 맞춰 텍스트를 수정
    // 주포와 스킬을 레벨업 할 때, 게임매니저에서 이 함수를 호출
    void UpdateLevelText()
    {
        if( myType == "Weapon" ) levelText.text = myWeapon.currentLevel.ToString();
    }
}