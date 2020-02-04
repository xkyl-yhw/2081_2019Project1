using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TurnoverReact : MonoBehaviour
{
    public Sprite[] textures;
    public Texture2D atlas;
    private Material testMaterial;
    private SpriteRenderer testSpriteRenderer;
    private int targetWidth, targetHeight;
    public Vector2 dir;
    [Range(2, 100)]
    public int length;

    int width, height;

    public bool ison = false;
    private void Start()
    {
        targetHeight = 100 * length;
        targetWidth = 100;
        width = height = 0;
        testSpriteRenderer = GetComponent<SpriteRenderer>();
        foreach (Sprite item in textures)
        {
            if (width < item.texture.width) width = item.texture.width;
            if (height < item.texture.height) height = item.texture.height;
        }
        height *= length;
    }


    public void React()
    {
        if (!ison)
        {
            ison = true;
            createSprite();
            posAdjust();
        }

    }

    public void posAdjust()
    {
        this.transform.localPosition = new Vector2(0, (float)(length - 2) / 2 + 1.0f / 2);
        if (dir == Vector2.down)
        {
            transform.parent.transform.Rotate(Vector3.forward, 180);
        }
        if (dir == Vector2.left)
        {
            transform.parent.transform.Rotate(Vector3.forward, 90);
        }
        if (dir == Vector2.right)
        {
            transform.parent.transform.Rotate(Vector3.forward, -90);
        }
    }

    public Texture2D scaleSprite(Texture2D temp)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
        for (int i = 0; i < result.width; i++)
        {
            for (int j = 0; j < result.height; j++)
            {
                Color newColor = temp.GetPixelBilinear((float)i / (float)result.width, (float)j / (float)result.height);
                result.SetPixel(i, j, newColor);
            }
        }
        result.Apply();
        return result;
    }

    public void createSprite()
    {
        int heightCount = 0;
        int y, x;
        atlas = new Texture2D(width, height, TextureFormat.RGBA32, false);
        for (int i = 0; i < length; i++)
        {
            y = 0;
            while (y < textures[1].texture.height)
            {
                x = 0;
                while (x < textures[1].texture.width)
                {
                    if (y < textures[1].texture.height)
                    {
                        atlas.SetPixel(x, y + heightCount, textures[1].texture.GetPixel(x, y));
                    }
                    else
                    {
                        atlas.SetPixel(x, y + heightCount, new Color(0, 0, 0, 0));
                    }
                    ++x;
                }
                ++y;
            }
            atlas.Apply();
            heightCount += textures[1].texture.height;
        }
        y = 0;
        while (y < textures[0].texture.height)
        {
            x = 0;
            while (x < textures[0].texture.width)
            {
                if (y < textures[0].texture.height)
                {
                    atlas.SetPixel(x, y, textures[0].texture.GetPixel(x, y));
                }
                else
                {
                    atlas.SetPixel(x, y, new Color(0, 0, 0, 0));
                }
                ++x;
            }
            ++y;
        }
        atlas.Apply();
        y = 0;
        while (y < textures[2].texture.height)
        {
            x = 0;
            while (x < textures[2].texture.width)
            {
                if (y < textures[2].texture.height)
                {
                    atlas.SetPixel(x, y + atlas.height - textures[2].texture.height, textures[2].texture.GetPixel(x, y));
                }
                else
                {
                    atlas.SetPixel(x, y + atlas.height - textures[2].texture.height, new Color(0, 0, 0, 0));
                }
                ++x;
            }
            ++y;
        }
        atlas.Apply();
        atlas = scaleSprite(atlas);
        if (testMaterial != null)
        {
            testMaterial.mainTexture = atlas;
        }
        Sprite s = Sprite.Create(atlas, new Rect(0, 0, atlas.width, atlas.height), new Vector2(0.5f, 0.5f));
        testSpriteRenderer.sprite = s;
        this.gameObject.AddComponent<PolygonCollider2D>();
    }


    /*
    public Vector3 center;
    public bool turnovered = false;
    public float border = 1;
    public Color color = Color.white;
    public Vector3 dir;
    public float blockLength = 1f;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Material material;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private PolygonCollider2D collider;

    private void Start()
    {
        center = transform.position;
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

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = null;
    }

    public void setDir(Vector3 newDir)
    {
        dir = newDir * blockLength;
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
    }*/
}
