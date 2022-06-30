using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController instance;
    public bool canChangeToBonusStageOffset;

    [SerializeField] CinemachineVirtualCamera camObj;
    [SerializeField] float smoothCamSpeed;
    CinemachineTransposer camOffset;
    Vector3 firstCamOffset;
    float firstXRotation;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        camOffset = camObj.GetCinemachineComponent<CinemachineTransposer>();
        firstCamOffset = camOffset.m_FollowOffset;
        firstXRotation = transform.rotation.x;

    }


    private void Update()
    {

        if (CamController.instance.canChangeToBonusStageOffset)
        {
            float offsetX = Mathf.Lerp(camOffset.m_FollowOffset.x, 0.36f, smoothCamSpeed * Time.deltaTime);
            float offsetY = Mathf.Lerp(camOffset.m_FollowOffset.y, 3f, smoothCamSpeed * Time.deltaTime);
            float offsetZ = Mathf.Lerp(camOffset.m_FollowOffset.z, -2.5f, smoothCamSpeed * Time.deltaTime);
            camOffset.m_FollowOffset = new Vector3(offsetX, offsetY, offsetZ);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(15f, 0, 0)), 3 * Time.deltaTime);
            
        }
    }

}
