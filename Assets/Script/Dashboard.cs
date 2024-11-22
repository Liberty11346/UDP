using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    private PlayerCtrl player;
    private FieldInfo value;
    private TextMeshProUGUI text; // 계기판에 값을 표시할 텍스트
    private Transform needleTR; // 계기판에서 회전할 바늘의 트랜스폼
    private string valueName;
    private float currentValue, maxValue, minValue;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>(); // 플레이어 스크립트를 참조
        needleTR = transform.Find("DashboardNeedle").GetComponent<Transform>(); // 계기판 바늘의 트랜스폼에 접근
        text = transform.Find("ValueText").GetComponent<TextMeshProUGUI>(); // 계기판 텍스트의 컴포넌트에 접근

        // PlayerCtrl로 부터 자신의 이름에 맞는 변수의 값을 가져온다.
        // 만약 gameObject.name이 Health라면, PlayerCtrl의 maxHealth, minHealth, currentHealth 값을 가져와 각각 maxValue, minValue, currentValue에 저장
        string valueName = gameObject.name;

        // PlayerCtrl 클래스 중에서 current + valueName와 이름이 같은 변수를 찾아 value에 저장
        value = typeof(PlayerCtrl).GetField("current" + valueName);
        
        var rawMaxValue = typeof(PlayerCtrl).GetField("max" + valueName).GetValue(player); // 변수의 최대값을 가져온다
        var rawMinValue = typeof(PlayerCtrl).GetField("min" + valueName).GetValue(player); // 변수의 최소값을 가져온다
        var rawCurrentValue = value.GetValue(player); // 변수의 현재 값을 가져온다
        
        // 가져온 값의 타입에 맞춰 적절히 형 변환하여 저장
        maxValue = rawMaxValue is float ? (float)rawMaxValue : (int)rawMaxValue;
        minValue = rawMinValue is float ? (float)rawMinValue : (int)rawMinValue;
        currentValue = rawCurrentValue is float ? (float)rawCurrentValue : (int)rawCurrentValue;
    }

    void Update()
    {
        Display();
    }

    // 계기판의 바늘이 이동할 각도를 계산, 변수의 현재 값을 텍스트로 출력
    void Display()
    {
        // 변수의 현재 값을 가져온다
        var rawCurrentValue = value.GetValue(player);
        currentValue = rawCurrentValue is float ? (float)rawCurrentValue : (int)rawCurrentValue;

        // 현재 값으로 텍스트를 업데이트
        text.text = ((int)currentValue).ToString();

        // 현재값, 최소값, 최대값으로 0 ~ 1 사이 비율을 구한 후 180을 곱하여 각도를 계산
        // z값이 음수가 되어야 시계방향으로 회전하기 때문에, 마지막에 -1을 곱한다.
        float rawAngle = (currentValue - minValue) / (maxValue - minValue) * 180 *-1;
        if( gameObject.name == "Speed") Debug.Log(rawAngle);

        // 이전의 z값과 현재의 z값 사이의 보간 값을 계산 후 계기판 바늘에 적용
        float zAngle = Mathf.LerpAngle(needleTR.rotation.eulerAngles.z, rawAngle, 1);
        needleTR.rotation = Quaternion.Euler(0, 0, zAngle);
    }
}