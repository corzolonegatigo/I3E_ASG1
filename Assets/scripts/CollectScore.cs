using Unity.VisualScripting;
using UnityEngine;

public class CollectScore : MonoBehaviour
{


    public AudioClip collectSound;
    public Vector3 rotationSpeed = new Vector3(0f, 60f, 0f);
    private manageUI updateUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUI = FindFirstObjectByType<manageUI>();
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
        if (gameObject.name.Contains("goldbar"))
        {
            GameManager.Instance.score += 1;
            
            
            updateUI.showCollectItem("You found a gold bar!", " +1k in the bank!");
            
        } else if (gameObject.name.Contains("Safe"))
        {
            GameManager.Instance.score += 10;
            
            updateUI.showCollectItem("You found a metal safe!", " +10k in the bank!");
            
        }
        playOnCollect();
        gameObject.SetActive(false);
        
        
    
    }

    // Update is called once per frame
    void Update()
    {
        /// animating obj
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}
