using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeLocationCheck : MonoBehaviour
{
    [SerializeField] private Material material;
    public Axe axe;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Axe_Location")
        {
            material.color = Color.green;
            //transform.GetComponent<MeshRenderer>().enabled = false;
            Renderer[] mesh = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh1 in mesh)
                mesh1.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (axe.axeInventory == false && other.tag == "Axe_Location")
        {
            material.color = Color.white;
            Renderer[] mesh = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh1 in mesh)
                mesh1.enabled = true;
        }
    }
}
