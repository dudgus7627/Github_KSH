using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    public AudioSource source;
    public AudioClip dropBullet;

    void OnCollisionEnter(Collision other)
    {
        source.PlayOneShot(dropBullet);
        Destroy(gameObject, 3f);
    }
}
