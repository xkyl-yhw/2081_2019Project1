using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed=2.0f;
    public GameObject player;
    //public float RotationTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target;
        Vector3 eulerAngles;
        float moveh = Input.GetAxis("Horizontal");
        float movev = Input.GetAxis("Vertical");
        Vector2 temp = new Vector2(moveh, movev);
        temp.Normalize();
        if (moveh != 0 || movev != 0)
        {
            target=new Vector3(moveh,movev,0);
           //// Debug.Log(target);
           // eulerAngles = Quaternion.FromToRotation(Vector3.right, target).eulerAngles;
           // player.transform.rotation = Quaternion.Euler(eulerAngles);
           // Debug.Log(target);
            player.transform.position += target*Time.deltaTime*speed;
        }
    }
}
