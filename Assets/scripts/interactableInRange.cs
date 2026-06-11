using UnityEngine;


/// <summary>
/// 
/// 
/// 
/// </summary>
public class interactableInRange : MonoBehaviour
{
   
    Ray ray;

    private string itemInView;

    public string objName;
    private Component interactFunction;

    // reading inputs (outside of character controller)

    void Start()
    {
    }

    public GameObject checkProximity()
    {
            // + transform.forward * 0.5f + Vector3.up * 1.7f
        ray = new Ray(transform.position, transform.forward);
        print("running check");
        /// only run checks if user has hammer item. if not this functionality does not matter 

        
        // checks if hit by raycast ray  ,
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD, 2), 1<<10, QueryTriggerInteraction.Ignore))
        {

            if (GameManager.Instance.hasHammer == GameManager.Instance.hasHammer)
            {

                print("hit");
                print("in range");

                print(hit.collider.gameObject.name);


                objName = hit.collider.name;
                
                if (objName.Contains("glass"))
                {
                    glassBehaviour breakGlass = hit.collider.GetComponent<glassBehaviour>(); 
                    interactFunction = breakGlass;
                    breakGlass.breakGlassPrompt();
                    itemInView = "glass";

                    
                    
                } 

                return hit.collider.gameObject;
            }

        }

        return null;
    }

        // Update is called once per frame
    void Update()
    {

    }
}
