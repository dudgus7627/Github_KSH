using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTextState : MonoBehaviour
{
    public GameObject no1;
    public GameObject no2;
    public GameObject no3;
    public GameObject no4;
    private void Start()
    {
        no1.gameObject.SetActive(false);
        no2.gameObject.SetActive(false);
        no3.gameObject.SetActive(false);
        no4.gameObject.SetActive(false);
    }

}
