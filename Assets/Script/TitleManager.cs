using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // 버튼 클릭 시 씬 전환
    public void WhenClick(string name)
    {
        // Play 버튼 클릭 시 Game 씬으로 전환
        if (name == "Play")
            SceneManager.LoadScene("New Game Scene");
        // Exit 버튼 클릭 시 게임 종료
        else if (name == "Exit")
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // 유니티 플레이 모드 종료
            #else
                Application.Quit(); // 애플리케이션 종료
            #endif
        }
    }
}
