using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatedWall : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDir;
    public Vector2 size;
    public LayerMask mask;

    private void Start()
    {
        transform.localScale = size;
    }

    private void Update()
    {
        float length = moveDir.x != 0 ? size.x : size.y;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        Debug.DrawRay(transform.position, moveDir);
        if (Physics2D.Raycast(transform.position, moveDir,length / 2 + 0.1f,mask))
        {
            moveDir = -moveDir;
        }
    }
}
