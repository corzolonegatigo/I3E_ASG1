using UnityEngine;

public class handleHazards : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Renderer rend;
    private Bounds bounds;
    private Vector3 p1;
    private Vector3 p2;
    private float rad;
    private float height;

    private CapsuleCollider col;
    


    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        bounds = rend.bounds;

        col = gameObject.GetComponent<CapsuleCollider>();
        height = col.height;
        rad = col.radius;

        InvokeRepeating(nameof(checkIfInHazardZone), 0f, 0.5f);
        
    }

    void checkIfInHazardZone()
    {
        bounds = rend.bounds;
        p1 = new Vector3(bounds.center.x, bounds.center.y + height/2 , bounds.center.z);
        p2 = new Vector3(bounds.center.x, bounds.center.y - height/2, bounds.center.z);

        Collider[] itemsTouching = Physics.OverlapCapsule(p1, p2, rad*3/4, 1<<7, QueryTriggerInteraction.Collide);
        foreach (Collider item in itemsTouching)
        {
            string hazardName = item.name;
            if (hazardName.Contains("Spikes"))
            {
                GameManager.Instance.playerHealth -= 5;
            }

            if (hazardName.Contains("lazer"))
            {
                GameManager.Instance.playerHealth -= 10;
            }
        }
        
       
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
