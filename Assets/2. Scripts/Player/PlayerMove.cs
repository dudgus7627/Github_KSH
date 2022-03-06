using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rigidbody;
    private float h;
    private float v;
     
    private float moveX;
    private float moveZ;
    public float speedH = 100f;
    public float speesZ = 100f;
    private bool got = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("JUMP00", -1,0);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            GameObject child = GameObject.Find("Wepon") as GameObject;
            if (!got)
            {
                child.transform.parent = this.transform;
                got = true;
            }
            else
            {
                child.transform.parent = null;
                got = false;
            }
        
        } 
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
      
        animator.SetFloat("h", h);
        animator.SetFloat("v", v);
        moveX = h * speedH * Time.deltaTime;
        moveZ = v * speesZ * Time.deltaTime;

        if(moveZ <= 0)
        {
            moveX = 0;
        }
        rigidbody.velocity = new Vector3(moveX, 0, moveZ); 
    }

}
