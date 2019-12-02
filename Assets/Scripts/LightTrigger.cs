using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject gear;
    public bool triggered = false;
    public GameObject[] ReferList;
    public int type;

    private Vector3 prePos;

    private void Start()
    {
        prePos = gear.transform.position;
    }

    private void Update()
    {
        if (type == 1)
        {
            if (triggered)
                gear.GetComponent<Collider2D>().enabled = false;
            else
                gear.GetComponent<Collider2D>().enabled = true;
        }
        if (type == 2)
        {
            if (getTheSum())
            {
                gear.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                gear.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public bool getTheSum()
    {
        bool temp = false;
        foreach(GameObject item in ReferList)
        {
            if (item.GetComponent<LightTrigger>().triggered)
            {
                temp = true;
            }
        }
        return temp;
    }

    public void ReceiveMessage()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("LightTrigger"))
        {
            item.GetComponent<LightTrigger>().triggered = true;
        }
    }

    public void stopMoving()
    {
        triggered = false;
    }


}
