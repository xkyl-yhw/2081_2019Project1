﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class LightShadow2D : MonoBehaviour
{
    public float range = 4f;
    [Range(0, 360)]
    public float angle = 360;
    public int segments = 50;
    public LayerMask cullingMask = -1;
    public LayerMask playerMask;
    public Color color = Color.white;
    public int typeOfLight = 1;
    public bool getLightSorce = false;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Material material;

    [HideInInspector]
    public Vector3[] vertices;
    private int[] triangles;
    private GameObject lastLight;
    public GameObject lastTrigger;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = null;
        meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(Shader.Find("Sprites/Default"));
        material.SetColor("_Color", color);
        meshRenderer.sharedMaterial = material;
        if (typeOfLight == 1)
        {
            RayCheck(Vector2.zero);
        }
    }

    private void Update()
    {
        if (typeOfLight == 1)
        {
            RayCheck(Vector2.zero);
        }
        else if (typeOfLight == 2)
        {
            if (getLightSorce)
            {
                RayCheck(this.transform.parent.position);
            }
            else
            {
                meshFilter.mesh = null;
            }
        }
    }

    public void RayCheck(Vector3 offset)
    {
        bool trigger1 = false;
        bool trigger2 = false;
        bool trigger3 = false;
        bool trigger4 = false;
        bool trigger5 = false;
        range = Mathf.Clamp(range, 0, range);
        material.SetColor("_Color", color);
        vertices = new Vector3[segments + 1];
        vertices[0] = transform.InverseTransformPoint(transform.localPosition + offset);

        float tempAngle = angle * Mathf.Deg2Rad;
        float currentAngle = tempAngle / 2;
        float interAngle = tempAngle / segments;
        for (int i = 0; i < segments; i++)
        {
            Vector2 dir = new Vector2(Mathf.Cos(currentAngle + countAngle(transform.right, Vector2.right)), Mathf.Sin(currentAngle + countAngle(transform.right, Vector2.right)));
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition + offset, dir, range, cullingMask);
            if (!trigger4)
            {
                RaycastHit2D hit1 = Physics2D.Raycast(transform.localPosition + offset, dir, range, playerMask);
                if (hit1.collider != null)
                {
                    if (hit1.collider.gameObject.tag == "player1")
                    {
                        RaycastHit2D[] hit2 = Physics2D.RaycastAll(transform.localPosition + offset, dir, range, playerMask);
                        foreach (var item in hit2)
                        {
                            if (item.collider.gameObject.tag == "player2")
                            {
                                trigger5 = true;
                                GameObject.Find("GameManager").GetComponent<GameManager>().devilDead();
                            }
                        }
                        trigger4 = true;
                        hit1.collider.GetComponent<moveLimited>().addMessage(this.gameObject);

                    }
                    if (hit1.collider.gameObject.tag == "player2")
                    {
                        trigger5 = true;
                        GameObject.Find("GameManager").GetComponent<GameManager>().devilDead();
                    }
                }
            }
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("sunLight"))
                {
                    trigger3 = true;
                    if (hit.collider.gameObject.transform.GetChild(0).GetComponent<LightShadow2D>().typeOfLight == 2 && !hit.collider.gameObject.transform.GetChild(0).GetComponent<LightShadow2D>().getLightSorce)
                    {
                        lastLight = hit.collider.gameObject.transform.GetChild(0).gameObject;
                        hit.collider.gameObject.transform.GetChild(0).GetComponent<LightShadow2D>().getLightSorce = true;
                    }
                }
                if (hit.collider.gameObject.tag == "Anim")
                {
                    SendToFear(hit.collider.gameObject);
                    trigger1 = true;
                }
                if (hit.collider.gameObject.tag == "LightTrigger")
                {
                    lastTrigger = hit.collider.gameObject;
                    trigger2 = true;
                }
            }
            float realDis = hit.collider == null ? range : hit.distance;
            Vector2 endPoint = new Vector2(transform.localPosition.x + offset.x + realDis * dir.x / dir.magnitude, transform.localPosition.y + offset.y + realDis * dir.y / dir.magnitude);
            endPoint = transform.InverseTransformPoint(endPoint);
            vertices[i + 1] = endPoint;
            currentAngle -= interAngle;
        }
        if (!trigger1)
            clearFear();
        if (!trigger2)
        {
            lastTrigger = null;
        }
        if (!trigger3)
        {
            if (lastLight != null)
            {
                lastLight.GetComponent<LightShadow2D>().getLightSorce = false;
            }
        }
        if (!trigger4)
        {
            if (GameObject.FindGameObjectWithTag("player1") != null)
                GameObject.FindGameObjectWithTag("player1").GetComponent<moveLimited>().minMessage(this.gameObject);
        }
        CreateLightMesh();
    }

    public void CreateLightMesh()
    {
        triangles = new int[segments * 3];
        for (int i = 0, vi = 1; i < segments * 3 - 3; i += 3, vi++)
        {
            triangles[i] = 0;
            triangles[i + 1] = vi;
            triangles[i + 2] = vi + 1;
        }
        if (segments != 0 && angle == 360)
        {
            triangles[segments * 3 - 3] = 0;
            triangles[segments * 3 - 2] = segments;
            triangles[segments * 3 - 1] = 1;
        }

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.sharedMesh = mesh;
    }



    private float countAngle(Vector3 a, Vector3 b)
    {
        Vector3 c = Vector3.Cross(a, b);
        float angle = Vector3.Angle(a, b);
        float sign = (c.z < 0) ? 1 : -1;
        return angle * sign * Mathf.Deg2Rad;
    }

    public void SendToFear(GameObject target)
    {
        target.GetComponent<SpeciesFearLight>().ReceiveMessage();
    }

    public void clearFear()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Anim"))
        {
            item.GetComponent<SpeciesFearLight>().StopMoving();
        }
    }

    #region
    /*
    public float Radius = 1f;
    public LayerMask masks;
    private LineRenderer line;
    private MeshFilter _meshFilter;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }

    void Update()
    {
        Raycast360(transform.position, Radius, masks);
    }

    void Raycast360(Vector2 origin,float distance,int mask)
    {
        List<Vector3> vertexList = new List<Vector3>();
        for (int i = 0; i < 360; i++)
        {
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * i), Mathf.Sin(Mathf.Deg2Rad * i));
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, mask);
            float realDistance = hit.collider == null ? distance : hit.distance;
            Vector2 end = new Vector2(origin.x + realDistance * dir.x / dir.magnitude, origin.y + realDistance * dir.y / dir.magnitude);
            vertexList.Add(end);
            //Debug.DrawLine(origin, end);
        }
        fillCircle(vertexList.ToArray());
        line.positionCount = vertexList.Count;
        vertexList[vertexList.Count - 1] = vertexList[0];
        line.SetPositions(vertexList.ToArray());
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
    }

    void fillCircle(Vector3[] vertexList)
    {
        Mesh mesh = new Mesh();
        int[] triangles = new int[355*3];
        for (int i = 0,vi=1; i < 355*3-3; i+=3,vi++)
        {
            triangles[i] = 0;
            triangles[i + 1] = vi;
            triangles[i + 2] = vi + 1;
        }
        mesh.vertices = vertexList;
        mesh.triangles = triangles;
        _meshFilter.mesh = mesh;
    }*/
    #endregion
}
