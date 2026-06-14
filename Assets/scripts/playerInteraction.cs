using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.Shapes;


/// <summary>
///  
/// author: zac
/// date: 6/11
/// description: handles player input with regard to interactive objects within the scene
/// 
/// followed this (https://www.youtube.com/watch?v=ONlMEZs9Rgw) tutorial as a rough guide for detecting inputs
/// this would be so much easier if i just combined this and interactable in range together but that seems like bad practise or something
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // reading inputs (outside of character controller)
    public InputActionReference interact;
    public InputActionReference restartGame;
    public InputActionReference collectItem;

    public InteractableInRange itemInRange;

    [SerializeField]
    private AudioClip winSound;
   



    private string item;
    private GameObject itemObj;
    private ManageUI updateUI;
    void Start()
    {
        updateUI = FindFirstObjectByType<ManageUI>();
        InvokeRepeating(nameof(getInteractionItem), 0.0f, 0.25f);
    }

    void getInteractionItem()
    {
        itemObj = itemInRange.checkProximity();
    }

    private void OnEnable() // from ref
    {
        interact.action.started += Interact;
        restartGame.action.started += restart;
        collectItem.action.started += Collect;

    }

    private void Collect(InputAction.CallbackContext obj)
    {


        if (itemObj != null)
        {
            if (itemObj.layer == 11)
            {
                item = itemObj.name;

                if (item.Contains("goldbar") || item.Contains("Safe"))
                {
                    CollectScore score = itemObj.GetComponent<CollectScore>();
                    score.onCollect();

                    updateUI.hideInteractiveOption();

                    updateUI.hideInteractiveOption();
                } else if  (item.Contains("medkit"))
                {
                    CollectMedkit medkit = itemObj.GetComponent<CollectMedkit>();
                    medkit.onCollect();


                    updateUI.hideInteractiveOption();

                } else
                {
                    CollectUnique unique = itemObj.GetComponent<CollectUnique>();
                    unique.onCollect();


                    updateUI.hideInteractiveOption();
                }
                
            }

            
        }
        
        
    }
    private void Interact(InputAction.CallbackContext obj) // func itself from ref. code inside func is original
    {


        if (itemObj != null)
        {
            if (itemObj.layer == 10)
            {
                item = itemObj.name;
        

                if (item == "glass")
                {
                    if (GameManager.Instance.hasHammer) {
                        GlassBehaviour glassScript = itemObj.GetComponent<GlassBehaviour>();
                        glassScript.breakGlass();


                        updateUI.hideInteractiveOption();
                    }
                    
                }  
                else if (item.Contains("Door"))
                {
                    DoorBehaviour doorScript = itemObj.GetComponent<DoorBehaviour>();
                    doorScript.openDoor();

                    updateUI.hideInteractiveOption();
                }


            } else if (itemObj.layer == 12)
            {
                updateUI.winScreen();

                if(winSound != null)
                {
                    AudioSource.PlayClipAtPoint(winSound, transform.position);
                }
                else
                {
                    print("no valid sound");
                }


            } else if (itemObj.layer == 9)
                {

                    ConnectRope ConnectRopeScript = itemObj.GetComponent<ConnectRope>();
                    ConnectRopeScript.toggleRopePresence();
                    
                }
            
            
        }

        

    }

    private void restart(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.gameOver)
        {
            GameManager.Instance.Reset();
            updateUI.gameRestart();

            
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
