using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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
    public interactableInRange itemInRange;



    private string item;
    private GameObject itemObj;
    private manageUI updateUI;
    void Start()
    {
        updateUI = FindFirstObjectByType<manageUI>();
    }

    public void OnEnable() // from ref
    {
        interact.action.started += Interact;
    }

    private void Interact(InputAction.CallbackContext obj) // func itself from ref. code inside func is original
    {
        print("interacted");

        item = itemObj.name;
        

        if (item == "glass")
        {
            glassBehaviour glassScript = itemObj.GetComponent<glassBehaviour>();
            glassScript.breakGlass();

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
        // Update is called once per frame
    void Update()
    {
        itemObj = itemInRange.checkProximity();
    }
}
