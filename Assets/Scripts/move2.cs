using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour
{
    public float speed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveh = Input.GetAxis("Horizontal");
        float movev = Input.GetAxis("Vertical");
        float arc=0;

        arc = Mathf.Atan2(movev, moveh) * Mathf.Rad2Deg;
    

        Debug.Log(movev + " " + moveh);
        Vector3 target = new Vector3(0,0,arc);
        Debug.Log(arc);
        Quaternion newRotation = Quaternion.Euler(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * speed );
       
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);
        //if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
        //{
        //    transform.rotation = targetRotation;                        
        //}

        //Debug.Log(transform.right);

       
    }




    public Vector3 LookRotation(Vector3 fromDir)

    {

        Vector3 eulerAngles = new Vector3();



        //AngleX = arc cos(sqrt((x^2 + z^2)/(x^2+y^2+z^2)))

        eulerAngles.x = Mathf.Acos(Mathf.Sqrt((fromDir.x * fromDir.x + fromDir.z * fromDir.z) / (fromDir.x * fromDir.x + fromDir.y * fromDir.y + fromDir.z * fromDir.z))) * Mathf.Rad2Deg;

        if (fromDir.y > 0) eulerAngles.x = 360 - eulerAngles.x;



        //AngleY = arc tan(x/z)

        eulerAngles.y = Mathf.Atan2(fromDir.x, fromDir.z) * Mathf.Rad2Deg;

        if (eulerAngles.y < 0) eulerAngles.y += 180;

        if (fromDir.x < 0) eulerAngles.y += 180;

        //AngleZ = 0

        eulerAngles.z = 0;

        return eulerAngles;

    }



}
