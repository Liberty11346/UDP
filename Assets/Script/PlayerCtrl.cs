using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float rotateSpeed = 15; // 플레이어 회전 속도
    public float currentSpeed = 10, // 플레이어의 현재 이동 속도
                 maxSpeed = 20, // 플레이어의 최대 이동 속도
                 minSpeed = 5; // 플레이어의 최소 이동 속도
    public float currentHealth = 100, // 체력
                 maxHealth = 100,
                 minHealth = 0;
    public float currentFuel = 100, // 연료
                 maxFuel = 100,
                 minFuel = 0;

    public int level,
                experience,
                weaponPoint,
                skillPoint;

    private  ExperienceTable experienceTable;

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
    private int selectedSkillIndex;
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
        Move(); // 키보드로 이동
        Attack(); // 마우스로 공격

        SelectType(); // 1 ~ 4 입력으로 주포 선택
        Skill(); // Q, E 입력으로 스킬 사용
        GainExperience(experience);
    }

    // 플레이어로부터 입력을 받아 함선을 기울임
    void Move()
    {
        // 기본적으로 플레이어는 직진만 한다.
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // 스페이스 바를 누르고 있으면 가속, 왼쪽 쉬프트를 누르고 있으면 감속
        if (Input.GetKey(KeyCode.Space)) currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, Time.deltaTime); // 가속
        if (Input.GetKey(KeyCode.LeftShift)) currentSpeed = Mathf.MoveTowards(currentSpeed, minSpeed, Time.deltaTime); // 감속

        // W A S D 키를 받아서 함선을 기울임
        if (Input.GetKey(KeyCode.W)) rotationX -= 10f * Time.deltaTime * rotateSpeed; // 상승
        if (Input.GetKey(KeyCode.S)) rotationX += 10f * Time.deltaTime * rotateSpeed; // 하강
        if (Input.GetKey(KeyCode.A)) rotationY -= 10f * Time.deltaTime * rotateSpeed; // 좌회전
        if (Input.GetKey(KeyCode.D)) rotationY += 10f * Time.deltaTime * rotateSpeed; // 우회전

        // W A S D 키를 눌러서 변경한 함선의 기울기를 적용
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    // 좌클릭으로 포탄 발사, 우클릭을 누르고 있는 동안 줌
    void Attack()
    {
        // 포탄 발사
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

        // 줌 인
        if (Input.GetMouseButton(1))
        {
            ActivateCamera(playerCam, true);
            ActivateCamera(mainCam, false);
            playerCam.fieldOfView = maxZoomFOV;
            currentCam = playerCam;
        }
        else // 줌 아웃
        {
            ActivateCamera(playerCam, false);
            ActivateCamera(mainCam, true);
            playerCam.fieldOfView = originalFOV;
            currentCam = mainCam;
        }
    }

    // ���� or ��ų ����
    void SelectType()
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
                StartCoroutine(playerSkill[1].CoolDown()); // 사용한 스킬의 쿨타임을 돌리기 시작한다.
            }
        }
    }

    void ActivateCamera(Camera cam, bool isActivated)
    {
        cam.enabled = isActivated;
        cam.gameObject.GetComponent<AudioListener>().enabled = isActivated;
    }

    public void GetDamage(float damage)
    {
        // 입은 피해 만큼 자신의 체력을 깎는다
        currentHealth -= damage;

        // 체력이 1 미만이라면 게임 오버
        if( currentHealth < 1 )
        {
            // 여기에 게임 오버 코드 입력
            Debug.Log("Game over");
        }
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
{
    // 다음 레벨 요구 경험치 계산
    int requiredEXP = experienceTable.GetExpForLevel(level + 1);
    
    // 경험치가 요구 경험치를 초과하거나 같을 경우 레벨 업 처리
    while (experience >= requiredEXP)
    {
        LevelUp();
        experience -= requiredEXP; // 초과된 경험치 차감
        Debug.Log("현재 경험치 : " + experience);

        // 다음 레벨의 요구 경험치를 다시 계산
        requiredEXP = experienceTable.GetExpForLevel(level + 1);
    }
}

    private void LevelUp()
    {
        level++;
        Debug.Log("레벨이 올라갔습니다" + level);
        weaponPoint = 1;
        Debug.Log("주포의 레벨을 올릴 수 있습니다!" + weaponPoint);
        if(level == 4 || level == 8)
        {
            skillPoint++;
            Debug.Log("스킬포인트가 증가하였습니다!" + skillPoint);
        }
        WeaponLevelUp();

        SkillLevelUp();
    }

    private void WeaponLevelUp()
    {
        if(weaponPoint >= 1 && Input.GetKeyDown(KeyCode.LeftControl) && playerWeapon[selectedWeaponIndex].currentLevel < 4)
        {
            PlayerWeaponBasic SelectWeapon = playerWeapon[selectedWeaponIndex];
            
            SelectType();
            Debug.Log("주포" + selectedWeaponIndex + "가 선택되었습니다");

            if(selectedWeaponIndex >= 0 && selectedWeaponIndex < playerWeapon.Length)
            {    
                SelectWeapon.currentLevel++;
                Debug.Log("현재 주포"+ selectedWeaponIndex + "의 레벨 : " + SelectWeapon.currentLevel);
                weaponPoint--;
                Debug.Log("현재 주포 포인트 : " + weaponPoint);
            }
        }
    }
    private void SkillLevelUp()
{
    // 스킬 포인트가 1 이상이고 LeftControl 키가 눌렸을 때 실행
    if (skillPoint >= 1 && Input.GetKeyDown(KeyCode.LeftControl))
    {
        // Q 키로 첫 번째 스킬 레벨 업
        if (Input.GetKey(KeyCode.Q) && playerSkill.Length > 0 && playerSkill[0] != null && playerSkill[0].currentLevel < 2)
        {
            playerSkill[0].currentLevel++;
            skillPoint--;
            Debug.Log("첫 번째 스킬의 레벨이 " + playerSkill[0].currentLevel + "로 증가했습니다.");
        }

        // E 키로 두 번째 스킬 레벨 업
        if (Input.GetKey(KeyCode.E) && playerSkill.Length > 1 && playerSkill[1] != null  && playerSkill[0].currentLevel < 2)
        {
            playerSkill[1].currentLevel++;
            skillPoint--;
            Debug.Log("두 번째 스킬의 레벨이 " + playerSkill[1].currentLevel + "로 증가했습니다.");
        }
    }
}
}

