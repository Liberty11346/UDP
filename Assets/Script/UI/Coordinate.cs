using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// 작성자: 5702600 이창민
// 화면 좌측 상단에 표시될 좌표계의 스크립트.
// 목표 위치와 현재 플레이어 위치의 좌표를 표시한다.
public class Coordinate : MonoBehaviour
{
    private Transform goalPos, // 목표 위치
                      playerPos; // 플레이어 위치
    private TextMeshProUGUI text;
    void Start()
    {
        // 목표 위치와 플레이어 위치를 찾는다.
        goalPos = GameObject.FindWithTag("GoalObject").GetComponent<Transform>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    
        // 자신의 컴포넌트에 접근
        text = transform.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = $"목표: {(int)goalPos.position.x}, {(int)goalPos.position.y}, {(int)goalPos.position.z}\n현재: {(int)playerPos.position.x}, {(int)playerPos.position.y}, {(int)playerPos.position.z}";
    }
}
