using UnityEngine;



/// <summary>
/// 
/// 
/// 
/// TODO: MAKE ROPE LENGTH DYNAMIC  
/// </summary>
public class connectRope : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject rope;

    public bool ropeAttached;
    
    void Start()
    {
        rope.SetActive(false);
        ropeAttached = false;
    }

    public void toggleRopePresence()
    {

        if (!ropeAttached)
        {
            rope.SetActive(true);
            ropeAttached = !ropeAttached;
            GameManager.Instance.hasRope = false;
            GameManager.Instance.inventoryList.Remove("Rope");
        } else
        {
            rope.SetActive(false);
            ropeAttached = !ropeAttached;
            GameManager.Instance.hasRope = true;
            GameManager.Instance.inventoryList.Add("Rope");
            
        }

        
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
