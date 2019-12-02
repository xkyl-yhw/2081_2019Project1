using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LightTriggerIns
{
    public GameObject Object;
    public bool isActive;
}

public class LightTriggerManager : MonoBehaviour
{
    [SerializeField]
    List<LightTriggerIns> lightTriggerInv = new List<LightTriggerIns>();

    private void Start()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("LightTrigger"))
        {
            LightTriggerIns temp = new LightTriggerIns();
            temp.Object = item;
            temp.isActive = item.GetComponent<LightTrigger>().triggered;
            lightTriggerInv.Add(temp);
        }
    }

    private void Update()
    {
        RefreshList();   
    }

    private void RefreshList()
    {
        lightTriggerInv.ForEach(item =>
        {
            item.isActive = false;
        });
        foreach (LightShadow2D item in GameObject.FindObjectsOfType<LightShadow2D>())
        {
            int temp = lightTriggerInv.FindIndex(delegate (LightTriggerIns s) { return s.Object == item.lastTrigger; });
            if (temp != -1)
                lightTriggerInv[temp].isActive = true;
        }
        lightTriggerInv.ForEach(item =>
        {
                item.Object.GetComponent<LightTrigger>().triggered = item.isActive;
        });
    }
}
