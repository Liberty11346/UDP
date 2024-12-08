using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    float duration = 3f; // �� ���ӽð�
    float timer = 0f;

    void Start()
    {
        // �÷��̾� ������Ʈ�� ã�� �� �θ�� ����
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

    // �踮�� ��Ȱ��ȭ
    void Deactive()
    {
        gameObject.SetActive(false);
        timer = 0f;
    }

    // �� �Ѿ� �ı�
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
