/*
┌─                     ─┐
 
 코드 작성: 5645866 구기현

└─                     ─┘
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager; // 게임매니저
    public GameObject menuCanvas; // 메뉴 UI
    public GameObject playerUICanvas; // 플레이어 UI
    public GameObject meleeToolTipText; // 근거리 툴팁
    public GameObject middleToolTipText; // 중거리 툴팁
    public GameObject rangeToolTipText; // 원거리 툴팁
    private GameObject gameOverUI; // 게임 오버 시 보여줄 UI

    public void WhenClick(string name)
    {
        // Back 버튼을 클릭하지 않았다면 해당 버튼에 맞는 플레이어 생성
        if( name != "Back" )
        {
            gameManager.playerType = name;
            StartGame();
        }
        // 누른 버튼이 Back 버튼이라면 메인으로 돌아감
        else SceneManager.LoadScene("Title");
    }

    void Start()
    {
        gameOverUI = GameObject.Find("GameOver");
        gameOverUI.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        gameOverUI.SetActive(true);
    }

    void StartGame()
    {
        // 메뉴 UI 비활성화
        menuCanvas.SetActive(false);

        // 게임 UI 활성화
        playerUICanvas.SetActive(true);

        // 게임매니저 StartGame함수 실행
        gameManager.StartGame();
    }

    // 근거리 툴팁 활성화
    public void ShowMeleeTooltip()
    {
        meleeToolTipText.SetActive(true);
    }

    // 근거리 툴팁 비활성화
    public void HideMeleeTooltip()
    {
        meleeToolTipText.SetActive(false);
    }

    // 중거리 툴팁 활성화
    public void ShowMiddleTooltip()
    {
        middleToolTipText.SetActive(true);
    }

    // 중거리 툴팁 비활성화
    public void HideMiddleTooltip()
    {
        middleToolTipText.SetActive(false);
    }

    // 원거리 툴팁 활성화
    public void ShowRangeTooltip()
    {
        rangeToolTipText.SetActive(true);
    }

    // 원거리 툴팁 비활성화
    public void HideRangeTooltip()
    {
        rangeToolTipText.SetActive(false);
    }
}
