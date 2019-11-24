using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesFearLight : MonoBehaviour
{
    public Vector2 moveDir = Vector2.zero;
    public bool StartMoving;
    public float movingSpeed = 3f;
    public float checkDis = 1f;
    public LayerMask layerMask;

    private void Update()
    {
        if (StartMoving)
        {
            movingCheck();
            transform.Translate(moveDir * movingSpeed * Time.deltaTime);
        }
    }

    public void movingCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, checkDis, layerMask);
        moveDir = hit.collider == null ? moveDir : -moveDir;
    }

    public void ReceiveMessage()
    {
        StartMoving = true;
    }

    public void StopMoving()
    {
        StartMoving = false;
    }
}
