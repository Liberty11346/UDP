using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class GoalObject : MonoBehaviour
{
    GameObject Player,
                goal;

    float toDistance;

    // Start is called before the first frame update
    void Start()
    {
         Player = GameObject.FindWithTag("Player");  // "Player" 태그를 가진 오브젝트를 찾음
         goal = GameObject.FindWithTag("Goal");      // "Goal" 태그를 가진 오브젝트를 찾음

          if (Player != null && goal != null)
        {
            toDistance = Vector3.Distance(Player.transform.position, goal.transform.position);
            Debug.Log("Player와 Goal 간의 초기 거리: " + toDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 Goal에 닿았을 때
        {
            Debug.Log("목표 도달! 게임 클리어!");
            GameClear();
        }
    }

    void GameClear()
    {
        // 게임 클리어 처리 로직
        Debug.Log("축하합니다! 게임을 클리어했습니다.");
        // 게임 종료 UI 출력 및 동작
    }
}

