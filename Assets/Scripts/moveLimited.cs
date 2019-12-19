using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeLimitIns
{
    /*
   public float minX { get; set; }
   public float maxX { get; set; }
   public Vector2 maxXPoint { get; set; }
   public Vector2 minXPoint { get; set; }
   public Vector2 originOne;
   public Vector2 farOne;
   public Vector2 leftOne;
   public Vector2 rightOne;
   */
    public Vector2[] verticesForRange;
    public RangeLimitIns()
    {
        // originOne = farOne = leftOne = rightOne = Vector2.zero;
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
    public int segmentForRange = 5;

    private void Start()
    {
        inventory = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (inventory.Count == 0)
        {
            return;
            on_Range = false;
        }
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
        for (int i = 0; i < storeLimit.Count; i++)
        {
            returnFunctionY(i, transform.position.x, out tempy);
            if (tempy.Length == 2)
                if (tempy[0] < transform.position.y && transform.position.y < tempy[1])
                {
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
                    Debug.Log(1);
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
            //Debug.Log(tempx2[0] + " " + tempx2[1]);
            //Debug.Log(tempy2[0] + " " + tempy2[1]);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, tempx2[0] + offsetX, tempx2[1] - offsetX), Mathf.Clamp(transform.position.y, tempy2[0] + offsetX, tempy2[1] - offsetX));
        }
        else
        {

        }
    }


    private void returnFunctionY(int index, float x, out float[] tempY)
    {
        float y1, y2, x1, x2;
        List<float> temp = new List<float>();
        for (int i = 0; i < storeLimit[index].verticesForRange.Length - 1; i++)
        {
            y1 = storeLimit[index].verticesForRange[i].y;
            y2 = storeLimit[index].verticesForRange[i + 1].y;
            x1 = storeLimit[index].verticesForRange[i].x;
            x2 = storeLimit[index].verticesForRange[i + 1].x;
            if (y1 != y2)
            {
                if (x1 > x2)
                {
                    if (x1 >= x && x2 <= x)
                    {
                        float k = (y1 - y2) / (x1 - x2);
                        float b = y1 - k * x1;
                        if (temp.FindIndex(s => (Mathf.Abs(s - ((k * x + b))) <= 0.0001)) == -1)
                            temp.Add(k * x + b);
                    }
                }
                else if (x2 > x1)
                {
                    if (x2 >= x && x1 <= x)
                    {
                        float k = (y1 - y2) / (x1 - x2);
                        float b = y1 - k * x1;
                        if (temp.FindIndex(s => (Mathf.Abs(s - ((k * x + b))) <= 0.0001)) == -1)
                            temp.Add(k * x + b);
                    }
                }

            }
            else
            {
                if (temp.FindIndex(s => (Mathf.Abs(s - y1) <= 0.0001)) == -1)
                    temp.Add(y1);
            }

        }
        y1 = storeLimit[index].verticesForRange[0].y;
        y2 = storeLimit[index].verticesForRange[storeLimit[index].verticesForRange.Length - 1].y;
        x1 = storeLimit[index].verticesForRange[0].x;
        x2 = storeLimit[index].verticesForRange[storeLimit[index].verticesForRange.Length - 1].x;
        if (y1 != y2)
        {
            if (x1 > x2)
            {
                if (x1 >= x && x2 <= x)
                {
                    float k = (y1 - y2) / (x1 - x2);
                    float b = y1 - k * x1;
                    if (temp.FindIndex(s => (Mathf.Abs(s - ((k * x + b))) <= 0.0001)) == -1)
                        temp.Add(k * x + b);
                }
            }
            else if (x2 > x1)
            {
                if (x2 >= x && x1 <= x)
                {
                    float k = (y1 - y2) / (x1 - x2);
                    float b = y1 - k * x1;
                    if (temp.FindIndex(s => (Mathf.Abs(s - ((k * x + b))) <= 0.0001)) == -1)
                        temp.Add(k * x + b);
                }
            }
        }
        else
        {
            if (temp.FindIndex(s => (Mathf.Abs(s - y1) <= 0.0001)) == -1)
                temp.Add(y1);
        }
        temp.Sort();
        if (temp.Count > 2)
        {
            float minY = temp[0];
            float maxY = temp[temp.Count - 1];
            temp.Clear();
            temp.Add(minY);
            temp.Add(maxY);
        }
        temp.Sort();
        tempY = temp.ToArray();
    }

    private void returnFunctionX(int index, float y, out float[] tempX)
    {
        float x1, x2, y1, y2;
        List<float> temp = new List<float>();
        for (int i = 0; i < storeLimit[index].verticesForRange.Length - 1; i++)
        {
            y1 = storeLimit[index].verticesForRange[i].y;
            y2 = storeLimit[index].verticesForRange[i + 1].y;
            x1 = storeLimit[index].verticesForRange[i].x;
            x2 = storeLimit[index].verticesForRange[i + 1].x;
            if (x1 != x2)
            {
                if (y1 > y2)
                {
                    if (y1 >= y && y2 <= y)
                    {
                        float k = (y1 - y2) / (x1 - x2);
                        float b = y1 - k * x1;
                        if (temp.FindIndex(s => ((Mathf.Abs(s - ((y - b) / k))) <= 0.0001)) == -1)
                            temp.Add((y - b) / k);
                    }
                }
                else if (y2 > y1)
                {
                    if (y2 >= y && y1 <= y)
                    {
                        float k = (y1 - y2) / (x1 - x2);
                        float b = y1 - k * x1;
                        if (temp.FindIndex(s => ((Mathf.Abs(s - ((y - b) / k))) <= 0.0001)) == -1)
                            temp.Add((y - b) / k);
                    }
                }
            }
            else
            {
                if (temp.FindIndex(s => ((Mathf.Abs(s - x1)) <= 0.0001)) == -1)
                    temp.Add(x1);
            }

        }
        y1 = storeLimit[index].verticesForRange[0].y;
        y2 = storeLimit[index].verticesForRange[storeLimit[index].verticesForRange.Length - 1].y;
        x1 = storeLimit[index].verticesForRange[0].x;
        x2 = storeLimit[index].verticesForRange[storeLimit[index].verticesForRange.Length - 1].x;
        if (x1 != x2)
        {
            if (y1 > y2)
            {
                if (y1 >= y && y2 <= y)
                {
                    float k = (y1 - y2) / (x1 - x2);
                    float b = y1 - k * x1;
                    if (temp.FindIndex(s => ((Mathf.Abs(s - ((y - b) / k))) <= 0.0001)) == -1)
                        temp.Add((y - b) / k);
                }
            }
            else if (y2 > y1)
            {
                if (y2 >= y && y1 <= y)
                {
                    float k = (y1 - y2) / (x1 - x2);
                    float b = y1 - k * x1;
                    if (temp.FindIndex(s => ((Mathf.Abs(s - ((y - b) / k))) <= 0.0001)) == -1)
                        temp.Add((y - b) / k);
                }
            }
        }
        else
        {
            if (temp.FindIndex(s => ((Mathf.Abs(s - x1)) <= 0.0001)) == -1)
                temp.Add(x1);
        }
        temp.Sort();
        if (temp.Count > 2)
        {
            float minX = temp[0];
            float maxX = temp[temp.Count - 1];
            temp.Clear();
            temp.Add(minX);
            temp.Add(maxX);
        }
        temp.Sort();
        tempX = temp.ToArray();
    }

    #region
    /*
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
    */
    #endregion

    private void preTreatment(GameObject templight)
    {
        if (storeLimit.FindIndex(delegate (RangeLimitIns s) { return s.relate == templight; }) != -1)
        {
            storeLimit.Remove(storeLimit.Find(delegate (RangeLimitIns s) { return s.relate == templight; }));
        }
        int indexNum = inventory.FindIndex(delegate (GameObject s) { return s == templight; });
        RangeLimitIns temp = new RangeLimitIns();
        LightShadow2D temp1 = inventory[indexNum].GetComponent<LightShadow2D>();
        List<Vector2> temp2 = new List<Vector2>();
        int interNum = temp1.vertices.Length / (segmentForRange - 1);
        temp2.Add(new Vector2(templight.transform.TransformPoint(temp1.vertices[0]).x, templight.transform.TransformPoint(temp1.vertices[0]).y));
        for (int i = 1; i < temp1.vertices.Length - 1; i += interNum)
        {
            temp2.Add(new Vector2(templight.transform.TransformPoint(temp1.vertices[i]).x, templight.transform.TransformPoint(temp1.vertices[i]).y));
        }
        temp2.Add(new Vector2(templight.transform.TransformPoint(temp1.vertices[temp1.vertices.Length - 1]).x, templight.transform.TransformPoint(temp1.vertices[temp1.vertices.Length - 1]).y));
        List<int> deleteMark = new List<int>();
        for (int i = 1; i < temp2.Count / 2; i++)
        {
            if (temp2[i - 1].x == temp2[i].x)
                if (temp2[i].x == temp2[i + 1].x)
                {
                    if (deleteMark.FindIndex(delegate (int s) { return s == i; }) == -1)
                        deleteMark.Add(i);
                }
            if (temp2[i - 1].y == temp2[i].y)
                if (temp2[i].y == temp2[i + 1].y)
                {
                    if (deleteMark.FindIndex(delegate (int s) { return s == i; }) == -1)
                        deleteMark.Add(i);
                }
        }
        for (int i = temp2.Count - 2; i > temp2.Count / 2; i--)
        {
            if (temp2[i - 1].x == temp2[i].x)
                if (temp2[i].x == temp2[i + 1].x)
                {
                    if (deleteMark.FindIndex(delegate (int s) { return s == i; }) == -1)
                        deleteMark.Add(i);
                }
            if (temp2[i - 1].y == temp2[i].y)
                if (temp2[i].y == temp2[i + 1].y)
                {
                    if (deleteMark.FindIndex(delegate (int s) { return s == i; }) == -1)
                        deleteMark.Add(i);
                }
        }
        deleteMark.Sort();
        for (int i = 0; i < deleteMark.Count; i++)
        {
            Debug.DrawLine(temp2[0], temp2[deleteMark[i]], Color.red);
        }
        for (int i = deleteMark.Count - 1; i >= 0; i--)
        {
            temp2.RemoveAt(deleteMark[i]);
        }
        temp.relate = templight;
        temp.verticesForRange = temp2.ToArray();
        //Debug.Log(temp2[0]+" "+temp2[1]+" "+temp2[2]+" "+temp2[3]);
        #region
        /*   
        temp.minX = temp.maxX = templight.transform.TransformPoint(temp1.verticesForRangeForRange[0]).x;
        temp.maxX = temp.maxX > templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]).x ? temp.maxX : templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]).x;
        temp.minX = temp.minX < templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]).x ? temp.minX : templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]).x;
        int temp2 = temp1.verticesForRangeForRange.Length;
        temp.maxX = temp.maxX > templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]).x ? temp.maxX : templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]).x;
        temp.minX = temp.minX < templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]).x ? temp.minX : templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]).x;
        temp.maxX = temp.maxX > templight.transform.TransformPoint(temp1.verticesForRangeForRange[(temp2 - 1) / 2]).x ? temp.maxX : templight.transform.TransformPoint(temp1.verticesForRangeForRange[(temp2 - 1) / 2]).x;
        temp.minX = temp.minX < templight.transform.TransformPoint(temp1.verticesForRangeForRange[(temp2 - 1) / 2]).x ? temp.minX : templight.transform.TransformPoint(temp1.verticesForRangeForRange[(temp2 - 1) / 2]).x;
        if (temp.maxX == templight.transform.TransformPoint(temp1.verticesForRangeForRange[0]).x)
            temp.maxXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[0]);
        else if (temp.maxX == templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]).x)
            temp.maxXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]);
        else if (temp.maxX == templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]).x)
            temp.maxXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]);
        else temp.maxXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[(temp2 - 1) / 2]);
        if (temp.minX == templight.transform.TransformPoint(temp1.verticesForRangeForRange[0]).x)
            temp.minXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[0]);
        else if (temp.minX == templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]).x)
            temp.minXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[1]);
        else if (temp.minX == templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]).x)
            temp.minXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[temp2 - 1]);
        else temp.minXPoint = templight.transform.TransformPoint(temp1.verticesForRangeForRange[(temp2 - 1) / 2]);
        temp.minY = temp.maxY = temp1.verticesForRangeForRange[0].y;        
        temp.maxY = temp.maxY > temp1.verticesForRangeForRange[1].y ? temp.maxY : temp1.verticesForRangeForRange[1].y;
        temp.minY = temp.minY < temp1.verticesForRangeForRange[1].x ? temp.minY : temp1.verticesForRangeForRange[1].y;        
        temp.maxY = temp.maxY > temp1.verticesForRangeForRange[temp2 - 1].y ? temp.maxY : temp1.verticesForRangeForRange[temp2 - 1].y;
        temp.minY = temp.minY < temp1.verticesForRangeForRange[temp2 - 1].x ? temp.minY : temp1.verticesForRange[temp2 - 1].y;       
        temp.maxY = temp.maxY > temp1.verticesForRange[(temp2 - 1) / 2].y ? temp.maxY : temp1.verticesForRange[(temp2 - 1) / 2].y;
        temp.minY = temp.minY < temp1.verticesForRange[(temp2 - 1) / 2].x ? temp.minY : temp1.verticesForRange[(temp2 - 1) / 2].y;
        if (temp.maxY == temp1.verticesForRange[0].y)
            temp.maxYPoint = temp1.verticesForRange[0];
        else if (temp.maxY == temp1.verticesForRange[1].y)
            temp.maxYPoint = temp1.verticesForRange[1];
        else if (temp.maxY == temp1.verticesForRange[temp2 - 1].y)
            temp.maxYPoint = temp1.verticesForRange[temp2 - 1];
        else temp.maxYPoint = temp1.verticesForRange[(temp2 - 1) / 2];
        if (temp.minY == temp1.verticesForRange[0].y)
            temp.minYPoint = temp1.verticesForRange[0];
        else if (temp.minY == temp1.verticesForRange[1].y)
            temp.minYPoint = temp1.verticesForRange[1];
        else if (temp.minY == temp1.verticesForRange[temp2 - 1].y)
            temp.minYPoint = temp1.verticesForRange[temp2 - 1];
        else temp.minYPoint = temp1.verticesForRange[(temp2 - 1) / 2];
        //temp.minX = inventory[indexNum].transform.TransformPoint(temp.minXPoint).x;
        //temp.maxX = inventory[indexNum].transform.TransformPoint(temp.maxXPoint).x;
        //temp.maxXPoint = inventory[indexNum].transform.TransformPoint(temp.maxXPoint);
        //temp.minXPoint = inventory[indexNum].transform.TransformPoint(temp.minXPoint);
        
        temp.originOne = inventory[indexNum].transform.TransformPoint(temp1.verticesForRange[0]);
        temp.farOne = inventory[indexNum].transform.TransformPoint(temp1.verticesForRange[(temp1.verticesForRange.Length - 1) / 2]);
        temp.rightOne = inventory[indexNum].transform.TransformPoint(temp1.verticesForRange[1]);
        temp.leftOne = inventory[indexNum].transform.TransformPoint(temp1.verticesForRange[temp1.verticesForRange.Length - 1]);
        temp.relate = inventory[indexNum];
        */
        #endregion
        storeLimit.Add(temp);
    }

    public void addMessage(GameObject temp)
    {
        if (inventory.FindIndex(delegate (GameObject s) { return s == temp; }) == -1)
        {
            inventory.Add(temp);
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
