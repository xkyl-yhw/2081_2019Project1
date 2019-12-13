using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 2.0f;
    public float arc_speed = 2.0f;
    public GameObject player;
    //public float RotationTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 target;

        float moveh = Input.GetAxis("Horizontal");
        float movev = Input.GetAxis("Vertical");
        float arc = 0;

        if (moveh != 0 || movev != 0)
        {
            arc = Mathf.Atan2(movev, moveh) * Mathf.Rad2Deg;
            Vector3 target_arc = new Vector3(0, 0, arc);
            Quaternion newRotation = Quaternion.Euler(target_arc);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, newRotation, Time.deltaTime * arc_speed);

            target = new Vector3(moveh, movev, 0);
            player.transform.position += target * Time.deltaTime * speed;
        }
    }
}
