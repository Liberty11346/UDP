using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager; // ���ӸŴ���
    public GameObject menuCanvas; // �޴� UI
    public GameObject playerUICanvas; // �÷��̾� UI
    public GameObject meleeToolTipText; // �ٰŸ� ����
    public GameObject middleToolTipText; // �߰Ÿ� ����
    public GameObject rangeToolTipText; // ���Ÿ� ����

    public void WhenClick(string name)
    {
        // Melee ��ư Ŭ�� �� playerType�� Melee�� �����ϰ� ���� ����
        if (name == "Melee")
        {
            gameManager.playerType = "Melee";
            StartGame();
        }

        // Middle ��ư Ŭ�� �� playerType�� Middle�� �����ϰ� ���� ����
        else if (name == "Middle")
        {
            gameManager.playerType = "Middle";
            StartGame();
        }

        // Range ��ư Ŭ�� �� playerType�� Range�� �����ϰ� ���� ����
        else if (name == "Range")
        {
            gameManager.playerType = "Range";
            StartGame();
        }

        // Back ��ư Ŭ�� �� ���� �޴��� ��ȯ
        else if (name == "Back")
            SceneManager.LoadScene("Retreat Protocol");
    }

    void StartGame()
    {
        // �޴� UI ��Ȱ��ȭ
        menuCanvas.SetActive(false);

        // ���� UI Ȱ��ȭ
        playerUICanvas.SetActive(true);

        // ���ӸŴ��� StartGame�Լ� ����
        gameManager.StartGame();
    }

    // �ٰŸ� ���� Ȱ��ȭ
    public void ShowMeleeTooltip()
    {
        meleeToolTipText.SetActive(true);
    }

    // �ٰŸ� ���� ��Ȱ��ȭ
    public void HideMeleeTooltip()
    {
        meleeToolTipText.SetActive(false);
    }

    // �߰Ÿ� ���� Ȱ��ȭ
    public void ShowMiddleTooltip()
    {
        middleToolTipText.SetActive(true);
    }

    // �߰Ÿ� ���� ��Ȱ��ȭ
    public void HideMiddleTooltip()
    {
        middleToolTipText.SetActive(false);
    }

    // ���Ÿ� ���� Ȱ��ȭ
    public void ShowRangeTooltip()
    {
        rangeToolTipText.SetActive(true);
    }

    // ���Ÿ� ���� ��Ȱ��ȭ
    public void HideRangeTooltip()
    {
        rangeToolTipText.SetActive(false);
    }
}
