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
            gear.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gear.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void ReceiveMessage()
    {
        triggered = true;
    }

    public void stopMoving()
    {
        triggered = false;
    }


}
