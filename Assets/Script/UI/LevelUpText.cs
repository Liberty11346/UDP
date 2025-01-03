using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 작성자: 5702600 이창민
// 레벨 업 텍스트의 스크립트. 플레이어가 무기, 스킬 포인트를 가지고 있다면 나타나서 메세지를 띄운다.
public class LevelUpText : MonoBehaviour
{
    private Color weaponColor = new Color(1, 0.8f, 0, 1), // 무기 포인트 텍스트 색상
                  skillColor = new Color(0, 0.8f, 1, 1); // 스킬 포인트 텍스트 색상
    private string weaponText = "Weapon\nLevel Up\n(Ctrl + 1~4)", // 무기 포인트 텍스트
                   skillText = "Skill\nLevel Up\n(Ctrl + Q or E)", // 스킬 포인트 텍스트
                   blankText = " ";
    private PlayerCtrl player;
    private TextMeshProUGUI text;
    void Start()
    {
        // 플레이어를 찾는다
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        
        // 자신의 컴포넌트에 접근
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        WhenPlayerLevelUp();
    }

    // 플레이어에게 무기 포인트나 스킬 포인트가 남아있으면 출력되어서 포인트 사용을 유도
    // 포인트가 둘 다 있다면 스킬 포인트 소모를 우선 유도
    private void WhenPlayerLevelUp()
    {
        if( player.skillPoint > 0 )
        {
            text.color = skillColor;
            text.text = skillText;
        } 
        else if( player.weaponPoint > 0 )
        {
            text.color = weaponColor;
            text.text = weaponText;
        }
        else
        {
            text.text = blankText;
        }
    }
}