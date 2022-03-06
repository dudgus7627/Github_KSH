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
                // ���� Ʈ����
                case "Drug":
                    if (!HomeButtonController.Instance.drugUI.activeSelf && !tryDrug)
                    {
                        HomeButtonController.Instance.drugUI.SetActive(true);
                    }
                    break;

                // ��Ż Ʈ���� : ��Ż UI SetActive(true)
                case "Portal":
                    if (GameManager.Instance.gameState == GameState.Reality1)
                    {
                        string portalUIText = "";

                        if (getWeapon)
                        {
                            portalUIText = "ħ���ڸ� ���� ���Ⱑ �ʿ���.";
                        }
                        else
                        {
                            portalUIText = "�������� �� �ŵ��̷�!";
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
