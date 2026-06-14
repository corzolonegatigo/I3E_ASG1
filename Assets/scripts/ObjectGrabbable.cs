using UnityEngine;
using UnityEngine.AdaptivePerformance;


/// <summary>
/// 
/// 
/// following https://www.youtube.com/watch?v=2IhzPTS4av4
/// BUT IF YOU ASK ME TO EXPLAIN THE CODE I CAN DO IT
/// mesh collider stuff is by me because the item being held hits other items
/// 
/// ui code is completely seperate from tutorial
/// 
/// </summary>
public class ObjectGrabbable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private manageUI updateUI;
    private MeshCollider cldr;

    private BoxCollider childCldr;

    void Start()
    {
       rb = gameObject.GetComponent<Rigidbody>();
       cldr = gameObject.GetComponent<MeshCollider>();
       updateUI = FindFirstObjectByType<manageUI>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        rb.isKinematic = true;
        cldr.enabled = false;
        gameObject.transform.localScale = gameObject.transform.localScale * 0.5f;
        updateUI.itemPickedUp(gameObject.tag.ToUpper());

    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;

        gameObject.transform.localScale = gameObject.transform.localScale * 2.0f;
        rb.useGravity = true;
        rb.isKinematic = false;
        cldr.enabled = true;
        
        updateUI.hidePickItem();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * 10.0f);
            rb.MovePosition(objectGrabPointTransform.position);
        }
    }
}
