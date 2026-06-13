using UnityEngine;


/// <summary>
/// 
/// 
/// overlap box documentation: https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Physics.OverlapBox.html
/// </summary>
public class doorBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator animator;
    private bool doorOpen = false;
    private bool playerClose = false;

    private Renderer rend;
    private Bounds bounds;

    private Vector3 origin;
    private Vector3 extents;


    void Start()
    {
        rend = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        bounds = rend.bounds;

        InvokeRepeating(nameof(checkIfPlayerInRange), 0f, 1f);
    }

    void checkIfPlayerInRange()
    {

        bool inRange = false;
        bounds = rend.bounds;
        origin = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z);
        extents = new Vector3(bounds.extents.x * 4, bounds.extents.y, bounds.extents.z * 4);

        Collider[] itemsTouching = Physics.OverlapBox(origin, extents, Quaternion.identity, 1<<3, QueryTriggerInteraction.Collide);
        foreach (Collider item in itemsTouching)
        {
            // just verify is it is the player in range
            if (item.name.Contains("Player"))
            {
                inRange = true;
            }
        }

        if (!inRange)
        {
            closeDoor();
        }
        
    }

    public void openDoor()
    {
        animator.SetTrigger("openDoor");
        doorOpen = true;
        print(gameObject.transform.rotation.y);
    }

    public void closeDoor()
    {
        animator.SetTrigger("closeDoor");
        doorOpen = false;
        print(gameObject.transform.rotation.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
