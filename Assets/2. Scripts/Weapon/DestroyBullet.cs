using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DestoryBulletNow());
    }

    IEnumerator DestoryBulletNow()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Enter : " + other.gameObject.name);
        Destroy(gameObject);
    }
}
