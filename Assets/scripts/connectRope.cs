using UnityEngine;



/// <summary>
/// 
/// 
/// 
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

        print("toggline rope" + ropeAttached);

        if (!ropeAttached)
        {
            rope.SetActive(true);

            GameManager.Instance.hasRope = false;
            GameManager.Instance.inventoryList.Remove("Rope");
        } else
        {
            rope.SetActive(false);
            
            GameManager.Instance.hasRope = true;
            GameManager.Instance.inventoryList.Add("Rope");
            
        }

        ropeAttached = !ropeAttached;

        
        
    }


}
