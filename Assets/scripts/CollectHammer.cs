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
            GameManager.Instance.hasHammer = true;
            playOnCollect();
            updateUI.AddItem("Hammer");
            updateUI.showCollectItem("You found a Hammer!", "'100 kilos of pure metal on a stick. '");

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
