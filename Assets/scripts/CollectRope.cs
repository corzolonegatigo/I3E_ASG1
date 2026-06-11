using UnityEngine;

public class CollectRope : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip collectSound;
    public Vector3 rotationSpeed = new Vector3(0f, 60f, 0f);
    private manageUI updateUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUI = FindFirstObjectByType<manageUI>();
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
    void OnCollisionEnter(Collision collision)
    {
        /// check if colliding w/ player character
        print(collision.gameObject.name);

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found in scene!");
            return;
        }

        if (collision.gameObject.name == "PlayerCapsule")
        {
            if (!GameManager.Instance.hasRope)
            {
                GameManager.Instance.inventoryList.Add("Rope");
            }
            GameManager.Instance.hasRope = true;
            playOnCollect();
            
            updateUI.showCollectItem("You found a Rope!", "'It goes round and round, round and round.'");

            Destroy(gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// animating obj
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
