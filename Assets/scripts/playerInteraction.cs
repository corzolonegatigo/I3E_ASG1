using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
///  
/// 
/// 
/// 
/// followed this (https://www.youtube.com/watch?v=ONlMEZs9Rgw) tutorial as a rough guide
/// this would be so much easier if i just combined this and interactable in range together but that seems like bad practise or something
/// </summary>
public class playerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // reading inputs (outside of character controller)
    public InputActionReference interact;
    public InputActionReference restartGame;
    public interactableInRange itemInRange;



    private string item;
    private GameObject itemObj;
    private manageUI updateUI;
    void Start()
    {
        updateUI = FindFirstObjectByType<manageUI>();
    }

    private void OnEnable() // from ref
    {
        interact.action.started += Interact;
        restartGame.action.started += restart;
        print("enabled");
    }

    private void Interact(InputAction.CallbackContext obj) // func itself from ref. code inside func is original
    {
        print("interacted");

        print(itemObj);

        if (itemObj != null)
        {
            item = itemObj.name;
        
            print(item + "here");
            if (item == "glass")
            {
                glassBehaviour glassScript = itemObj.GetComponent<glassBehaviour>();
                glassScript.breakGlass();
                print("break glass");

                updateUI.hideInteractiveOption();
                
            }  

            print(itemInRange.objName);
            if (item.Contains("rope"))
            {
                if (GameManager.Instance.hasRope)
                {
                    print("here");
                    connectRope connectRopeScript = itemObj.GetComponent<connectRope>();
                    connectRopeScript.toggleRopePresence();
                }
                
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

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
        // Update is called once per frame
    void Update()
    {
        itemObj = itemInRange.checkProximity();
    }
}
