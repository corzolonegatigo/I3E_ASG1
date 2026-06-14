using UnityEngine;
using UnityEngine.AdaptivePerformance;


/// <summary>
/// author: zac
/// date: 6/12
/// description: code attached to block to allow it to be picked up
/// 
/// following https://www.youtube.com/watch?v=2IhzPTS4av4
/// BUT IF YOU ASK ME TO EXPLAIN THE CODE I CAN DO IT
/// mesh collider stuff is by me because the item being held hits other items
/// 
/// ui code is completely seperate from tutorial
/// sound code also
/// 
/// </summary>
public class ObjectGrabbable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private ManageUI updateUI;
    private MeshCollider cldr;

    private BoxCollider childCldr;

    [SerializeField]
    private AudioClip pickUpSound;
    [SerializeField]
    private AudioClip dropSound;

    public bool grabbed;

    void Start()
    {
       rb = gameObject.GetComponent<Rigidbody>();
       cldr = gameObject.GetComponent<MeshCollider>();
       updateUI = FindFirstObjectByType<ManageUI>();
       grabbed = false;
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        rb.isKinematic = true;
        cldr.enabled = false;
        gameObject.transform.localScale = gameObject.transform.localScale * 0.5f;
        updateUI.itemPickedUp(gameObject.tag.ToUpper());

        grabbed = true;


        if(pickUpSound != null)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
        }
        else
        {
            print("no valid sound");
        }

    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;

        gameObject.transform.localScale = gameObject.transform.localScale * 2.0f;
        rb.useGravity = true;
        rb.isKinematic = false;
        cldr.enabled = true;
        

        grabbed = false;
        updateUI.hidePickItem();

        if(dropSound != null)
        {
            AudioSource.PlayClipAtPoint(dropSound, transform.position);
        }
        else
        {
            print("no valid sound");
        }

        
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
