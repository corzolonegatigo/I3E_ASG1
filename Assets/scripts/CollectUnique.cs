using UnityEngine;


/// <summary>
/// 
/// author: zac
/// date: 6/13
/// description: manages behaviour of unique collectibles and how the player interacts with them
/// </summary>
public class CollectUnique : MonoBehaviour
{
    public AudioClip collectSound;
    public Vector3 rotationSpeed = new Vector3(0f, 60f, 0f);
    private ManageUI updateUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUI = FindFirstObjectByType<ManageUI>();
        GameManager.Instance.collectibles.Add(gameObject);
    }

    private void playOnCollect()
    {
        if(collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            else
            {
                print("no valid sound");
            }
    }
    public void onCollect()
    {

        // check what object this script is attached to. i think you can have it so that you initialise a variable which is a ref to the related bool, but i dont have internet rn
        // not an if...elif...else cause 
        if (gameObject.name == "admin Card")
        {
            if (!GameManager.Instance.hasKeyCard) // prevent it adding the same string multiple times
            {
                GameManager.Instance.inventoryList.Add("Admin Card");
                GameManager.Instance.hasKeyCard = true;
            }
            
            playOnCollect();
            updateUI.showCollectItem("You found an Admin Card!", "'Jane Doe, 27. Quant Engineer. \n I wonder what this card is for...'");
        } else if (gameObject.name == "hammer")
        {
                if (!GameManager.Instance.hasHammer) // prevent it adding the same string multiple times
            {
                GameManager.Instance.inventoryList.Add("Hammer");
                GameManager.Instance.hasHammer = true;
            }
            
            playOnCollect();
            updateUI.showCollectItem("You found a Hammer!", "'100 kilos of pure metal on a stick. '");

        } else if (gameObject.name == "rope")
        {
            if (!GameManager.Instance.hasRope) // prevent it adding the same string multiple times
            {
                GameManager.Instance.inventoryList.Add("Rope");
                GameManager.Instance.hasRope = true;
            }
            
            playOnCollect();
            
            updateUI.showCollectItem("You found a Rope!", "'It goes round and round, round and round.'");
        } else
        {
            print("attached to "+ gameObject.name + "which is not in the list of uniquye collectibles");
        }
        

        gameObject.SetActive(false);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        /// animating obj
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
