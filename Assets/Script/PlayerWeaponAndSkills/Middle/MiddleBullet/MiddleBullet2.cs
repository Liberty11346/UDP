using System.Collections;
using System.Data;
using UnityEngine;

public class MiddleBullet2 : PlayerBulletBasic
{
    private float power = 15000f;

    private float destroyDelay = 0.5f;

    Transform GravityTarget;
    Rigidbody _rb;

    private float gravity = 0.81f;

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
  
    public override void Update()
    {
        // 기본적으로 직진
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 마우스 왼쪽 버튼 클릭 시 적을 끌어당김
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button clicked. Finding and pulling targets.");
            GravityActive();
        }
        // 플레이어와 일정 거리 이상 떨어지면 스스로를 삭제
        if (Vector3.Distance(transform.position, player.transform.position) > 300)
        {
            Debug.Log("DarkMatter is too far from the player, destroying.");
            Destroy(gameObject, destroyDelay);
        }

        
    }

    // 마우스 왼쪽 클릭 시, 주변 적들을 끌어당기는 함수
    void GravityActive()
    {
       
      Vector3 diff = transform.position - GravityTarget.position;
      _rb.AddForce(diff.normalized * gravity * (_rb.mass));
    }
    

   
}
