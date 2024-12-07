using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ��ư Ŭ�� �� �� ��ȯ
    public void WhenClick(string name)
    {
        // Play ��ư Ŭ�� �� Game ������ ��ȯ
        if (name == "Play")
            SceneManager.LoadScene("MainGame");
        // Exit ��ư Ŭ�� �� ���� ����
        else if (name == "Exit")
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // ����Ƽ �÷��� ��� ����
            #else
                Application.Quit(); // ���ø����̼� ����
            #endif
        }
    }
}
