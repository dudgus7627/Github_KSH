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


    //��Ż�� ���� ���� ���κ�����?
    public void playerTeleport(GameObject target)
    {
        //��Ż �̸��� �˷��ֳ�?
        PotalData potalData = datas.Find(x => x.target == target);

        //�� ��Ż�� �����͸� �̾Ƽ�

        //�� �����͸� �÷��̾�� ���� ��Ų��.
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
