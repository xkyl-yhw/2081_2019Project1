using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushTrigger : MonoBehaviour
{
    public GameObject gear;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gear.GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gear.GetComponent<Collider2D>().enabled = false;
        }
    }
}
