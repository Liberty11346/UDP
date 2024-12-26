using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자: 5702600 이창민
// 나침반 오브젝트에 들어갈 스크립트: 플레이어를 따라다니면서 골 오브젝트를 바라본다.
public class Compass : MonoBehaviour
{
    private Transform goalPos;
    void Start()
    {
        goalPos = GameObject.FindWithTag("GoalObject").GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(goalPos.position);        
    }
}
