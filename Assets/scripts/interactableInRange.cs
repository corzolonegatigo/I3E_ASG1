using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// 
/// basic raycasting fundamentals + the bitwise layer mask from https://www.youtube.com/watch?v=B34iq4O5ZYI
/// this is like looking at documentation right
/// </summary>
public class interactableInRange : MonoBehaviour
{
   
    Ray ray;

    private string itemInView;

    public string objName;
    private Component interactFunction;
    public manageUI updateUI;

    // reading inputs (outside of character controller)

    void Start()
    {
        updateUI = FindFirstObjectByType<manageUI>();
    }

    public GameObject checkProximity()
    {
            // + transform.forward * 0.5f + Vector3.up * 1.7f
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
         // checks if hit by raycast ray
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        if (Physics.Raycast(ray, out hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD, 2), 1<<10 + 1<<11, QueryTriggerInteraction.Ignore))
        {

            print(hit.collider.GetComponent<GameObject>().name);
            

            // get layer value. reads ray detecting both tgt and run layer specific code so that player cannot collect item and interact with a gameobject at the same time
            int layer = hit.collider.GetComponent<GameObject>().layer;

            objName = hit.collider.name;


            if (layer == 10)
            {
                if (objName.Contains("glass"))
                {
                    if (GameManager.Instance.hasHammer)
                    {
                        glassBehaviour breakGlass = hit.collider.GetComponent<glassBehaviour>(); 
                        interactFunction = breakGlass;
                        updateUI.showInteractiveOption("Press (E) to break glass");
                        itemInView = "glass";  
                    
                    } 
                }

                
            else if (objName.Contains("door"))
            
            {

                string doorType = hit.collider.GetComponent<GameObject>().tag;

                if (doorType != "OpenAccess")
                {
                    if (!GameManager.Instance.hasKeyCard)
                    {
                        updateUI.showInteractiveOption("You don't have permission to open this door");
                        return null;
                    }
                }
                doorBehaviour door = hit.collider.GetComponent<doorBehaviour>();
                interactFunction = door;
                updateUI.showInteractiveOption("Press (E) to open door");

                itemInView = "door";
            }
            }
            
            // only two layers, thus can just run a binary if...else loop
            else 
            {
                // with this amount of collectibles, nbd to write a condition for each. though, possible to combine card, rope, hammer scripts tgt.

                if (objName.Contains("goldbar"))
                {
                    CollectBar bar = hit.collider.GetComponent<CollectBar>();
                    interactFunction = bar;
                    updateUI.showInteractiveOption("Click (LMB) to collect Gold Bar");

                    itemInView = "goldbar";
                }
                else if (objName.Contains("medkit"))
                {
                    CollectMedkit medkit = hit.collider.GetComponent<CollectMedkit>();
                    interactFunction = medkit;
                    updateUI.showInteractiveOption("Click (LMB) to collect Medkit");

                    itemInView = "medkit";
                } 
            }

            return hit.collider.gameObject;

        }

        if (Physics.Raycast(ray, out hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD_ROPE, 2), 1<<9, QueryTriggerInteraction.Ignore))
        {
            
            
            itemInView = "ropeConnection";
            print(hit.collider.gameObject);

            connectRope ropeConnectionScript = hit.collider.gameObject.GetComponent<connectRope>();
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
            }
            

            return hit.collider.gameObject;
        }

        updateUI.hideInteractiveOption();
        return null;
    }

        // Update is called once per frame
    void Update()
    {

    }
}
