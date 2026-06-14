using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;


/// <summary>
/// 
/// please ignore this file its not in use but i dont want to delete it :(
/// 
/// not used because it doesnt work and there is an alternative. keeping cause i spent 3 hours on this
/// https://claude.ai/share/fc605568-7cb1-42a1-9b9d-6d1efd020099 
/// ^^  NaN validation
/// </summary>
public class SetLazerShape : MonoBehaviour
{


    
    [SerializeField] 
    private GameObject l_point;
    [SerializeField]
    private GameObject r_point;
    [SerializeField]
    private Transform lazer;

    // l_point and r_point mesh information
    private MeshRenderer l_rend;
    private MeshRenderer r_rend;

    private Bounds l_bounds;
    private Bounds r_bounds;

    private Vector3 l_center;
    private Vector3 r_center;

    private Vector3 lazer_pivot;

    private float x_dist;
    private float y_dist;
    private float z_dist;

    private float rotate_x;
    private float rotate_y;
    private float rotate_z;

    private Vector3 rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        l_rend = l_point.GetComponent<MeshRenderer>();
        r_rend = r_point.GetComponent<MeshRenderer>();

        l_bounds = l_rend.bounds;
        r_bounds = r_rend.bounds;

        l_center = new Vector3(l_bounds.center.x, l_bounds.center.y, l_bounds.center.z);
        r_center = new Vector3(r_bounds.center.x, r_bounds.center.y, r_bounds.center.z);

        // average coordinates
        lazer_pivot = new Vector3((l_bounds.center.x + r_bounds.center.x)/2, (l_bounds.center.y + r_bounds.center.y)/2, (l_bounds.center.z + r_bounds.center.z)/2 );

        x_dist = l_bounds.center.x - r_bounds.center.x;
        y_dist = l_bounds.center.y - r_bounds.center.y;
        z_dist = l_bounds.center.z - r_bounds.center.z;

        // angle of rotation on 1 axis = tan(opposite/adjacent)
        // rotation is split up so its easier to read
        print("xd"+ x_dist);
        print("yd"+ y_dist);
        print("zd"+ z_dist);

        rotate_x = MathF.Tan(x_dist/y_dist) * Mathf.Rad2Deg;
        rotate_y = MathF.Tan(y_dist/x_dist) * Mathf.Rad2Deg;
        rotate_z = MathF.Tan(x_dist/z_dist) * Mathf.Rad2Deg;

        // NaN validation
        rotate_x = float.IsNaN(rotate_x) ? 0f : rotate_x;
        rotate_y = float.IsNaN(rotate_y) ? 0f : rotate_y;
        rotate_z = float.IsNaN(rotate_z) ? 0f : rotate_z;

        print("rx" + rotate_x.ToString());
        print("ry" + rotate_y.ToString());
        print("rz" + rotate_z.ToString());



        // normals not aligned to world axises (for some reason). 
        // some reason rotate_z polarity is inverse
        rotation = new Vector3( rotate_x, -rotate_z, rotate_y);
        Quaternion rotationQuat = Quaternion.Euler(rotation);



        lazer.rotation = rotationQuat;

        lazer.position = lazer_pivot;

        
    }



    

    // Update is called once per frame
    void Update()
    {
        
    }
}
