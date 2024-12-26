/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    GameObject player;
    public GameObject endingCanvas;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player == null)
        {
            endingCanvas.SetActive(true);
        }
    }
    
    public void WhenClick(string name)
    {
        switch (name)
        {
            case "Menu": SceneManager.LoadScene("Title"); break;
            case "Exit": Application.Quit(); break;
        }
    }
}
