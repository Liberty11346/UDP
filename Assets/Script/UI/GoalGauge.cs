using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalGauge : MonoBehaviour
{
    private Image gauge; // 게이지
    private TextMeshProUGUI goalText; // 텍스트
    private Transform playerPos, // 플레이어의 위치
                      goalPos; // 골 오브젝트의 위치
    private RectTransform rectTransform;
    private float maxDistance, // 게임 시작 시 플레이어와 골 오브젝트 사이의 거리
                  currentDistance, // 현재 거리
                  gaugeLength; // 게이지 길이
    private float percentage; // 달성도
    void Start()
    {
        // 자신의 컴포넌트에 접근
        gauge = transform.Find("GoalG").GetComponent<Image>();
        rectTransform = transform.Find("GoalG").GetComponent<RectTransform>();
        goalText = transform.Find("GoalText").GetComponent<TextMeshProUGUI>();

        // 컴포넌트 속성 초기화
        rectTransform.sizeDelta = new Vector2(0, 10);

        // 플레이어와 골 오브젝트의 위치를 찾는다
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        goalPos = GameObject.FindWithTag("GoalObject").GetComponent<Transform>();

        // 플레이어와 골 오브젝트 사이의 거리를 저장
        maxDistance = Vector3.Distance(playerPos.position, goalPos.position);
    }

    void Update()
    {
        CalPercentage();
        DisplayGauge();
    }

    // 현재 플레이어와 골 오브젝트 사이의 거리를 계산하여 퍼센테이지를 산출 및 출력
    void CalPercentage()
    {
        // 현재 플레이어와 골 오브젝트 사이의 거리를 계산
        currentDistance = Vector3.Distance(playerPos.position, goalPos.position);

        // 최대 거리보다 먼 경우(플레이어가 뒤로 간 경우) 0%로 계산
        if( maxDistance < currentDistance ) percentage = 0;
        else percentage = (maxDistance - currentDistance) / maxDistance * 100; // 그렇지 않은 경우 퍼센테이지를 계산하여 반환
    }

    void DisplayGauge()
    {
        // 텍스트에 표시
        goalText.text = ((int)percentage).ToString() + "%";
        
        // 게이지에 반영
        gaugeLength = Mathf.Lerp(rectTransform.sizeDelta.x, percentage * 10, 0.5f);
        rectTransform.sizeDelta = new Vector2(gaugeLength, 10);
    }
}
