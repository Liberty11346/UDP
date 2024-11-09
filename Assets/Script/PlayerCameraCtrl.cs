using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCameraCtrl : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Vector3 Offset = new Vector3 (0, 0, 3);

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 PlayerPosition = Player.position + Player.TransformDirection(Offset);
        transform.position = Vector3.Lerp(transform.position, PlayerPosition, Time.deltaTime);
    }
}
