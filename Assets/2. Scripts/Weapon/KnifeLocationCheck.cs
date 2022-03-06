using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeLocationCheck : MonoBehaviour
{
    [SerializeField] private Material material;
    public Knife knife;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Knife_Location")
        {
            material.color = Color.green;
            MeshRenderer[] mesh = transform.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh1 in mesh)
                mesh1.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Knife_Location" && knife.knifeInventory == false)
        {
            material.color = Color.white;
            MeshRenderer[] mesh = transform.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh1 in mesh)
                mesh1.enabled = true;
        }
    }
}
