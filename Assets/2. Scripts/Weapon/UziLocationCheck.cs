using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziLocationCheck : MonoBehaviour
{
    [SerializeField] private Material material;
    public Gun mGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Uzi_Location")
        {
            material.color = Color.green;
            Renderer[] mesh = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh1 in mesh)
                mesh1.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Uzi_Location" && mGun.isinvantory == false)
        {
            material.color = Color.white;
            Renderer[] mesh = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh1 in mesh)
                mesh1.enabled = true;
        }
    }
}
