using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTextIntraction : MonoBehaviour
{
    public GameObject no1;
    public GameObject no2;
    public GameObject no3;
    public GameObject no4;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Par")
        {
            no1.gameObject.SetActive(true);
        }     
        if(other.gameObject.tag == "Door")
        {
            no4.gameObject.SetActive(true);
        }
    }
    public void No1()
    {
        no1.gameObject.SetActive(false);
        no2.gameObject.SetActive(true);
    }
    public void No2()
    {
        no2.gameObject.SetActive(false);
        no3.gameObject.SetActive(true);
    }
   public void No3()
    {
        no3.gameObject.SetActive(false);
    }
    public void No4()
    {
        no4.gameObject.SetActive(false);
    }
}
