using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    float moveSpeed = 500f;

    void Start()
    {
        Invoke("DestroySelf", 3f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
