using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject gear;
    public bool triggered = false;

    private Vector3 prePos;

    private void Start()
    {
        prePos = gear.transform.position;
    }

    private void Update()
    {
        if (triggered)
        {
            gear.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            gear.GetComponent<Collider2D>().enabled = true;
        }
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
