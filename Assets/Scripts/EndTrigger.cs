using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "player1")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().angleFin();
            collision.collider.transform.parent.GetComponent<change>().changeController();
            collision.collider.gameObject.SetActive(false);
        }
        if (collision.collider.tag == "player2")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().devilFin();
            collision.collider.transform.parent.GetComponent<change>().changeController();
            collision.collider.gameObject.SetActive(false);
        }
    }
}
