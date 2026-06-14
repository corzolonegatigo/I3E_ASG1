using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// author: zac
/// date: 6/8
/// description: handles collection of collectibles which affect score
/// </summary>
public class CollectScore : MonoBehaviour
{


    public Vector3 rotationSpeed = new Vector3(0f, 60f, 0f);
    private ManageUI updateUI;

    [SerializeField]
    private AudioClip barSound;
    [SerializeField]
    private AudioClip safeSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUI = FindFirstObjectByType<ManageUI>();
        GameManager.Instance.collectibles.Add(gameObject);
    }

    private void playOnCollect(AudioClip sound)
    {
        if(sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, transform.position);
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
            playOnCollect(barSound);
            
        } else if (gameObject.name.Contains("Safe"))
        {
            GameManager.Instance.score += 10;
            
            updateUI.showCollectItem("You found a metal safe!", " +10k in the bank!");
            playOnCollect(safeSound);
            
        } else
        {

        }
        
        gameObject.SetActive(false);
        
        
    
    }

    // Update is called once per frame
    void Update()
    {
        /// animating obj
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}
