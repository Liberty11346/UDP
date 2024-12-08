using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
