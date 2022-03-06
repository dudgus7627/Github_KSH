using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolLocationCheck : MonoBehaviour
{
    [SerializeField] private Material material;
    public SimpleShoot mSimpleShoot;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Pistol"));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pistol_Location")
        {
            material.color = Color.green;
            MeshRenderer[] MeshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshr in MeshRenderers)
                meshr.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (mSimpleShoot.inventoryCheck == false && other.tag == "Pistol_Location")
        {
            material.color = Color.white;
            MeshRenderer[] MeshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshr in MeshRenderers)
                meshr.enabled = true;
        }
    }
}
