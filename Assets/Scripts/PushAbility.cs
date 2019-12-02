using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask layerMask;
    public LayerMask layerMask1;

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
        
        hit = Physics2D.Raycast(transform.position, transform.right, distance, layerMask);
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
            RaycastHit2D temp2 = Physics2D.Raycast(hit.collider.gameObject.transform.position, dir , distance * hit.collider.GetComponent<TurnoverReact>().blockLength, layerMask1);
            if (temp2.collider==null)
            {
                hit.collider.gameObject.GetComponent<TurnoverReact>().setDir(dir);
                hit.collider.gameObject.GetComponent<TurnoverReact>().turnovered = true;
            }
        }
    }
}
