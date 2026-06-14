using UnityEngine;
using UnityEngine.ProBuilder.Shapes;


/// <summary>
/// 
/// 
/// overlap box documentation: https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Physics.OverlapBox.html
/// </summary>
public class doorBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator animator;
    public bool doorOpen = false;
    private bool playerClose = false;

    private Renderer rend;
    private Bounds bounds;

    private Vector3 origin;
    private Vector3 extents;


    void Start()
    {
        GameManager.Instance.doors.Add(gameObject);
        rend = gameObject.transform.Find("door").GetComponent<Renderer>();
        bounds = rend.bounds;

        InvokeRepeating(nameof(checkIfPlayerInRange), 0f, 1f);
    }

    void checkIfPlayerInRange()
    {

        bool inRange = false;
        bounds = rend.bounds;
        origin = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z);
        extents = new Vector3(bounds.extents.x * 8, bounds.extents.y, bounds.extents.z * 8);

        Collider[] itemsTouching = Physics.OverlapBox(origin, extents*5, Quaternion.identity, 1<<3, QueryTriggerInteraction.Collide);
        foreach (Collider item in itemsTouching)
        {
            // just verify is it is the player in range
            if (item.name.Contains("Player"))
            {
                inRange = true;
            }
        }

        if (doorOpen)
        {
            
            if (!inRange)
            {
                animator.SetTrigger("closeDoor");
            doorOpen = false;
            }
            
        }
            

            
        
        
    }

    public void openDoor()
    {

        doorOpen = !doorOpen;

        if (doorOpen)
        {
            animator.SetTrigger("openDoor");
            
        } else
        {
            animator.SetTrigger("closeDoor");
            
        }
        print("door state" + doorOpen + "running");
        
        
        print(gameObject.transform.rotation.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
