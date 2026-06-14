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
public class playerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // reading inputs (outside of character controller)
    public InputActionReference interact;
    public InputActionReference restartGame;
    public InputActionReference collectItem;

    public interactableInRange itemInRange;

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
        print("enabled");
    }

    private void Collect(InputAction.CallbackContext obj)
    {
        print("collect");

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
                    print("kit");

                    updateUI.hideInteractiveOption();

                } else
                {
                    CollectUnique unique = itemObj.GetComponent<CollectUnique>();
                    unique.onCollect();
                    print("unique");

                    updateUI.hideInteractiveOption();
                }
                
            }

            
        }
        
        
    }
    private void Interact(InputAction.CallbackContext obj) // func itself from ref. code inside func is original
    {
        print("interacted");

        print(itemObj);

        if (itemObj != null)
        {
            if (itemObj.layer == 10)
            {
                item = itemObj.name;
        
                print(item + "here");
                if (item == "glass")
                {
                    if (GameManager.Instance.hasHammer) {
                        GlassBehaviour glassScript = itemObj.GetComponent<GlassBehaviour>();
                        glassScript.breakGlass();
                        print("break glass");

                        updateUI.hideInteractiveOption();
                    }
                    
                }  
                else if (item.Contains("Door"))
                {
                    print("interacting with door");
                    DoorBehaviour doorScript = itemObj.GetComponent<DoorBehaviour>();
                    doorScript.openDoor();

                    updateUI.hideInteractiveOption();
                }


                 print(itemInRange.objName);

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

                    print("hasrope");
                    ConnectRope ConnectRopeScript = itemObj.GetComponent<ConnectRope>();
                    ConnectRopeScript.toggleRopePresence();
                    
                }
            
            
        }

        

    }

    private void restart(InputAction.CallbackContext obj)
    {
        print("here"); 
        if (GameManager.Instance.gameOver)
        {
            print("game over");
            GameManager.Instance.Reset();
            updateUI.gameRestart();

            
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
