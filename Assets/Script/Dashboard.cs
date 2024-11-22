using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    private PlayerCtrl player;
    string valueName;
    int currentValue, maxValue, minValue;
    void Start()
    {
        // 플레이어 스크립트를 참조
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        // PlayerCtrl로 부터 자신의 이름에 맞는 변수의 값을 가져온다.
        // 만약 gameObject.name이 Health라면, PlayerCtrl의 maxHealth, minHealth, currentHealth 값을 가져와 각각 maxValue, minValue, currentValue에 저장
        string valueName = gameObject.name;
        maxValue = (int)typeof(PlayerCtrl).GetField("max" + valueName).GetValue(player); // 변수의 최대값을 가져온다
        minValue = (int)typeof(PlayerCtrl).GetField("min" + valueName).GetValue(player); // 변수의 최소값을 가져온다
        currentValue = GetCurrentValue(); // 변수의 현재 값을 가져온다.
    }

    void Update()
    {
        
    }

    // 자신의 이름에 맞춰 PlayerCtrl로 부터 체력, 연료, 속도 중 하나의 값을 가져온다.
    int GetCurrentValue()
    {
        // PlayerCtrl 클래스 중에서 current + valueName와 이름이 같은 변수를 찾아 value에 저장
        FieldInfo value = typeof(PlayerCtrl).GetField("current" + valueName);
        
        // player 오브젝트가 가진 PlayerCtrl 속 value 변수의 값을 가져온다.
        // 가져온 값을 int로 변환하여 반환.
        return (int)value.GetValue(player);
    }

    // 변수의 최대, 최소, 현재 값에 따라 계기판의 바늘을 움직인다
    // + 현재 값을 텍스트로 표시
    void Display()
    {
        
    }
}