﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask layerMask;
    public float pushLength = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Raycheck();
        }
    }

    private void Raycheck()
    {
        Vector2 dir = Vector2.zero;
        RaycastHit2D hit;
        Debug.DrawRay(transform.position, transform.up, Color.red);
        hit = Physics2D.Raycast(transform.position, transform.up, distance, layerMask);
        if (hit.collider != null)
        {
            Vector2 tempV2 = hit.collider.transform.position - transform.position;
            tempV2.Normalize();
            if (Mathf.Abs(tempV2.x) > Mathf.Abs(tempV2.y))
            {
                dir = tempV2.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            if (Mathf.Abs(tempV2.x) < Mathf.Abs(tempV2.y))
            {
                dir = tempV2.y > 0 ? Vector2.up : Vector2.down;
            }
            else
            {
                return;
            }
            if (!Physics2D.Raycast(hit.collider.gameObject.transform.position, dir, distance, LayerMask.NameToLayer("Wall")))
            {
                hit.collider.gameObject.GetComponent<TurnoverReact>().setDir(dir*pushLength);
                hit.collider.gameObject.GetComponent<TurnoverReact>().turnovered = true;
            }
        }
    }
}
