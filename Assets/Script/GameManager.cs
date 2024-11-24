using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public GameObject playerPrefab; // 플레이어 프리팹
    private GameObject player; // 현재 플레이어
    public string playerType; // 현재 선택한 플레이어 함선의 종류
    public Material[] materials; // 몬스터에 적용될 Material 배열
    public GameObject enemyPrefab; // 몬스터 프리팹
    public float spawnInterval = 2f; // 몬스터 생성 간격 (초 단위)
    public int maxMonsterCount = 10; // 최대 몬스터 수
    public int currentMonsterCount = 0; // 현재 씬에 존재하는 몬스터 수
    public float minToDistance = 10f; // 플레이어와의 최소 거리
    public float minToEnemyDistance = 2f; // 다른 몬스터와의 최소 거리

    void Start()
    {
        // 게임 시작 시 플레이어를 생성
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        
        // 플레이어의 타입에 맞춰 플레이어 무기를 설정
        player.GetComponent<PlayerCtrl>().PlayerTypeSetting(playerType);

        // 카메라의 스크립트를 활성화
        GameObject.Find("Main Camera").GetComponent<MainCameraCtrl>().enabled = true;
        GameObject.Find("PlayerCamera").GetComponent<PlayerCameraCtrl>().enabled = true;

        // Dashboard UI의 스크립트를 활성화
        GameObject[] dashboard = GameObject.FindGameObjectsWithTag("Dashboard");
        foreach( GameObject obj in dashboard ) obj.GetComponent<Dashboard>().enabled = true;

        // HotKey UI의 스크립트를 활성화
        GameObject[] hotKey = GameObject.FindGameObjectsWithTag("HotKey");
        foreach( GameObject obj in hotKey ) obj.GetComponent<HotKey>().enabled = true;

        // 적 생성 시작
        StartCoroutine(Spawn());
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

        // 생성한 방향으로, 생성한 거리만큼 떨어진 곳에 적을 생성한다.
        float spawnDistance = spawnMinDistance + spawnRandomDistance;
        Vector3 spawnPosition = randomVector * spawnDistance;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 적의 수 증가
        currentMonsterCount++;
    }
}
