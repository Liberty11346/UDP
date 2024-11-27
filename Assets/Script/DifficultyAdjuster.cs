using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjuster  : MonoBehaviour
{
    public GameObject player;   // 플레이어 오브젝트
    public GameObject goal;     // 목표 지점 오브젝트
    public GoalGauge goalGauge;

    private float toGoalDistance;
    // Start is called before the first frame update
    void Start()
    {
        toGoalDistance = goalGauge.currentDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public float GetDifficultyFactor()
    {
        if (player == null || goal == null)
        {
            Debug.LogError("플레이어나 목표 지점이 설정되지 않았습니다.");
            return 0f;
        }

        // 현재 거리와 최대 거리 기반으로 난이도 보정 계산
        float currentDistance = Vector3.Distance(player.transform.position, goal.transform.position);
        float difficultyFactor = Mathf.Clamp01(1 - (currentDistance / toGoalDistance)); // 0 ~ 1 사이 값
        return difficultyFactor;
    }
}
