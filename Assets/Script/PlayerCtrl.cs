using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float currentSpeed = 10f, // 플레이어의 현재 이동 속도
                 maxSpeed = 20f, // 플레이어의 최대 이동 속도
                 minSpeed = 5f; // 플레이어의 최소 이동 속도
    public int currentHealth = 100, // 체력
               maxHealth = 100,
               minHealth = 0;
    public int currentFuel = 100, // 연료
               maxFuel = 100,
               minFuel = 0;
    private float rotationX = 0f, // 플레이어의 X축 회전값
                  rotationY = 0f; // 플레이어의 Y축 회전값
    private float maxZoomFOV = 30f, // 줌인 시 카메라 FOV
                  originalFOV = 60f; // 평상시 카메라 FOV
    public int selectedWeaponIndex; // 선택된 주포 타입
    private GameObject mainCamObj, // 평상시 시야를 보여줄 카메라
                       playerCamObj; // 줌인 시 시야를 보여줄 카메라
    private Camera mainCam, playerCam; // 두 카메라 클래스
    public Camera currentCam; // 현재 활성화된 카메라 저장용
    public PlayerWeaponBasic[] playerWeapon = new PlayerWeaponBasic[4]; // 플레이어가 사용할 주포
    public PlayerSkillBasic[] playerSkill = new PlayerSkillBasic[2]; // 플레이어가 사용할 스킬
    public bool isRangeSecondSkilled; // 원거리 함선 스킬인 비상발전을 구현하기 위한 변수. 스킬이 사용되었다면 true
    public string playerType; // 플레이어의 함선 타입
    public Action whenSelectWeapon;
     
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
    }

    // 플레이어의 함선 타입에 맞게 주포와 스킬을 설정하는 함수
    // 플레이어 생성 후 게임매니저에서 적절한 타입을 매개변수로 넣어 이 함수를 호출한다.
    public void PlayerTypeSetting(string type)
    {
        // 타입에 맞는 플레이어 주포를 설정
        for( int i = 0 ; i < playerWeapon.Length ; i++ )
        {
            GameObject weaponObject = Instantiate(Resources.Load<GameObject>(type + "/" + type + "Weapon" + i.ToString()));
            weaponObject.transform.SetParent(gameObject.transform);
            playerWeapon[i] = weaponObject.GetComponent<PlayerWeaponBasic>();
        }

        // 타입에 맞는 플레이어 스킬을 설정
        for( int i = 0 ; i < playerSkill.Length ; i++ )
        {
            GameObject skillObject = Instantiate(Resources.Load<GameObject>(type + "/" + type + "Skill" + i.ToString()));
            skillObject.transform.SetParent(gameObject.transform);
            playerSkill[i] = skillObject.GetComponent<PlayerSkillBasic>();
        }
            
    }

    void Update()
    {
        // 기본적으로 플레이어는 직진만 한다.
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        
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
            rotationX -= 10f * Time.deltaTime * currentSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }

        // 하강
        if (Input.GetKey(KeyCode.S))
        {
            if (rotationX <= -70f)
            {
                rotationX = -70f;
            }
            rotationX += 10f * Time.deltaTime * currentSpeed;
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
            rotationY -= 10f * Time.deltaTime * currentSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (rotationY >= 360f)
            {
                rotationY = 0f;
            }
            rotationY += 10f * Time.deltaTime * currentSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    // ����
    void acceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, Time.deltaTime);
        }
    }

    //����
    void deceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, minSpeed, Time.deltaTime);
        }
    }

    // ����
    void fire()
    {
        if (Input.GetMouseButton(0))
        {
            PlayerWeaponBasic selectedWeapon = playerWeapon[selectedWeaponIndex];
            
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
            currentCam = playerCam;
        }
        else
        {
            ActivateCamera(playerCam, false);
            ActivateCamera(mainCam, true);
            playerCam.fieldOfView = originalFOV;
            currentCam = mainCam;
        }
    }

    // ���� or ��ų ����
    void selectType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeaponIndex = 0;
            whenSelectWeapon?.Invoke(); // 주포 슬롯 UI들에게 선택된 주포를 강조하게 하는 메세지를 보낸다.
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeaponIndex = 1;
            whenSelectWeapon?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeaponIndex = 2;
            whenSelectWeapon?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeaponIndex = 3;
            whenSelectWeapon?.Invoke();
        }
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 0번 스킬을 사용
            if( playerSkill[0].isUseAble() )
            {
                playerSkill[0].Activate(); // 스킬 사용
                StartCoroutine(playerSkill[0].CoolDown()); // 사용한 스킬의 쿨타임을 돌리기 시작한다.
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
