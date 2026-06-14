using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// author: zac
/// date: 6/13
/// description: handles player side interaction with objects that can be picked up
/// following https://www.youtube.com/watch?v=2IhzPTS4av4
/// BUT IF YOU ASK ME TO EXPLAIN THE CODE I CAN DO IT
/// </summary>
public class PickUpItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 positionSetWhenGrabbed;

    [SerializeField]
    private InputActionReference pickup;

    [SerializeField]
    private Transform objectGrabPointTransform;


    public ObjectGrabbable objectGrabbable;
    

    
    Ray ray;

    void Start()
    {
        positionSetWhenGrabbed = gameObject.transform.position + new Vector3(0.0f, 0.0f, 1.0f);


    }

    private void OnEnable()
    {
        pickup.action.started += pickUpDown;
    }

    private void pickUpDown(InputAction.CallbackContext obj)
    {

        
        if (objectGrabbable == null)
        {
            ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, GameManager.Instance.DISTANCE_THRESHOLD, 1<<6, QueryTriggerInteraction.Ignore))
            {
                // check if it actually has the script to allow it to be picked up
                if (hit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    this.objectGrabbable = objectGrabbable;
                }
            }
        } else
        {
            objectGrabbable.Drop();
            objectGrabbable = null;
        }


        
        
    }

    
    // Update is called once per frame
    void Update()
    {
         
    }
}
