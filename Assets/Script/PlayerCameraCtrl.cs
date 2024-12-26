/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCameraCtrl : MonoBehaviour
{
    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
