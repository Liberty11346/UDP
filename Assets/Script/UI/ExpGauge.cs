using TMPro;
using UnityEngine;

public class ExpGauge : MonoBehaviour
{
    private PlayerCtrl player;
    private TextMeshProUGUI expAmountText,
                            playerLevelText;
    private RectTransform expGauge;
    private float percentage,
                  gaugeLength;
    void Start()
    {
        // 플레이어를 찾는다
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        // 자신이 가진 컴포넌트에 접근
        playerLevelText = transform.Find("PlayerLevelText").GetComponent<TextMeshProUGUI>();
        expAmountText = transform.Find("ExpAmountText").GetComponent<TextMeshProUGUI>();
        expGauge = transform.Find("ExpG").GetComponent<RectTransform>();
    }

    void Update()
    {
        // 플레이어의 현재 경험치와 필요 경험치의 비율을 계산
        percentage = (float)((float)player.experience / (float)player.requireExp[player.level] * 700);

        // 게이지에 반영
        gaugeLength = Mathf.Lerp(expGauge.sizeDelta.x, percentage, 0.2f);
        expGauge.sizeDelta = new Vector2(gaugeLength, 10);

        // 텍스트에 현재 경험치 양을 표시
        expAmountText.text = player.experience + " / " + player.requireExp[player.level];

        // 텍스트에 현재 함선 레벨을 표시
        playerLevelText.text = player.level.ToString();
    }
}
