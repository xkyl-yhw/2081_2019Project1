﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public GameObject angel;
    public float dis=3.0f;
    private Vector3 target;
    private Vector3 eulerAngles;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        angel = GameObject.FindGameObjectWithTag("player1");
    }

    // Update is called once per frame
    void Update()
    {
        target = angel.transform.position - transform.position;
        eulerAngles = Quaternion.FromToRotation(Vector3.right, target).eulerAngles;
        distance = Vector3.Distance(transform.position, angel.transform.position);
        if (Input.GetKey(KeyCode.L)&&distance<=dis)
        {


            GameObject.Find("playerControl").GetComponent<move>().enabled = false;
            angel.transform.position = transform.position + transform.right *1;
            float movev = Input.GetAxis("Horizontal");
            if (movev!=0)
            {
                transform.Rotate(new Vector3(0,0,-1*movev));
            }
            //target = angel.transform.position - transform.position;
            //eulerAngles = Quaternion.FromToRotation(Vector3.right, target).eulerAngles;
            //distance = Vector3.Distance(transform.position, angel.transform.position);
           // angel.transform.position = transform.position+transform.forward*3;
         
            //transform.rotation = Quaternion.Euler(eulerAngles);
           

        }
        if (!Input.GetKey(KeyCode.L))
        {
            GameObject.Find("playerControl").GetComponent<move>().enabled = true;
        }
    }
}
