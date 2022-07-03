using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController instance;
    public bool canChangeToBonusStageOffset;

    public CinemachineVirtualCamera camObj;
    [SerializeField] float smoothCamSpeed;
    public CinemachineTransposer camOffset;
    Vector3 firstCamOffset;
    Quaternion firstRotation;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += resetCam;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= resetCam;
    }


    private void Start()
    {
        camOffset = camObj.GetCinemachineComponent<CinemachineTransposer>();
        firstCamOffset = camOffset.m_FollowOffset;
        firstRotation = transform.rotation;

    }


    private void Update()
    {
        if (canChangeToBonusStageOffset)
        {
            float offsetX = Mathf.Lerp(camOffset.m_FollowOffset.x, 0.36f, smoothCamSpeed * Time.deltaTime);
            float offsetY = Mathf.Lerp(camOffset.m_FollowOffset.y, 3f, smoothCamSpeed * Time.deltaTime);
            float offsetZ = Mathf.Lerp(camOffset.m_FollowOffset.z, -2.5f, smoothCamSpeed * Time.deltaTime);
            camOffset.m_FollowOffset = new Vector3(offsetX, offsetY, offsetZ);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(15f, 0, 0)), 3 * Time.deltaTime);
            if(camOffset.m_FollowOffset == new Vector3(0.36f, 3f, -2.5f))
            {
                canChangeToBonusStageOffset = false;
                UIManager.instance.showBonusStageUI();
            }
        }
    }

    void resetCam(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Ready)
        {
            CamController.instance.camObj.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            camOffset.m_FollowOffset = firstCamOffset;
            transform.rotation = firstRotation;
        }
    }

}
