using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;


/// <summary>
/// 
/// 
/// https://www.youtube.com/watch?v=ONlMEZs9Rgw again for reading input and (the concept of) moving player with rigidbody
/// https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Rigidbody-linearVelocity.html linear velo
/// </summary>
public class climbRope : MonoBehaviour
{

    ropeCollisionDetection touchingRope;

    private float climbSpeed = 3.0f;
    private bool onRope;
    private bool isClimbing = false;
    private Rigidbody rb;
    private Vector2 climbingV2;
    private float climbDirection;

    public InputActionReference climbLadderAction;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        onRope = touchingRope.canClimb;
    }


    void stopClimbing()
    {
        
        // turn on gravity
        rb.useGravity = true;
        isClimbing = false;
        print("stop");
    }

    void startClimbing()
    {
        // turn off gravity
        rb.useGravity = false;
        isClimbing = true;
        print("start");
    }
    void climb()
    {
        // read value and then grab forward/backwards vector. we will only be changing vertical position
        climbingV2 = climbLadderAction.action.ReadValue<Vector2>();
        climbDirection = climbingV2.y;
        print(climbDirection);
        print(rb.useGravity);

        rb.linearVelocity = new Vector3(0.0f, climbDirection * climbSpeed, 0.0f);

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "rope" )
        {
            startClimbing();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "rope" )
        {
            print(climbLadderAction.action.ReadValue<Vector2>());
            climb();
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "rope" )
        {
            stopClimbing();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        
        
    }
}
