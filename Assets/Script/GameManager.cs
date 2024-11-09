using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public GameObject Player; // 플레이어 Transform
    public Material[] materials; // 몬스터에 적용될 Material 배열
    public GameObject enemyPrefab; // 몬스터 프리팹
    public float spawnInterval = 2f; // 몬스터 생성 간격 (초 단위)
    public int maxMonsterCount = 10; // 최대 몬스터 수

    private List<GameObject> Enemies = new List<GameObject>();

    private float spawnTimer;
    private int currentMonsterCount = 0; // 현재 씬에 존재하는 몬스터 수

    public float minToDistance = 10f; // 플레이어와의 최소 거리
    public float minToEnemyDistance = 2f; // 다른 몬스터와의 최소 거리S

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        StartCoroutine(Spawn());
    }

    void Update()
    {
        if( Player != null ) transform.position = Player.transform.position;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        if( currentMonsterCount < 4 ) SpawnMonsters();
        StartCoroutine(Spawn());
    }

    void SpawnMonsters()
    {
        // 플레이어 위치 근처에 몬스터를 생성
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float spawnMinDistance = 30;
        float spawnRandomDistance = Random.Range(10, 100);
        float spawnDistance = spawnMinDistance + spawnRandomDistance;

        Vector3 spawnPosition = randomVector * spawnDistance;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentMonsterCount++;
    }
}
