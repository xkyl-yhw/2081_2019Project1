using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeLimitIns
{
    public float minX { get; set; }
    public float maxX { get; set; }
    public Vector2 maxXPoint { get; set; }
    public Vector2 minXPoint { get; set; }
    public Vector2 originOne;
    public Vector2 farOne;
    public Vector2 leftOne;
    public Vector2 rightOne;
    public RangeLimitIns()
    {
        originOne = farOne = leftOne = rightOne = Vector2.zero;
        relate = null;
    }
    public GameObject relate;
}
public class moveLimited : MonoBehaviour
{
    [SerializeField]
    List<GameObject> inventory;
    public bool on_Range = false;
    private int index;
    List<RangeLimitIns> storeLimit = new List<RangeLimitIns>();
    public float offsetX = 0.1f;

    private void Start()
    {
        inventory = new List<GameObject>();
    }

    private void Update()
    {

        if (inventory.Count == 0) on_Range = false;
        else on_Range = true;
        for (int i = 0; i < inventory.Count; i++)
        {
            preTreatment(inventory[i]);
        }
        float[] tempy2 = { transform.position.y, transform.position.y };
        float[] tempx2 = { transform.position.x, transform.position.x };
        bool tempy3 = false;
        bool tempx3 = false;
        float[] tempy = { 0 };
        float[] tempx = { 0 };
        int temp1 = 0;
        for (int i = 0; i < storeLimit.Count; i++)
        {
            returnFunctionY(i, transform.position.x, out tempy);
            if (tempy.Length == 2)
                if (tempy[0] < transform.position.y && transform.position.y < tempy[1])
                {
                    temp1 = i;
                    if (!tempy3)
                    {
                        tempy3 = true;
                        tempy2[0] = tempy[0];
                        tempy2[1] = tempy[1];
                        continue;
                    }
                    tempy2[0] = tempy2[0] < tempy[0] ? tempy2[0] : tempy[0];
                    tempy2[1] = tempy2[1] > tempy[1] ? tempy2[1] : tempy[1];
                }
        }
        for (int i = 0; i < storeLimit.Count; i++)
        {
            returnFunctionX(i, transform.position.y, out tempx);
            if (tempx.Length == 2)
                if (tempx[0] < transform.position.x && transform.position.x < tempx[1])
                {
                    temp1 = i;
                    if (!tempx3)
                    {
                        tempx3 = true;
                        tempx2[0] = tempx[0];
                        tempx2[1] = tempx[1];
                        continue;
                    }
                    tempx2[0] = tempx2[0] < tempx[0] ? tempx2[0] : tempx[0];
                    tempx2[1] = tempx2[1] > tempx[1] ? tempx2[1] : tempx[1];
                }
        }
        if (on_Range)
        {
            Debug.Log(tempx2[0] + " " + tempx2[1]);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, tempx2[0] + offsetX, tempx2[1] - offsetX), Mathf.Clamp(transform.position.y, tempy2[0] + offsetX, tempy2[1] - offsetX));
        }
        else
        {

        }
    }

    public void returnFunctionX(int index, float y, out float[] tempX)
    {
        List<float> temp = new List<float>();
        // Debug.Log(storeLimit[index].rightOne.x + " " + storeLimit[index].originOne.x + " " + storeLimit[index].leftOne.x + " " + storeLimit[index].farOne.x);
        //从原点射出的两射线
        if (storeLimit[index].rightOne.y > storeLimit[index].originOne.y)
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.y < y && storeLimit[index].rightOne.y > y)
            {

                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        else
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.y > y && storeLimit[index].rightOne.y < y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        if (storeLimit[index].leftOne.y > storeLimit[index].originOne.y)
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.y < y && storeLimit[index].leftOne.y > y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        else
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.y > y && storeLimit[index].leftOne.y < y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        //从远点射出的两射线
        if (storeLimit[index].rightOne.y > storeLimit[index].farOne.y)
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.y < y && storeLimit[index].rightOne.y > y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        else
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.y > y && storeLimit[index].rightOne.y < y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        if (storeLimit[index].leftOne.y > storeLimit[index].farOne.y)
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.y < y && storeLimit[index].leftOne.y > y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        else
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.y > y && storeLimit[index].leftOne.y < y)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add((y - b) / k);
            }
        }
        temp.Sort();
        tempX = temp.ToArray();
    }

    public void returnFunctionY(int index, float x, out float[] tempY)
    {
        List<float> temp = new List<float>();
        // Debug.Log(storeLimit[index].rightOne.x + " " + storeLimit[index].originOne.x + " " + storeLimit[index].leftOne.x + " " + storeLimit[index].farOne.x);
        //从原点射出的两射线
        if (storeLimit[index].rightOne.x > storeLimit[index].originOne.x)
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.x < x && storeLimit[index].rightOne.x > x)
            {

                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        else
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.x > x && storeLimit[index].rightOne.x < x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        if (storeLimit[index].leftOne.x > storeLimit[index].originOne.x)
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.x < x && storeLimit[index].leftOne.x > x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        else
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].originOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].originOne.y;
            if (storeLimit[index].originOne.x > x && storeLimit[index].leftOne.x < x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        //从远点射出的两射线
        if (storeLimit[index].rightOne.x > storeLimit[index].farOne.x)
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.x < x && storeLimit[index].rightOne.x > x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        else
        {
            float x1 = storeLimit[index].rightOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].rightOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.x > x && storeLimit[index].rightOne.x < x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        if (storeLimit[index].leftOne.x > storeLimit[index].farOne.x)
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.x < x && storeLimit[index].leftOne.x > x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        else
        {
            float x1 = storeLimit[index].leftOne.x, x2 = storeLimit[index].farOne.x;
            float y1 = storeLimit[index].leftOne.y, y2 = storeLimit[index].farOne.y;
            if (storeLimit[index].farOne.x > x && storeLimit[index].leftOne.x < x)
            {
                float k = (y1 - y2) / (x1 - x2);
                float b = y1 - k * x1;
                temp.Add(x * k + b);
            }
        }
        temp.Sort();
        tempY = temp.ToArray();
    }

    private void preTreatment(GameObject templight)
    {
        if (storeLimit.FindIndex(delegate (RangeLimitIns s) { return s.relate == templight; }) != -1)
        {
            storeLimit.Remove(storeLimit.Find(delegate (RangeLimitIns s) { return s.relate == templight; }));
        }
        int indexNum = inventory.FindIndex(delegate (GameObject s) { return s == templight; });
        RangeLimitIns temp = new RangeLimitIns();
        LightShadow2D temp1 = inventory[indexNum].GetComponent<LightShadow2D>();
        temp.minX = temp.maxX = templight.transform.TransformPoint(temp1.vertices[0]).x;
        temp.maxX = temp.maxX > templight.transform.TransformPoint(temp1.vertices[1]).x ? temp.maxX : templight.transform.TransformPoint(temp1.vertices[1]).x;
        temp.minX = temp.minX < templight.transform.TransformPoint(temp1.vertices[1]).x ? temp.minX : templight.transform.TransformPoint(temp1.vertices[1]).x;
        int temp2 = temp1.vertices.Length;
        temp.maxX = temp.maxX > templight.transform.TransformPoint(temp1.vertices[temp2 - 1]).x ? temp.maxX : templight.transform.TransformPoint(temp1.vertices[temp2 - 1]).x;
        temp.minX = temp.minX < templight.transform.TransformPoint(temp1.vertices[temp2 - 1]).x ? temp.minX : templight.transform.TransformPoint(temp1.vertices[temp2 - 1]).x;
        temp.maxX = temp.maxX > templight.transform.TransformPoint(temp1.vertices[(temp2 - 1) / 2]).x ? temp.maxX : templight.transform.TransformPoint(temp1.vertices[(temp2 - 1) / 2]).x;
        temp.minX = temp.minX < templight.transform.TransformPoint(temp1.vertices[(temp2 - 1) / 2]).x ? temp.minX : templight.transform.TransformPoint(temp1.vertices[(temp2 - 1) / 2]).x;
        if (temp.maxX == templight.transform.TransformPoint(temp1.vertices[0]).x)
            temp.maxXPoint = templight.transform.TransformPoint(temp1.vertices[0]);
        else if (temp.maxX == templight.transform.TransformPoint(temp1.vertices[1]).x)
            temp.maxXPoint = templight.transform.TransformPoint(temp1.vertices[1]);
        else if (temp.maxX == templight.transform.TransformPoint(temp1.vertices[temp2 - 1]).x)
            temp.maxXPoint = templight.transform.TransformPoint(temp1.vertices[temp2 - 1]);
        else temp.maxXPoint = templight.transform.TransformPoint(temp1.vertices[(temp2 - 1) / 2]);
        if (temp.minX == templight.transform.TransformPoint(temp1.vertices[0]).x)
            temp.minXPoint = templight.transform.TransformPoint(temp1.vertices[0]);
        else if (temp.minX == templight.transform.TransformPoint(temp1.vertices[1]).x)
            temp.minXPoint = templight.transform.TransformPoint(temp1.vertices[1]);
        else if (temp.minX == templight.transform.TransformPoint(temp1.vertices[temp2 - 1]).x)
            temp.minXPoint = templight.transform.TransformPoint(temp1.vertices[temp2 - 1]);
        else temp.minXPoint = templight.transform.TransformPoint(temp1.vertices[(temp2 - 1) / 2]);
        #region
        /*        
        temp.minY = temp.maxY = temp1.vertices[0].y;        
        temp.maxY = temp.maxY > temp1.vertices[1].y ? temp.maxY : temp1.vertices[1].y;
        temp.minY = temp.minY < temp1.vertices[1].x ? temp.minY : temp1.vertices[1].y;        
        temp.maxY = temp.maxY > temp1.vertices[temp2 - 1].y ? temp.maxY : temp1.vertices[temp2 - 1].y;
        temp.minY = temp.minY < temp1.vertices[temp2 - 1].x ? temp.minY : temp1.vertices[temp2 - 1].y;       
        temp.maxY = temp.maxY > temp1.vertices[(temp2 - 1) / 2].y ? temp.maxY : temp1.vertices[(temp2 - 1) / 2].y;
        temp.minY = temp.minY < temp1.vertices[(temp2 - 1) / 2].x ? temp.minY : temp1.vertices[(temp2 - 1) / 2].y;
        if (temp.maxY == temp1.vertices[0].y)
            temp.maxYPoint = temp1.vertices[0];
        else if (temp.maxY == temp1.vertices[1].y)
            temp.maxYPoint = temp1.vertices[1];
        else if (temp.maxY == temp1.vertices[temp2 - 1].y)
            temp.maxYPoint = temp1.vertices[temp2 - 1];
        else temp.maxYPoint = temp1.vertices[(temp2 - 1) / 2];
        if (temp.minY == temp1.vertices[0].y)
            temp.minYPoint = temp1.vertices[0];
        else if (temp.minY == temp1.vertices[1].y)
            temp.minYPoint = temp1.vertices[1];
        else if (temp.minY == temp1.vertices[temp2 - 1].y)
            temp.minYPoint = temp1.vertices[temp2 - 1];
        else temp.minYPoint = temp1.vertices[(temp2 - 1) / 2];*/
        //temp.minX = inventory[indexNum].transform.TransformPoint(temp.minXPoint).x;
        //temp.maxX = inventory[indexNum].transform.TransformPoint(temp.maxXPoint).x;
        //temp.maxXPoint = inventory[indexNum].transform.TransformPoint(temp.maxXPoint);
        //temp.minXPoint = inventory[indexNum].transform.TransformPoint(temp.minXPoint);
        #endregion
        temp.originOne = inventory[indexNum].transform.TransformPoint(temp1.vertices[0]);
        temp.farOne = inventory[indexNum].transform.TransformPoint(temp1.vertices[(temp1.vertices.Length - 1) / 2]);
        temp.rightOne = inventory[indexNum].transform.TransformPoint(temp1.vertices[1]);
        temp.leftOne = inventory[indexNum].transform.TransformPoint(temp1.vertices[temp1.vertices.Length - 1]);
        temp.relate = inventory[indexNum];
        storeLimit.Add(temp);
    }

    public void addMessage(GameObject temp)
    {
        if (inventory.FindIndex(delegate (GameObject s) { return s == temp; }) == -1)
        {
            inventory.Add(temp);
            preTreatment(temp);
        }
    }

    public void minMessage(GameObject temp)
    {
        if (inventory.FindIndex(delegate (GameObject s) { return s == temp; }) != -1)
        {
            RangeLimitIns temp1 = storeLimit.Find((delegate (RangeLimitIns s) { return s.relate == temp; }));
            storeLimit.Remove(temp1);
            inventory.Remove(temp);
        }
    }
}
