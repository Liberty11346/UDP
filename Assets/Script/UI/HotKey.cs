using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotKey : MonoBehaviour
{
    private PlayerCtrl player;
    private PlayerWeaponBasic myWeapon; // 자신이 주포 슬롯인 경우, 자신이 담당할 주포
    private PlayerSkillBasic mySkill; // 자신이 스킬 슬롯인 경우, 자신이 담당할 스킬
    private Image weaponBorder, // 자신의 주포 슬롯의 테두리
                  icon; // 자신의 주포/스킬의 아이콘
    private TextMeshProUGUI cooltimeText,
                            levelText;
    private Color origin = new Color(1, 1, 1, 1), // 기본 테두리 색상
                  selected = new Color(0, 1, 0, 1), // 주포 선택 시 테두리 색상
                  unOpend = new Color(1, 1, 1, 0.5f); // 배우지 않은 무기/스킬일 경우 아이콘 색깔
    private string myType; // 자신의 슬롯 타입
    private int myIndex; // 자신의 슬롯 번호
    void Start()
    {
        // 플레이어를 찾는다.
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        // 자신의 이름을 통해 자신이 스킬 슬롯인지, 주포 슬롯인지 확인
        myType = gameObject.name.Contains("Weapon") ? "Weapon" : "Skill";

        // 자신의 이름을 통해 자신이 담당할 스킬/주포의 배열 내 인덱스를 확인
        myIndex = int.Parse(gameObject.name.Substring(0, 1));

        // 자신의 슬롯 타입과 인덱스를 토대로 자신이 담당할 주포/스킬에 접근
        if( myType == "Weapon" ) myWeapon = player.playerWeapon[myIndex];
        else mySkill = player.playerSkill[myIndex];

        // 자신의 컴포넌트에 접근
        cooltimeText = transform.Find("CooltimeText").GetComponent<TextMeshProUGUI>();
        if( myType == "Weapon" )
        {
            levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            weaponBorder = transform.Find("Border").GetComponent<Image>();
            player.whenSelectWeapon += HighLightSelectedWeapon; // 플레이어의 무기 선택 감지 시작
        }
        icon = GetComponent<Image>();

        // 슬롯 내 아이콘 설정
        DisplayIcon();
    }

    void Update()
    {
        // 쿨타임 표시
        if( myType == "Weapon" ) DisplayWeaponCooltime();
        else DisplaySkillCooltime();

        // 레벨 표시
        if( myType == "Weapon" ) levelText.text = (myWeapon.currentLevel + 1).ToString();
    }

    // 자신이 담당한 주포/스킬의 아이콘을 표시
    void DisplayIcon()
    {
        // 자신의 타입과 인덱스, 플레이어의 타입을 기반으로 적절한 스킬 아이콘을 로드하여 적용
        string myPath = player.playerType + myType + myIndex.ToString();
        Sprite myIcon = Resources.Load<Sprite>("Icon/" + myPath);
        icon.sprite = myIcon;
    }

    // 자신이 담당한 주포의 쿨타임을 표시
    void DisplayWeaponCooltime()
    {
        cooltimeText.text = myWeapon.currentCoolTime.ToString();
        if( myWeapon.currentCoolTime < 1 ) cooltimeText.text = " ";

        if( myWeapon.currentLevel < 0 ) icon.color = unOpend;
        else icon.color = origin;
    }

    // 자신이 담당한 스킬의 쿨타임을 표시
    void DisplaySkillCooltime()
    {
        cooltimeText.text = mySkill.currentCoolTime.ToString();
        if( mySkill.currentCoolTime < 1 ) cooltimeText.text = " ";

        if( mySkill.currentLevel < 0 ) icon.color = unOpend;
        else icon.color = origin;
    }

    // 현재 선택된 주포를 강조
    void HighLightSelectedWeapon()
    {
        if( player.selectedWeaponIndex == myIndex ) weaponBorder.color = selected;
        else weaponBorder.color = origin;
    }
}