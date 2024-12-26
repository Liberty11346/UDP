using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자: 5702600 이창민
// 폭발 연출용 오브젝트에 들어갈 스크립트: n초후 자신을 삭제하는 간단한 코드
public class Explosion : MonoBehaviour
{
    public float duration;
    void Start()
    {
        Invoke("DestroySelf", duration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}