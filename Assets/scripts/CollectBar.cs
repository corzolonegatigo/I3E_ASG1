using UnityEngine;

public class CollectBar : MonoBehaviour
{


    public AudioClip collectSound;
    public Vector3 rotationSpeed = new Vector3(0f, 60f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        
        GameManager.Instance.score += 1;
        playOnCollect();
        

        gameObject.SetActive(false);
        
    
    }

    // Update is called once per frame
    void Update()
    {
        /// animating obj
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
