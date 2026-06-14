using UnityEngine;



/// <summary>
/// author: zac
/// date: 6/11
/// description: handles rope connection point functionality
/// </summary>
public class ConnectRope : MonoBehaviour
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
