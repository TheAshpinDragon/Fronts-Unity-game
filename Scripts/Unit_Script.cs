using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Script : MonoBehaviour
{
    public GameObject[] UnitArray; // All individual units attatched to this culmative unit
    public string[] Formation;
    private Vector3 GoalPos;
    private Vector3[] GoalPosPath;
    private float AdvanceSpeed;
    private float GoalAdvanceSpeed;
    private bool InCombat = false;
    private bool Selected = false;

    public Vector3 GlobalMousePos;
    public GameObject CameraObj;
    private Camera Camera;
    private int layerMask = 1 << 8;

    public float CHealth;//c = culmative stat for all individual units
    public float CArmor;
    public float CDamage;
    public float CMoral = 100;

    void Start()
    {
        Camera = Camera.main;
    }
    
    void Update()
    {
        GlobalMousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.transform.position.z));

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(GlobalMousePos, fwd, Mathf.Infinity))
            print("There is something in front of the object!");
    }

    void FixedUpdate()
    {
        GlobalMousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.transform.position.z));
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(GlobalMousePos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else 
            {
                Debug.Log("Did not Hit");
                Debug.DrawRay(GlobalMousePos, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }
    }
}
