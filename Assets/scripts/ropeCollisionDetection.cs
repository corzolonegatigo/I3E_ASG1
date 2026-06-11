using UnityEngine;


/// <summary>
/// 
/// 
/// 
/// retired because it doesnt make sense to use raytracing for this cause you're tracking if character is touching mesh. ill be implementing it for raycasting sake if i did it
/// getting bounds of cylinder from https://claude.ai/share/435c7854-faf8-4b9c-b41a-6941fb54555f
/// capsulecast documentation https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Physics.CapsuleCast.html
/// </summary>
public class ropeCollisionDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Renderer rend;
    Bounds bounds;
    private Vector3 p1;
    private Vector3 p2;
    private float rad;

    public bool canClimb;

    
    void Start()
    {

        // get bounds
        rend = gameObject.GetComponent<Renderer>();
        bounds = rend.bounds;
        p1 = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
        p2 = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);

        rad = Mathf.Sqrt(Mathf.Pow(bounds.max.x - bounds.min.x, 2) + Mathf.Pow(bounds.max.z - bounds.min.z, 2));

        canClimb = false;

    }

    void checkIfTouchingPlayer()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(p1, p2, rad, transform.forward, out hit, 3.0f, 1<<3, QueryTriggerInteraction.Ignore))
        {
            print("inside of me!");
            canClimb = true;
        }

        


    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) // instead of using ropeAttached to reduce calling from other scripts (sounds like it should be more performant)
        {
            checkIfTouchingPlayer();
        }
    }
}
