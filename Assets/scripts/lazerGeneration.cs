using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// author: zac
/// date: 6/12
/// description: generates a lazer between two lazer point gameObjects
/// 
/// done with the help of https://claude.ai/share/95bcd527-2290-4297-9efa-158c750cffcb
/// mesh creation itself is ai genned, but everything around it is hand-typed
/// again i can explain it if needed
/// </summary>
/// 
/// 
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class LazerGeneration : MonoBehaviour
{


    
    [SerializeField] 
    private GameObject l_point;
    [SerializeField]
    private GameObject r_point;
    [SerializeField]
    private MeshFilter lazer;

    [SerializeField]
    private Material mat;

    [SerializeField]
    private Transform parentT;

    [SerializeField]
    private float width;
    // l_point and r_point mesh information
    private MeshRenderer l_rend;
    private MeshRenderer r_rend;

    private Bounds l_bounds;
    private Bounds r_bounds;

    private Vector3 l_center;
    private Vector3 r_center;

    private Vector3[] worldVerts;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        l_rend = l_point.GetComponent<MeshRenderer>();
        r_rend = r_point.GetComponent<MeshRenderer>();

        l_bounds = l_rend.bounds;
        r_bounds = r_rend.bounds;

        l_center = new Vector3(l_bounds.center.x, l_bounds.center.y, l_bounds.center.z);
        r_center = new Vector3(r_bounds.center.x, r_bounds.center.y, r_bounds.center.z);
    }

    void buildLazerMesh()
    {

        

        Vector3 direction = (l_center - r_center).normalized;
        Vector3 up = Mathf.Abs(Vector3.Dot(direction, Vector3.up)) < 0.99f
             ? Vector3.up
             : Vector3.forward;


        if (parentT.rotation.y == 0) // variants depending on rotation is by me
        {
            worldVerts = new Vector3[8]
            {
                new Vector3(l_bounds.center.x , l_bounds.center.y - width, l_bounds.center.z - width),
                new Vector3(l_bounds.center.x , l_bounds.center.y + width, l_bounds.center.z - width),
                new Vector3(l_bounds.center.x, l_bounds.center.y + width, l_bounds.center.z + width),
                new Vector3(l_bounds.center.x, l_bounds.center.y - width, l_bounds.center.z + width),

                new Vector3(r_bounds.center.x, r_bounds.center.y - width, r_bounds.center.z - width),
                new Vector3(r_bounds.center.x, r_bounds.center.y + width, r_bounds.center.z - width),
                new Vector3(r_bounds.center.x, r_bounds.center.y + width, r_bounds.center.z + width),
                new Vector3(r_bounds.center.x, r_bounds.center.y - width, r_bounds.center.z + width),

            };
        } else if (Math.Abs(parentT.rotation.y) / 90 == 0)
        {
            worldVerts = new Vector3[8]
            {
                new Vector3(l_bounds.center.x - width , l_bounds.center.y - width, l_bounds.center.z),
                new Vector3(l_bounds.center.x - width , l_bounds.center.y + width, l_bounds.center.z),
                new Vector3(l_bounds.center.x + width, l_bounds.center.y + width, l_bounds.center.z),
                new Vector3(l_bounds.center.x + width, l_bounds.center.y - width, l_bounds.center.z),

                new Vector3(r_bounds.center.x - width, r_bounds.center.y - width, r_bounds.center.z),
                new Vector3(r_bounds.center.x - width, r_bounds.center.y + width, r_bounds.center.z),
                new Vector3(r_bounds.center.x + width, r_bounds.center.y + width, r_bounds.center.z),
                new Vector3(r_bounds.center.x + width, r_bounds.center.y - width, r_bounds.center.z),

            };
        } else
        {
            worldVerts = new Vector3[8]
            {
                new Vector3(l_bounds.center.x - width , l_bounds.center.y - width, l_bounds.center.z - width),
                new Vector3(l_bounds.center.x - width , l_bounds.center.y + width, l_bounds.center.z - width),
                new Vector3(l_bounds.center.x + width, l_bounds.center.y + width, l_bounds.center.z + width),
                new Vector3(l_bounds.center.x + width, l_bounds.center.y - width, l_bounds.center.z + width),

                new Vector3(r_bounds.center.x - width, r_bounds.center.y - width, r_bounds.center.z - width),
                new Vector3(r_bounds.center.x - width, r_bounds.center.y + width, r_bounds.center.z - width),
                new Vector3(r_bounds.center.x + width, r_bounds.center.y + width, r_bounds.center.z + width),
                new Vector3(r_bounds.center.x + width, r_bounds.center.y - width, r_bounds.center.z + width),

            };
        }
        ;
        

        Vector3[] verts = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            verts[i] = transform.InverseTransformPoint(worldVerts[i]); // convert to local space
        }

        int[] tris = new int[]
        {

            // Face A
            0, 2, 1,   0, 3, 2,
            // Face B
            4, 5, 6,   4, 6, 7,
            // Bottom
            0, 5, 4,   0, 1, 5,
            // Top
            3, 6, 2,   3, 7, 6,
            // Left
            0, 7, 3,   0, 4, 7,
            // Right
            1, 2, 6,   1, 6, 5,
        };

        Vector2[] uvs = new Vector2[] //generate uvs
        {
            new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1),
            new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1),
        };


        // actually creating the mesh
        Mesh mesh = new Mesh();
        mesh.name      = "lazer";
        mesh.vertices  = verts;
        mesh.triangles = tris;
        mesh.uv        = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        

        GetComponent<MeshFilter>().mesh = mesh;
        

        MeshCollider col = GetComponent<MeshCollider>();
        col.sharedMesh = mesh;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        
        mr.sharedMaterial = mat;
        
    }

    

    // Update is called once per frame
    void Update()
    {
        buildLazerMesh();
    }
}
