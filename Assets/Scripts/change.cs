using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change : MonoBehaviour
{
    public GameObject controler;
    private string nowPlayer;
    // Start is called before the first frame update
    void Start()
    {
        nowPlayer = "player1";
        controler = GameObject.FindGameObjectWithTag(nowPlayer);
        transform.GetComponent<move>().player = controler;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (nowPlayer == "player1")
            {
                nowPlayer = "player2";
            }
            else
            {
                nowPlayer = "player1";
            }
            Debug.Log(nowPlayer);
            controler = GameObject.FindGameObjectWithTag(nowPlayer);
            transform.GetComponent<move>().player = controler;
        }
    }
}
