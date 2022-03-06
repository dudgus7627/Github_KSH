using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeTrigger : MonoBehaviour
{
    private static HomeTrigger instance;

    public static HomeTrigger Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType< HomeTrigger > ();

            return instance;
        }
    }

    public Text portalText;
    public bool getWeapon = false;
    public bool tryDrug = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TriggerObj")
        {
            switch (other.gameObject.name)
            {
                // 마약 트리거
                case "Drug":
                    if (!HomeButtonController.Instance.drugUI.activeSelf && !tryDrug)
                    {
                        HomeButtonController.Instance.drugUI.SetActive(true);
                    }
                    break;

                // 포탈 트리거 : 포탈 UI SetActive(true)
                case "Portal":
                    if (GameManager.Instance.gameState == GameState.Reality1)
                    {
                        string portalUIText = "";

                        if (getWeapon)
                        {
                            portalUIText = "침입자를 죽일 무기가 필요해.";
                        }
                        else
                        {
                            portalUIText = "오마에와 모 신데이루!";
                        }
                        portalText.text = portalUIText;
                    }

                    if (!HomeButtonController.Instance.portalUI.activeSelf)
                    {
                        HomeButtonController.Instance.portalUI.SetActive(true);
                    }
                    break;
            }
        }     
    }
}
