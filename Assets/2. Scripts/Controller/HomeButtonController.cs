using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButtonController : MonoBehaviour
{
    private static HomeButtonController instance;
    public static HomeButtonController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HomeButtonController>();

            return instance;
        }
    }

    public GameObject controlInfoUI;
    public GameObject parentUI;
    public GameObject drugUI;
    public GameObject portalUI;
    public GameObject[] scenarioUI;

    public Camera mainCamera;

    public CameraShake mCameraShake;

    public void Awake()
    {
        controlInfoUI.SetActive(true);
        drugUI.SetActive(false);
        portalUI.SetActive(false);

        mainCamera = Camera.main;
        Player player = GameManager.Instance.player.GetComponent<Player>();
        mCameraShake = player.mCameraShake;

        // Portal UI
        HomeTrigger.Instance.portalText = portalUI.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    public void BtnDrug()
    {
        Debug.Log("Try Drug");

        // 카메라 쉐이크
        transform.gameObject.SetActive(false);
        HomeTrigger.Instance.tryDrug = true;

        if (mCameraShake != null)
            mCameraShake.Shake();
    }

    //[ContextMenu("BtnHomePortal")]
    public void BtnHomePortal()
    {
        Debug.Log("BtnHomePortal");
        transform.gameObject.SetActive(false);
        GameManager.Instance.ChangeScene();
    }

    public void BtnFantasyPortal()
    {
        
    }
}
