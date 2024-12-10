using UnityEngine;

public class PlayerCameraCtrl : MonoBehaviour
{
    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
