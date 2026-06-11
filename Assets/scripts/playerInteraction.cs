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
/// </summary>
public class playerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Ray ray;

    private string itemInView;

    // reading inputs (outside of character controller)
    public InputActionReference interact;
    public interactableInRange itemInRange;



    private string item;
    private GameObject itemObj;
    void Start()
    {
        
       
    }

    public void OnEnable()
    {
        interact.action.started += Interact;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        print("interacted");

        item = itemInRange.objName;
        itemObj = itemInRange.checkProximity();

        if (item == "glass")
        {
            glassBehaviour glassScript = itemObj.GetComponent<glassBehaviour>();
            glassScript.breakGlass();


        }

    }



        // Update is called once per frame
    void Update()
    {

    }
}
