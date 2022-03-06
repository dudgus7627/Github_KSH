using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class PortalManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] rooms;
    private GameObject nextPositon;
    Vector3 nextRoom;


    [System.Serializable]
    public class PotalData
    {
        public GameObject target;
        public List<GameObject> potals;
    }

    public List<PotalData> datas;


    //포탈이 물어 본다 어디로보낼까?
    public void playerTeleport(GameObject target)
    {
        //포탈 이름을 알려주네?
        PotalData potalData = datas.Find(x => x.target == target);

        //저 포탈의 데이터를 뽑아서

        //그 데이터를 플레이어에게 적용 시킨다.
        StartCoroutine(PlayerMovceCheck(potalData.potals[1]));
        Debug.Log("Position : " + Player.transform.position);
    }

    IEnumerator PlayerMovceCheck(GameObject _target)
    {
        Player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
        Player.GetComponent<NavMeshAgent>().enabled = false;

        Player.transform.position = _target.transform.position;
        yield return null;
        Player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
        Player.GetComponent<NavMeshAgent>().enabled = true;
    }
}
