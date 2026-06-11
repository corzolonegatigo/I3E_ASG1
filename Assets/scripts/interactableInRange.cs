using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;


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
         // checks if hit by raycast ray  ,
        if (Physics.Raycast(ray, out hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD, 2), 1<<10, QueryTriggerInteraction.Ignore))
        {

            if (GameManager.Instance.hasHammer == GameManager.Instance.hasHammer)
            {

                print("hit");
                print("in range");

                print(hit.collider.gameObject.name);


                objName = hit.collider.name;
                
                if (objName.Contains("glass"))
                {

                    print("glass in range");
                    glassBehaviour breakGlass = hit.collider.GetComponent<glassBehaviour>(); 
                    interactFunction = breakGlass;
                    updateUI.showInteractiveOption("Press (E) to break glass");
                    itemInView = "glass";  
                    
                } 

                return hit.collider.gameObject;
            }

        }

        if (Physics.Raycast(ray, out hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD_ROPE, 2), 1<<9, QueryTriggerInteraction.Ignore))
        {
            
            print("touching hooks");
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
