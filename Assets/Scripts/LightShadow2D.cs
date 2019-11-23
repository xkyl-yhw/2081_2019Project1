using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
//[ExecuteInEditMode]
public class LightShadow2D : MonoBehaviour
{
    public float range = 4f;
    [Range(0,360)]
    public float angle=360;
    public int segments = 50;
    public LayerMask cullingMask = -1;
    public Color color=Color.white;


    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Material material;

    private Vector3[] vertices;
    private int[] triangles;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        material = new Material(Shader.Find("Sprites/Default"));
        material.SetColor("_color", color);
        meshRenderer.sharedMaterial = material;
    }

    private void Update()
    {
        range = Mathf.Clamp(range, 0, range);
        material.SetColor("_color", color);
        vertices = new Vector3[segments + 1];
        vertices[0] = transform.InverseTransformPoint(transform.localPosition);

        float tempAngle = angle * Mathf.Deg2Rad;
        float currentAngle = tempAngle / 2;
        float interAngle = tempAngle / segments;
        for (int i=0;i<segments;i++)
        {
            Vector2 dir = new Vector2(Mathf.Cos(currentAngle+countAngle(transform.right,Vector2.right)), Mathf.Sin(currentAngle + countAngle(transform.right, Vector2.right)));
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, dir, range, cullingMask);
            float realDis = hit.collider == null ? range : hit.distance;
            Vector2 endPoint = new Vector2(transform.localPosition.x + realDis * dir.x / dir.magnitude, transform.localPosition.y + realDis * dir.y / dir.magnitude);
            endPoint = transform.InverseTransformPoint(endPoint);
            vertices[i + 1] = endPoint;
            currentAngle -= interAngle;
        }

        triangles = new int[segments * 3];
        for (int i = 0,vi=1; i < segments*3-3; i+=3,vi++)
        {
            triangles[i] = 0;
            triangles[i + 1] = vi;
            triangles[i + 2] = vi + 1;
        }
        if (segments != 0&&angle==360)
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

    private void OnDisable()
    {
        meshFilter.sharedMesh = null;
    }

    private float countAngle(Vector3 a,Vector3 b)
    {
        Vector3 c = Vector3.Cross(a, b);
        float angle = Vector3.Angle(a, b);
        float sign = (c.z>=0)? 1:-1;
        return angle*sign*Mathf.Deg2Rad;
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
