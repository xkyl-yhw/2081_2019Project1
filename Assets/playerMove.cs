using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{

    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
         float Vertical = Input.GetAxis("Vertical");
         Vector2 temp = new Vector2(horizontal, -Vertical);
        /* temp.Normalize();
        transform.Translate(temp * speed * Time.deltaTime);*/
        transform.Rotate(Vector3.forward * horizontal * 20 * Time.deltaTime);//左右旋转
    }
}
