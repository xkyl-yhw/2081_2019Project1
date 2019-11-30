using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class TurnoverReact : MonoBehaviour
{
    public Vector3 center;
    public bool turnovered = false;
    public float border = 1;
    public Color color = Color.white;
    public Vector3 dir;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Material material;

    private Vector3[] vertices;
    private int[] triangles;
    private PolygonCollider2D collider;

    private void Start()
    {
        center = transform.position;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(Shader.Find("Sprites/Default"));
        material.SetColor("_Color", color);
        meshRenderer.sharedMaterial = material;
        collider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (!turnovered)
        {
            CreateinitMesh();
        }
        else
        {
            ChangeTurnOveredMesh();
        }
        collider.points = To2(vertices);
    }

    private void ChangeTurnOveredMesh()
    {
        if (dir.x != 0)
        {
            if (dir.x < 0)
            {
                vertices[3] = dir + new Vector3(-border / 2, border / 2);
                vertices[2] = dir + new Vector3(-border / 2, -border / 2);
            }
            else
            {
                vertices[0] = dir + new Vector3(border / 2, border / 2);
                vertices[1] = dir + new Vector3(border / 2, -border / 2);
            }
        }
        if (dir.y != 0)
        {
            if (dir.y < 0)
            {
                vertices[1] = dir + new Vector3(border / 2, -border / 2);
                vertices[2] = dir + new Vector3(-border / 2, -border / 2);
            }
            else
            {
                vertices[0] = dir + new Vector3(border / 2, border / 2);
                vertices[3] = dir + new Vector3(-border / 2, border / 2);
            }
        }
        setMesh();
    }
    private void CreateinitMesh()
    {
        vertices = new Vector3[4];
        vertices[0] = new Vector3(border / 2, border / 2);
        vertices[1] = new Vector3(border / 2, -border / 2);
        vertices[2] = new Vector3(-border / 2, -border / 2);
        vertices[3] = new Vector3(-border / 2, border / 2);
        triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[3] = 3;
        setMesh();
    }

    private void setMesh()
    {
        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.sharedMesh = mesh;
    }

    private void OnDisable()
    {
        meshFilter.sharedMesh = null;
    }

    public void setDir(Vector3 newDir )
    {
        dir = newDir;
        turnovered = true;
    }

    private Vector2[] To2(Vector3[] temp)
    {
        Vector2[] tempV2 = new Vector2[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            tempV2[i] = new Vector2(temp[i].x, temp[i].y);
        }
        return tempV2;
    }
}
