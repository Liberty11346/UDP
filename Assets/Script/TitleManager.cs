/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ��ư Ŭ�� �� �� ��ȯ
    public void WhenClick(string name)
    {
        switch( name )
        {
            case "Play": SceneManager.LoadScene("MainGame"); break;
            case "Tutorial": SceneManager.LoadScene("Tutorial"); break;
            case "Exit": Application.Quit(); break;
        }
    }

    void Start()
    {
        Cursor.visible = true;
    }
}
