using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandRayCast : MonoBehaviour
{
    [HideInInspector]
    public bool mbTriger = false;

    public XRController controller = null;

    private Action mAction_Enter;
    private Action mAction_Exit;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    private void OnEnable()
    {
        mAction_Enter += none;
        mAction_Exit += none;

        mAction_Enter += OnEnter;
        mAction_Exit += OnExit;
    }

    void none()
    { 
    
    }

    private void OnDisable()
    {
        mAction_Enter -= none;
        mAction_Exit -= none;

        mAction_Enter -= OnEnter;
        mAction_Exit -= OnExit;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger)) { }

        if (trigger)
        {
            if (mbTriger != trigger)
            {
                mbTriger = trigger;
                
                mAction_Enter();
            }
        }
        else
        {
            if (mbTriger != trigger)
            {
                mbTriger = trigger;
                
                mAction_Exit();
            }
        }
    }

    public float MaxDistance = 15;

    private void OnEnter()
    {
        RaycastHit hit;

        //Debug.Log("On");
        Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider != null)
            {
                switch (GameManager.Instance.gameState)
                {
                    case GameState.Fantasy1:
                        fantasyCall(hit.collider.gameObject);
                        break;
                    case GameState.Reality1:
                        homeCall(hit.collider.gameObject);
                        break;
                }
            }
        }
    }

    private void OnExit()
    {

        //Debug.Log("Off");
    }

    private void fantasyCall(GameObject _obj)
    {
        if (_obj == null)
            return;

        NPC npc = _obj.GetComponent<NPC>();
        if(npc != null)
        {
            switch (npc.mNPCType)
            {
                case NPCType.GenernalNPC :
                    // ÀÏ¹Ý NPC UI ¿ÀÇÂ
                    _obj.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case NPCType.ShopNPC:
                    break;
            }
            
        }

    }

    private void homeCall(GameObject _obj)
    {
        if (_obj == null)
            return;

        NPC npc = _obj.GetComponent<NPC>();

        if (npc != null)
        {
            switch (npc.mNPCType)
            {
                case NPCType.Parent:
                    // ÀÏ¹Ý NPC UI ¿ÀÇÂ
                    _obj.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case NPCType.ShopNPC:
                    break;
            }
        }
    }
}



