using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject gear;
    public float speed;
    public bool triggered = false;
    public float offsetz = 0.5f;

    private Vector3 prePos;

    private void Start()
    {
        prePos = gear.transform.position;
    }

    private void Update()
    {
        if (triggered)
        {
            gear.transform.position = Vector3.Lerp(gear.transform.position, prePos + Vector3.back * offsetz, Time.deltaTime * speed);
        }
        else
        {
            gear.transform.position = Vector3.Lerp(gear.transform.position, prePos, Time.deltaTime * speed);
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
