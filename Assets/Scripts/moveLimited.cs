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
    List<RangeLimitIns> storeLimit;
    public float offsetX=0.1f;

    private void Start()
    {
        inventory = new List<GameObject>();
        storeLimit = new List<RangeLimitIns>();
    }

    private void Update()
    {
        if (inventory.Count == 0) on_Range = false;
        else on_Range = true;
        for (int i = 0; i < inventory.Count; i++)
        {
            preTreatment(i);
        }
        float[] temp= {0};
        int temp1=0;
        for (int i = 0; i < storeLimit.Count; i++)
        {
            returnFunctionY(i, transform.position.x,out temp);
            if(temp.Length==2)
            if (temp[0] < transform.position.y && transform.position.y < temp[1])
            {
                temp1 = i;
                break;
            }
        }
        if (on_Range)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x,storeLimit[temp1].minX+offsetX,storeLimit[temp1].maxX-offsetX),Mathf.Clamp(transform.position.y, temp[0],temp[1]));
        }
    }

    public void returnFunctionY(int index, float x,out float[] tempY )
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

                float k = (y1-y2) / (x1-x2);
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

    private void preTreatment(int indexNum)
    {
        if (storeLimit.FindIndex(delegate (RangeLimitIns s) { return s.relate == inventory[indexNum]; }) != -1)
        {
            storeLimit.Remove(storeLimit.Find(delegate (RangeLimitIns s) { return s.relate == inventory[indexNum]; }));
        }
        RangeLimitIns temp = new RangeLimitIns();
        LightShadow2D temp1 = inventory[indexNum].GetComponent<LightShadow2D>();
        temp.minX = temp.maxX = temp1.vertices[0].x;
        temp.maxX = temp.maxX > temp1.vertices[1].x ? temp.maxX : temp1.vertices[1].x;
        temp.minX = temp.minX < temp1.vertices[1].x ? temp.minX : temp1.vertices[1].x;
        int temp2 = temp1.vertices.Length;
        temp.maxX = temp.maxX > temp1.vertices[temp2 - 1].x ? temp.maxX : temp1.vertices[temp2 - 1].x;
        temp.minX = temp.minX < temp1.vertices[temp2 - 1].x ? temp.minX : temp1.vertices[temp2 - 1].x;
        temp.maxX = temp.maxX > temp1.vertices[(temp2 - 1) / 2].x ? temp.maxX : temp1.vertices[(temp2 - 1) / 2].x;
        temp.minX = temp.minX < temp1.vertices[(temp2 - 1) / 2].x ? temp.minX : temp1.vertices[(temp2 - 1) / 2].x;
        if (temp.maxX == temp1.vertices[0].x)
            temp.maxXPoint = temp1.vertices[0];
        else if (temp.maxX == temp1.vertices[1].x)
            temp.maxXPoint = temp1.vertices[1];
        else if (temp.maxX == temp1.vertices[temp2 - 1].x)
            temp.maxXPoint = temp1.vertices[temp2 - 1];
        else temp.maxXPoint = temp1.vertices[(temp2 - 1) / 2];
        if (temp.minX == temp1.vertices[0].x)
            temp.minXPoint = temp1.vertices[0];
        else if (temp.minX == temp1.vertices[1].x)
            temp.minXPoint = temp1.vertices[1];
        else if (temp.minX == temp1.vertices[temp2 - 1].x)
            temp.minXPoint = temp1.vertices[temp2 - 1];
        else temp.minXPoint = temp1.vertices[(temp2 - 1) / 2];
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
        #endregion
        temp.minX = inventory[indexNum].transform.TransformPoint(temp.minXPoint).x;
        temp.maxX = inventory[indexNum].transform.TransformPoint(temp.maxXPoint).x;
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
            preTreatment(inventory.FindIndex(delegate (GameObject s) { return s == temp; }));
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
