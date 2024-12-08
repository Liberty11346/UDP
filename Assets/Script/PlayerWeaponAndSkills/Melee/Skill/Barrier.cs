using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    float duration = 3f; // 방어막 지속시간
    float timer = 0f;

    void Start()
    {
        // 플레이어 오브젝트를 찾은 후 부모로 설정
        GameObject player = GameObject.FindWithTag("Player");
        transform.SetParent(player.transform);
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                Deactive();
            }
        }
    }

    // 배리어 비활성화
    void Deactive()
    {
        gameObject.SetActive(false);
        timer = 0f;
    }

    // 적 총알 파괴
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
