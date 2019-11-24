using System.Collections;
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
         
            transform.rotation = Quaternion.Euler(eulerAngles);
  
        }
    }
}
