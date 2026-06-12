using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    void OnCollisionEnter(Collision collision)
    {
        /// check if colliding w/ player character
        print(collision.gameObject.name);

        if (collision.gameObject.name == "PlayerCapsule")
        {

            if (!GameManager.Instance.hasHammer) // prevent it adding the same string multiple times
            {
                GameManager.Instance.inventoryList.Add("Hammer");
            }
            
            GameManager.Instance.hasHammer = true;
            
            
            playOnCollect();
            updateUI.showCollectItem("You found a Hammer!", "'100 kilos of pure metal on a stick. '");

            gameObject.SetActive(false);

            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// animating obj
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
