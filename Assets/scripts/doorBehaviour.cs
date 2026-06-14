using UnityEngine;
using UnityEngine.ProBuilder.Shapes;


/// <summary>
/// author: zac
/// date: 6/13
/// description: handles door functionality (open, close) and closes automatically pass a certain distance using RAYCASTING
/// overlap box documentation: https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Physics.OverlapBox.html
/// </summary>
public class DoorBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator animator;
    public bool doorOpen = false;
    private bool playerClose = false;

    private Renderer rend;
    private Bounds bounds;

    private Vector3 origin;
    private Vector3 extents;

    [SerializeField]
    private AudioClip openCloseSound;


    void Start()
    {
        GameManager.Instance.doors.Add(gameObject);
        rend = gameObject.transform.Find("door").GetComponent<Renderer>();
        bounds = rend.bounds;

        InvokeRepeating(nameof(checkIfPlayerInRange), 0f, 1f);
    }

    private void playOnUse()
    {
        if(openCloseSound != null)
            {
                AudioSource.PlayClipAtPoint(openCloseSound, transform.position);
            }
            else
            {
                print("no valid sound");
            }
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

        playOnUse();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
