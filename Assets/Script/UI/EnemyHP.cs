using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public GameObject myEnemyObj; // 자신이 체력을 표시할 적 오브젝트
    public Enemy myEnemyScript; // 자신이 체력을 표시할 적의 스크립트
    private PlayerCtrl player;
    private RectTransform canvas, // 캔버스의 RectTransform
                          gauge; // 체력 게이지의 것
    private float percentage,
                  gaugeLength;
    public bool isReady;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        gauge = transform.Find("HPG").GetComponent<RectTransform>();
    }

    void Update()
    {
        if( isReady == true )
        {
            CalPercentage();
            DisplayGauge();
            SetPosition();
        }
    }

    // myEnemy의 위치에 따라 캔버스 내 자신의 위치를 바꾼다
    void SetPosition()
    {
        Vector3 enemyPos = new Vector3(myEnemyObj.transform.position.x, myEnemyObj.transform.position.y + 10, myEnemyObj.transform.position.z);
        
        // 월드 좌표 -> 스크린 좌표
        Vector3 screenPosition = player.currentCam.WorldToScreenPoint(enemyPos);

        // z 값 확인: 카메라 앞쪽일 때만 UI 표시
        if (screenPosition.z > 0)
        {
            transform.position = screenPosition; // UI를 화면 좌표로 이동
            gameObject.SetActive(true);         // UI 활성화
        }
        else
        {
            gameObject.SetActive(false);        // UI 숨김
        }
    }

    // myEnemy의 최대 체력과 현재 체력의 비율을 계산
    void CalPercentage()
    {
        percentage = (myEnemyScript.maxHealth - myEnemyScript.currentHealth) / myEnemyScript.maxHealth * 100;
    }

    void DisplayGauge()
    {
        // 게이지에 반영
        gaugeLength = Mathf.Lerp(gauge.sizeDelta.x, percentage * 3, 0.5f);
        gauge.sizeDelta = new Vector2(gaugeLength, 10);
    }
}