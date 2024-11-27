using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public GameObject playerPrefab, // 플레이어 프리팹
                      enemyPrefab, // 몬스터 프리팹
                      goal; // 골 오브젝트 프리팹
     private DifficultyAdjuster difficultyAdjuster;

    private GameObject player; // 현재 플레이어
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

        GameObject tempObject = new GameObject("TempAdjuster"); // 임시 오브젝트 생성

        difficultyAdjuster = tempObject.AddComponent<DifficultyAdjuster>(); //difficultyAdjuster 초기화
        difficultyAdjuster.player = player;
        difficultyAdjuster.goal = goal;

        
        // 플레이어의 타입에 맞춰 플레이어 무기를 설정
        player.GetComponent<PlayerCtrl>().PlayerTypeSetting(playerType);

        // 플레이어가 생성되어야만 정상 작동하는 스크립트들을 활성화
        ActivateScript();

        // 적 생성 시작
        StartCoroutine(Spawn());

        SpawnGoalPoint();
<<<<<<< Updated upstream
=======

        //작업이 끝났을 경우 삭제
        Destroy(tempObject);
>>>>>>> Stashed changes
    }
    void Update()
    {
        // 항상 플레이어를 따라다닌다.
        if( player != null ) transform.position = player.transform.position;
    }

    IEnumerator Spawn()
    {
        // 현재 생성된 적의 수가 최대치에 도달하지 않았다면 1초마다 적을 생성
        yield return new WaitForSeconds(1f);
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

        // 난이도 보정치 가져오기
        float difficultyFactor = difficultyAdjuster.GetDifficultyFactor();

        // 생성한 방향으로, 생성한 거리만큼 떨어진 곳에 적을 생성한다.
        float spawnDistance = spawnMinDistance + spawnRandomDistance;
        Vector3 spawnPosition = randomVector * spawnDistance;
           // 적 생성
    GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    // 생성된 적의 스탯을 난이도 보정치로 조정
    if (spawnedEnemy != null)
    {
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>(); // Enemy 컴포넌트 참조
        if (enemyScript != null)
        {
            enemyScript.AdjustStatsBasedOnDistance(difficultyFactor);
        }
        else
        {
            Debug.LogWarning("생성된 적에 Enemy 컴포넌트가 없습니다.");
        }
    }
        
      

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
    }

    void SpawnGoalPoint()
    {
        // 플레이어 위치를 중심으로 무작위 방향 벡터와 거리를 생성한 후
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float spawnMinDistance = 1000f;
        float spawnRandomDistance = Random.Range(1000, 1100);

        // 생성한 방향으로, 생성한 거리만큼 떨어진 곳에 목적지을 생성한다.
        float spawnDistance = spawnMinDistance + spawnRandomDistance;
        Vector3 spawnPosition = randomVector * spawnDistance;
        Instantiate(goal, spawnPosition, Quaternion.identity);
    }
<<<<<<< Updated upstream
}
=======
    
    }

>>>>>>> Stashed changes
