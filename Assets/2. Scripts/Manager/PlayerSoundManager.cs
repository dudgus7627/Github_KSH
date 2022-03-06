using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    CharacterController cc;

    private bool is_floor_check = true;

    public AudioSource source;
    public AudioClip floor_sound;
    public AudioClip road_sound;

    private float nextfire = 0.0f;
    private float fire = .5f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && Time.time > nextfire && is_floor_check)
        {
            nextfire = Time.time + fire;
            source.PlayOneShot(floor_sound);
        }
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && Time.time > nextfire && is_floor_check == false)
        {
            nextfire = Time.time + fire;
            source.PlayOneShot(road_sound);
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor")
        {
            is_floor_check = true;
        }

        if (other.tag == "Road")
        {
            is_floor_check = false;
        }
    }
}

