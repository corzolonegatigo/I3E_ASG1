using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// author: zac
/// date: 6/12
/// description: handles majority of raycasting between user camera and interactive objects (collectibles, winzone, doors, glass) using RAYCASTING
/// basic raycasting fundamentals + the bitwise layer mask from https://www.youtube.com/watch?v=B34iq4O5ZYI
/// ^^ this is like looking at documentation right
/// </summary>
public class interactableInRange : MonoBehaviour
{
   
    Ray ray;

    private string itemInView;

    public string objName;
    private Component interactFunction;
    public ManageUI updateUI;

    private PickUpItem item;

    void Start()
    {
        updateUI = FindFirstObjectByType<ManageUI>();
    }

    public GameObject checkProximity()
    {
        
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
         // checks if hit by raycast ray
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        if (Physics.Raycast(ray, out hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD, 2), 1<<10 | 1<<11 | 1<<6 | 1<<12, QueryTriggerInteraction.Ignore))
        {

            // get layer value. reads ray detecting both tgt and run layer specific code so that player cannot collect item and interact with a gameobject at the same time
            int layer = hit.collider.gameObject.layer;

            objName = hit.collider.name;
            

            if (layer == 10)
            {
                if (objName.Contains("glass"))
                {
                    if (GameManager.Instance.hasHammer)
                    {
                        GlassBehaviour breakGlass = hit.collider.GetComponent<GlassBehaviour>(); 
                        interactFunction = breakGlass;
                        updateUI.showInteractiveOption("Press (E) to break glass");
                        itemInView = "glass";  
                    
                    } 
                    else
                    {
                        updateUI.showInteractiveOption("You need something strong to break this!");
                        return null;
                    }
                }

                
            else if (objName.Contains("Door"))
            
            {

                string doorType = hit.collider.gameObject.tag;

                if (doorType != "OpenAccess")
                {
                    if (!GameManager.Instance.hasKeyCard)
                    {
                        updateUI.showInteractiveOption("You don't have permission to open this door");
                        return null;
                    }
                }
                DoorBehaviour door = hit.collider.GetComponent<DoorBehaviour>();
                interactFunction = door;

                if (door.doorOpen)
                {
                    updateUI.showInteractiveOption("Press (E) to close door");
                } else
                {
                    updateUI.showInteractiveOption("Press (E) to open door");
                }
                

                itemInView = "door";
            }
            }
            
            
            else if (layer == 11)
            {
                // with this amount of collectibles, nbd to write a condition for each. though, possible to combine card, rope, hammer scripts tgt.
                print(objName);
                if (objName.Contains("goldbar"))
                {
                    CollectScore bar = hit.collider.GetComponent<CollectScore>();
                    interactFunction = bar;
                    updateUI.showInteractiveOption("Click (LMB) to collect Gold Bar");

                    itemInView = "goldbar";
                }
                else if (objName.Contains("Safe"))
                {
                    CollectScore safe = hit.collider.GetComponent<CollectScore>();
                    interactFunction = safe;
                    updateUI.showInteractiveOption("Click (LMB) to open this Safe");

                    itemInView = "goldbar";
                }
                
                else if (objName.Contains("medkit"))
                {
                    CollectMedkit medkit = hit.collider.GetComponent<CollectMedkit>();
                    interactFunction = medkit;
                    updateUI.showInteractiveOption("Click (LMB) to collect Medkit");

                    itemInView = "medkit";
                } else
                {
                    CollectUnique unique = hit.collider.GetComponent<CollectUnique>();
                    interactFunction = unique;
                    updateUI.showInteractiveOption("Click (LMB) to collect " + char.ToUpper(objName[0]) + objName.Substring(1));

                    itemInView = "unique";
                    
                }
            } else if (layer == 6) // could use a if...elif...else but this makes it more readable imo (slower though)
            {
                string tag = hit.collider.gameObject.tag;
                if (item == null)
                {
                    updateUI.showInteractiveOption("Click (F) to pick up this " + tag);
                } else
                {
                    updateUI.showInteractiveOption("Click (F) to drop");
                }
                

            } else if (layer == 12)
            {

                if (GameManager.Instance.scoreMax == GameManager.Instance.score)
                {
                    updateUI.showWinPrompt("Escape?", "You have stolen all the gold here. Click (E) to leave."); // not an actual collectible. its the box with a title and description, and theres no need to create a third box for this

                } else
                {
                    updateUI.showWinPrompt("Escape?", "Theres still $" + (GameManager.Instance.scoreMax - GameManager.Instance.score).ToString() + "K worth of items to find! Click (E) to leave.");
                }
                
                itemInView = "winzone";

            }

            return hit.collider.gameObject;

        }

        if (Physics.Raycast(ray, out hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD_ROPE, 2), 1<<9, QueryTriggerInteraction.Ignore))
        {
            
            
            itemInView = "ropeConnection";
            print(hit.collider.gameObject);

            ConnectRope ropeConnectionScript = hit.collider.gameObject.GetComponent<ConnectRope>();
            print(ropeConnectionScript);

            if (GameManager.Instance.hasRope == true)
            {
                updateUI.showInteractiveOption("Press (E) to attach the rope.");
            } else if (ropeConnectionScript.ropeAttached == true)
            {
                updateUI.showInteractiveOption("Press (E) to detach the rope.");
            } else
            {
                updateUI.showInteractiveOption("You need a rope to interact with this.");
                return null;
            }
            

            return hit.collider.gameObject;
        }

        updateUI.hideInteractiveOption();
        updateUI.hideWinPrompt();
        return null;
    }

        // Update is called once per frame
    void Update()
    {

    }
}
