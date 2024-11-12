using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 10f; // 플레이어의 현재 이동 속도
    private float maxSpeed = 20f, // 플레이어의 최대 이동 속도
                  minSpeed = 5f; // 플레이어의 최소 이동 속도
    private float rotationX = 0f, // 플레이어의 X축 회전값
                  rotationY = 0f; // 플레이어의 Y축 회전값
    private float maxZoomFOV = 30f, // 줌인 시 카메라 FOV
                  originalFOV = 60f; // 평상시 카메라 FOV
    private int selecetedWeaponType; // 선택된 주포 타입
    private GameObject mainCamObj, // 평상시 시야를 보여줄 카메라
                       playerCamObj; // 줌인 시 시야를 보여줄 카메라
    private Camera mainCam, playerCam; // 두 카메라 클래스
    [SerializeField] private PlayerWeaponBasic[] playerWeapon = new PlayerWeaponBasic[4]; // 플레이어가 사용할 주포
    [SerializeField] private PlayerSkillBasic[] playerSkill = new PlayerSkillBasic[2]; // 플레이어가 사용할 스킬
   
    void Start()
    {
        // 씬 내에서 카메라를 찾아 접근
        mainCamObj = GameObject.Find("Main Camera");
        playerCamObj = GameObject.Find("PlayerCamera");
        mainCam = mainCamObj.GetComponent<Camera>();
        playerCam = playerCamObj.GetComponent<Camera>();
        
        // 줌인 시 시야를 보여줄 카메라는 비활성화
        ActivateCamera(playerCam, false);

        // 평상시 카메라의 FOV값 초기화
        originalFOV = playerCam.fieldOfView;

        TestInit();
    }

    void TestInit()
    {
        playerWeapon[0] = GameObject.Find("RangeWeaponFirst").GetComponent<RangeWeaponFirst>();
    }

    void Update()
    {
        // 기본적으로 플레이어는 직진만 한다.
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
        verticalMove(); // W, S 입력으로 위 아래 기울기
        horizontalMove(); // A, D 입력으로 좌 우 기울기
        
        acceleration(); // Space 입력으로 가속
        deceleration(); // Shift 입력으로 감속

        fire(); // 왼쪽 마우스 클릭으로 발사
        zoom(); // 오른쪽 마우스 클릭으로 줌

        selectType(); // 1 ~ 4 입력으로 주포 선택
        Skill(); // Q, E 입력으로 스킬 사용
    }

    void ActivateCamera(Camera cam, bool isActivated)
    {
        cam.enabled = isActivated;
        cam.gameObject.GetComponent<AudioListener>().enabled = isActivated;
    }

    // ���� �̵�
    void verticalMove()
    {
        // 상승
        if (Input.GetKey(KeyCode.W))
        {
            if (rotationX >= 90f)
            {
                rotationX = 90f;
            }
            rotationX -= 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }

        // 하강
        if (Input.GetKey(KeyCode.S))
        {
            if (rotationX <= -70f)
            {
                rotationX = -70f;
            }
            rotationX += 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    // ���� �̵�
    void horizontalMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (rotationY <= -360f)
            {
                rotationY = 0f;
            }
            rotationY -= 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (rotationY >= 360f)
            {
                rotationY = 0f;
            }
            rotationY += 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    // ����
    void acceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed, Time.deltaTime);
        }
    }

    //����
    void deceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, minSpeed, Time.deltaTime);
        }
    }

    // ����
    void fire()
    {
        if (Input.GetMouseButton(0))
        {
            PlayerWeaponBasic selectedWeapon = playerWeapon[selecetedWeaponType];
            
            // 현재 선택된 주포를 발사
            if( selectedWeapon.isUseAble() )
            {
                selectedWeapon.Fire(); // 주포 발사
                StartCoroutine(selectedWeapon.CoolDown()); // 발사한 주포의 쿨타임을 돌리기 시작한다
            }
        }
    }

    // ��
    void zoom()
    {
        if (Input.GetMouseButton(1))
        {
            ActivateCamera(playerCam, true);
            ActivateCamera(mainCam, false);
            playerCam.fieldOfView = maxZoomFOV;
        }
        else
        {
            ActivateCamera(playerCam, false);
            ActivateCamera(mainCam, true);
            playerCam.fieldOfView = originalFOV;
        }
    }

    // ���� or ��ų ����
    void selectType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selecetedWeaponType = 0;
            Debug.Log("1�� ����");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selecetedWeaponType = 1;
            Debug.Log("2�� ����");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selecetedWeaponType = 2;
            Debug.Log("3�� ����");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selecetedWeaponType = 3;
            Debug.Log("4�� ����");
        }

    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 0번 스킬을 사용
            if( playerSkill[0].isUseAble() )
            {
                playerSkill[0].Activate();
            }
        }

        if( Input.GetKeyDown(KeyCode.E))
        {
            // 1번 스킬을 사용
            if( playerSkill[1].isUseAble() )
            {
                playerSkill[1].Activate();
            }
        }
    }
}
