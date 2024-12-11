using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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