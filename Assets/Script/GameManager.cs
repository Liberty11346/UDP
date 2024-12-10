using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  
    public GameObject playerPrefab, // 플레이어 프리팹
                      enemyPrefab, // 몬스터 프리팹
                      goalPrefab; // 골 오브젝트 프리팹
    private GameObject player, // 현재 생성된 플레이어
                       goal; // 현재 생성된 골 오브젝트
    public string playerType; // 현재 선택한 플레이어 함선의 종류
    public Material[] materials; // 몬스터에 적용될 Material 배열
    public float spawnInterval = 2f; // 몬스터 생성 간격 (초 단위)
    public int maxMonsterCount = 10; // 최대 몬스터 수
    public int currentMonsterCount = 0; // 현재 씬에 존재하는 몬스터 수
    public float minToDistance = 10f; // 플레이어와의 최소 거리
    public float minToEnemyDistance = 2f; // 다른 몬스터와의 최소 거리
    

    public void StartGame()
    {
        // 게임 시작 시 플레이어를 생성
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // 플레이어의 타입에 맞춰 플레이어 무기를 설정
        player.GetComponent<PlayerCtrl>().PlayerTypeSetting(playerType);

        // 목표 지점 생성
        SpawnGoalPoint();

        // 플레이어가 생성되어야만 정상 작동하는 스크립트들을 활성화
        ActivateScript();

        // 적 생성 시작
        StartCoroutine(Spawn());
    }
    void Update()
    {
        // 항상 플레이어를 따라다닌다.
        if( player != null ) transform.position = player.transform.position;

        // 게임 중 ESC 누르면 타이틀로 이동 (엑스포 전시용)
        if( Input.GetKeyDown(KeyCode.Escape) ) SceneManager.LoadScene("Title");
    }

    IEnumerator Spawn()
    {
        // 현재 생성된 적의 수가 최대치에 도달하지 않았다면 7~12초마다 적을 생성
        float randomTime = Random.Range(7f, 12f);
        yield return new WaitForSeconds(randomTime);
        if( currentMonsterCount < 4 ) SpawnMonsters();
        StartCoroutine(Spawn());
    }

    // 플레이어 위치 근처에 몬스터를 생성
    void SpawnMonsters()
    {
        // 플레이어 위치를 중심으로 무작위 방향 벡터와 거리를 생성한 후
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float spawnMinDistance = 30;
        float spawnRandomDistance = Random.Range(10, 100);

        // 생성한 방향으로, 생성한 거리만큼 떨어진 곳에 적을 생성한다.
        float spawnDistance = spawnMinDistance + spawnRandomDistance;
        Vector3 spawnPosition = randomVector * spawnDistance;

        // 현재 플레이어와 목표 지점 사이의 거리를 구한다.
        float currentDistance = Vector3.Distance(player.transform.position, goal.transform.position);
        
        // 적을 생성하고 플레이어와 묙표 지점 사이의 거리에 따라 스탯을 재설정
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        enemyScript.SetStatus(currentDistance, 2100);

        // 적의 수 증가
        currentMonsterCount++;
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
        GameObject.Find("Goal").GetComponent<GoalGauge>().enabled = true;

        // 좌표계 UI의 스크립트를 활성화
        GameObject.Find("Coordinate").GetComponent<Coordinate>().enabled = true;

        // 레벨업 텍스트의 스크립트를 활성화
        GameObject.Find("LevelUpText").GetComponent<LevelUpText>().enabled = true;

        // 경험치 게이지의 스크립트를 활성화
        GameObject.Find("Exp").GetComponent<ExpGauge>().enabled = true;
    }

    void SpawnGoalPoint()
    {
        // 플레이어 위치를 중심으로 무작위 방향 벡터를 생성
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float spawnMinDistance = 2100f;

        // 생성한 무작위 방향으로, 일정 거리만큼 떨어진 곳에 목적지를 생성
        float spawnDistance = spawnMinDistance;
        Vector3 spawnPosition = randomVector * spawnDistance;
        goal = Instantiate(goalPrefab, spawnPosition, Quaternion.identity);
    }

}
