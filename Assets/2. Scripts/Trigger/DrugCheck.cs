using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugCheck : MonoBehaviour
{
    public AudioSource source;
    Vector3 startPos;

    private void Start()
    {
        Debug.Log("DrugCheck_Beta Start");
        startPos = transform.position;
        source.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Drug OnTrigger");
        if(other.CompareTag("PlayerHand"))
        {
            
            Debug.Log("Try Drug");
            
            // ���� ���� UI
            HomeButtonController.Instance.drugUI.SetActive(true);

            source.enabled = true;


            // ���� ������Ʈ ���߱�
            //Destroy(transform.parent.gameObject);
            //Destroy(transform.gameObject);
            transform.gameObject.SetActive(false);
            transform.position = startPos;

            HomeTrigger.Instance.tryDrug = true;
            GameObject.Find("VR_Player").GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
