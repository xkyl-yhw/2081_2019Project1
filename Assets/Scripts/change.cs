using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change : MonoBehaviour
{
    List<GameObject> playerList = new List<GameObject>();
    public GameObject controler;
    // Start is called before the first frame update
    void Start()
    {
        playerList.Add(GameObject.FindGameObjectWithTag("player1"));
        playerList.Add(GameObject.FindGameObjectWithTag("player2"));
        controler = playerList[0];
        playerList.Reverse();
        transform.GetComponent<move>().player = controler;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            changeController();
            transform.GetComponent<move>().player = controler;
        }
    }

    public void changeController()
    {
        if (playerList[0].activeSelf == true)
        {
            controler = playerList[0];
            playerList.Reverse();
        }
    }
}
