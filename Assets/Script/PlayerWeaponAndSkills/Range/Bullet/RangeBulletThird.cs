using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBulletThird : PlayerBulletBasic
{
    public override void Update()
    {
        // 기본적으로 직진
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 지속적으로 주변 적 탐색
        FindTarget();
    }

    void FindTarget()
    {
        
    }
}
