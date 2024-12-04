using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerPrefab, // 플레이어 프리팹
                      goalPrefab,
                      enemyPrefab; // 튜토리얼용
    private GameObject playerObj; // 현재 플레이어
    private TextMeshProUGUI tutorText, // 플레이어에게 주로 보여줄 튜토리얼 텍스트
                            guideText; // 화면 하단에 나타나 튜토리얼 진행을 돕는 텍스트
    private List<String> tutorTexts = new List<string>(),
                         guideTexts = new List<string>();
    private GoalGauge goalGauge;
    private PlayerCtrl playerScript;
    private int currentTutorStep; // 현재 진행중인 튜토리얼 단계
    private bool isPassable; // 다음 단계로 넘어갈 수 있는 경우 true
    void Start()
    {
        // 게임 시작 시 플레이어를 생성
        playerObj = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        playerScript = playerObj.GetComponent<PlayerCtrl>();

        // 플레이어의 타입에 맞춰 플레이어 무기를 설정
        playerScript.PlayerTypeSetting("Range");

        // 목표 지점 생성
        SpawnGoalPoint();

        // 플레이어가 생성되어야만 정상 작동하는 스크립트들을 활성화
        ActivateScript();
        
        // 튜토리얼 문구 초기화
        InitExplainText();

        // 튜토리얼 시작
        currentTutorStep = 0;
        StartCoroutine(ShowExplainText());
        StartCoroutine(ShowGuideText());
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Tab) && isPassable )
        {
            // 22단계(튜토리얼 마지막)에서 탭을 누르면 타이틀로 돌아감
            if( currentTutorStep == 22 ) SceneManager.LoadScene("Title");

            // 그게 아닌 경우 다음 단계 진행
            StartCoroutine(ShowExplainText());
            StartCoroutine(ShowGuideText());
            currentTutorStep++;
            isPassable = false;
        }

        // 7단계: 상단 게이지가 30에 도달하면 다음 단계 진행
        if( currentTutorStep == 7 )
        {
            if( goalGauge.percentage > 30 )
            {
                StartCoroutine(ShowExplainText());
                StartCoroutine(ShowGuideText());
                currentTutorStep++;
                isPassable = false;
            }
        }

        // 14단계: 무기, 스킬 포인트를 모두 소모하면 다음 단계 진행
        if( currentTutorStep == 14 )
        {
            if( playerScript.weaponPoint < 1 && playerScript.skillPoint < 1 )
            {
                StartCoroutine(ShowExplainText());
                StartCoroutine(ShowGuideText());
                currentTutorStep++;
                isPassable = false;
            }
        }
    }

    void SpawnGoalPoint()
    {
        Vector3 randomVector = new Vector3(1,0,1);
        float spawnMinDistance = 2100f;

        // 생성한 무작위 방향으로, 일정 거리만큼 떨어진 곳에 목적지를 생성
        float spawnDistance = spawnMinDistance;
        Vector3 spawnPosition = randomVector * spawnDistance;
        Instantiate(goalPrefab, spawnPosition, Quaternion.identity);
    }

    // 플레이어가 생성되어야만 정상 작동하는 스크립트들을 활성화
    void ActivateScript()
    {
        // 카메라의 스크립트를 활성화
        GameObject.Find("Main Camera").GetComponent<MainCameraCtrl>().enabled = true;
        GameObject.Find("PlayerCamera").GetComponent<PlayerCameraCtrl>().enabled = true;

        // Dashboard UI의 스크립트를 활성화
        GameObject[] dashboard = GameObject.FindGameObjectsWithTag("Dashboard");
        foreach( GameObject obj in dashboard ) obj.GetComponent<Dashboard>().enabled = true;

        // HotKey UI의 스크립트를 활성화
        GameObject[] hotKey = GameObject.FindGameObjectsWithTag("HotKey");
        foreach( GameObject obj in hotKey ) obj.GetComponent<HotKey>().enabled = true;

        // GoalGauge UI의 스크립트를 활성화
        GameObject goalG = GameObject.Find("Goal");
        goalG.GetComponent<GoalGauge>().enabled = true;
        goalGauge = goalG.GetComponent<GoalGauge>();

        // 좌표계 UI의 스크립트를 활성화
        GameObject.Find("Coordinate").GetComponent<Coordinate>().enabled = true;

        // 레벨업 텍스트의 스크립트를 활성화
        GameObject.Find("LevelUpText").GetComponent<LevelUpText>().enabled = true;

        // 경험치 게이지의 스크립트를 활성화
        GameObject.Find("Exp").GetComponent<ExpGauge>().enabled = true;

        // 튜토리얼 텍스트 활성화
        tutorText = GameObject.Find("TutorText").GetComponent<TextMeshProUGUI>();
        guideText = GameObject.Find("GuideText").GetComponent<TextMeshProUGUI>();
    }

    IEnumerator ShowExplainText()
    {
        tutorText.text = "";
        
        yield return new WaitForSeconds(1);
        if( currentTutorStep < 1 ) yield return new WaitForSeconds(2);
        
        tutorText.text = tutorTexts[currentTutorStep];

        // "당신이 순순히 복귀하지 못하게 방해하는 추격자들이 있습니다."
        // 문구가 등장하면서 적 생성 (튜토리얼 전용이라 공격력이 1)
        if( currentTutorStep == 8 )
        {
            Vector3 spawnPos = new Vector3(1, 0, -1) * 150 + playerObj.transform.position;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }

        // "일시적으로 경험치를 지급해 드릴테니 직접 레벨을 올려보세요."
        // 문구가 등장하면서 레벨을 올려 준다.
        if( currentTutorStep == 14 )
        {
            playerScript.experience += 8000;
            for( int i = 0 ; i < 16 ; i++ ) playerScript.CheckForLevelUp();
        }
    }

    IEnumerator ShowGuideText()
    {
        guideText.text = "";
        
        yield return new WaitForSeconds(3);
        if( currentTutorStep < 1 ) yield return new WaitForSeconds(2);

        switch( currentTutorStep )
        {
            case 7: guideText.text = "상단 게이지 30%에 도달하여 다음 단계 진행"; break;
            case 14: guideText.text = "포인트를 모두 소모하여 다음 단계 진행"; break;
            default: guideText.text = "TAB을 눌러 다음 단계 진행"; isPassable = true; break;
        }
    }

    void InitExplainText()
    {
        tutorTexts.Clear();
        // 0 ~ 7
        tutorTexts.Add("당신은 조난되었습니다.");
        tutorTexts.Add("격렬한 전투에서 홀로 살아남은 당신은,\n근처의 아군 정거장으로 복귀해야 합니다.");
        tutorTexts.Add("화면 좌측 상단에서 현재 위치와 정거장의 위치 좌표를 확인할 수 있습니다.");
        tutorTexts.Add("화면 상단의 게이지는 함선이 정거장에 가까워 질수록 증가합니다.");
        tutorTexts.Add("좌표와 게이지를 확인하면서 정거장에 가까워질 수 있도록\n함선을 조종해야 합니다.");
        tutorTexts.Add("함선은 기본적으로 직진만 가능하지만,\nW, A, S, D 버튼으로 함선을 기울여 이동 방향을 변경할 수 있습니다.");
        tutorTexts.Add("또한 SPACE로 가속, SHIFT로 감속할 수 있습니다.");
        tutorTexts.Add("좌표와 게이지를 확인하면서 정거장 방향으로 이동하도록 함선을 조종해보세요.");
        // 목표 게이지 30%에 도달하면 다음 시작
        
        // 8 ~ 14
        tutorTexts.Add("당신이 순순히 복귀하지 못하게 방해하는 추격자들이 있습니다.");
        tutorTexts.Add("추격자들을 공격하고, 격추하여 안전을 확보해야 합니다.");
        tutorTexts.Add("추격자들을 공격하기 위해 무기와 스킬을 사용합니다.\n화면 하단에서 사용 가능한 무기와 스킬을 확인할 수 있습니다.");
        tutorTexts.Add("무기와 스킬을 사용하기 위해서선\n무기와 스킬이 최소 1레벨 이상이어야 합니다.");
        tutorTexts.Add("추격자들을 격추하여 얻을 수 있는 경험치를 모아 함선의 레벨을 올리면\n무기와 스킬의 레벨을 올릴 수 있는 포인트를 얻습니다.");
        tutorTexts.Add("무기는 최대 4레벨까지 올릴 수 있고, 레벨이 오를 수록 강력해집니다.\n반면 스킬은 4레벨과 8레벨에 한 번씩만 레벨을 올릴 수 있습니다.");
        tutorTexts.Add("일시적으로 경험치를 지급해 드릴테니 직접 레벨을 올려보세요.");
        // 8레벨업 시킨 후 포인트를 모두 소진하면 다음 시작
        
        // 15 ~ 16
        tutorTexts.Add("1 ~ 4 버튼으로 무기를 선택한 후, 마우스 우클릭으로 조준하세요.\n조준이 완료되면, 좌클릭하여 선택한 무기를 발사할 수 있습니다.");
        tutorTexts.Add("스킬은 Q, E 버튼을 누르는 즉시 사용됩나다.");

        // 17 ~ 22
        tutorTexts.Add("화면 하단의 계기판을 통해 현재 함선의 상태를 확인할 수 있습니다.");
        tutorTexts.Add("Speed는 현재 속도를,\nHP는 현재 함선의 내구도를,\nFuel은 남은 연료의 양을 표시합니다.");
        tutorTexts.Add("HP 또는 Fuel이 모두 소진되면 게임에서 패배합니다.");
        tutorTexts.Add("HP는 추격자에게 공격 받으면 줄어들고,\n레벨 업할 때 회복됩니다.");
        tutorTexts.Add("Fuel은 현재 이동 속도에 비례하여 지속적으로 줄어들고,\n적을 격추할 때 회복됩니다.");
        tutorTexts.Add("게임의 전반적인 설명을 모두 마쳤습니다.\nTAB을 누르면 메인 화면으로 돌아갑니다.");
    }
}
