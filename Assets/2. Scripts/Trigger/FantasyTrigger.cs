using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class FantasyTrigger : MonoBehaviour
{
    public GameObject player;

    public GameObject Portal_Pos;

    void Start()
    {
        player = GameObject.Find("VR_Player");
    }

    IEnumerator @PlayerMovePosCoroutine(GameObject _player, Vector3 _pos)
    {
        if (_player == null) Debug.Log("��Ż �÷��̾� ����");
        _player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
        _player.GetComponent<ContinuousTurnProviderBase>().enabled = false;
        _player.GetComponent<NavMeshAgent>().enabled = false;
        _player.transform.position = _pos;

        yield return null;

        _player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
        _player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        _player.GetComponent<NavMeshAgent>().enabled = true;

        // ���̵� ��
        FadeInOutController.Instance.FadeIn();
        FadeInOutController.Instance.FadeOut();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("��Ż ������ Ʈ����");
            player = GameObject.Find("VR_Player(Clone)");
            StartCoroutine(@PlayerMovePosCoroutine(player, Portal_Pos.transform.position));
        }

        if( other.CompareTag("Player") && GameManager.Instance.gameState == GameState.Final)
        {
            GameManager.Instance.gameState = GameState.Final;
            GameManager.Instance.ChangeScene();
        }
    }
}
