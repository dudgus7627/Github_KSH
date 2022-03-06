using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    EnermyState enermyState = new EnermyState();

    private void Awake()
    {
        PlayerState.Instance.PlayerTempelate();
    }
    void Start()
    {

    }

    void LateUpdate()
    {

        PlayerState.Instance.LevelCheck();
        if (PlayerState.Instance.currentHp <= 0)
        {
            SceneManager.LoadScene("Die");
            Debug.Log("Player Die");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "attack")
        {
            PlayerState.Instance.currentHp -= enermyState.enermyCurrentdamage;
        }
    }


}
