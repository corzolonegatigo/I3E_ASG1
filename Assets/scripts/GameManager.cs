using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;



/// <summary>
/// 
/// game manager concept from https://claude.ai/share/0439b4e5-ff7e-4510-b4d9-95653e49afc4
/// ^^ basically the awake stuff, public static instance, and i also asked it how to change the values but thats like looking for documentation basically i just forgot how to do it (just saying in case you check the logs)
/// 
/// lists from https://learn.unity.com/tutorial/lists-and-dictionaries
/// https://www.geeksforgeeks.org/c-sharp/list-class-in-c-sharp/ (g4g documuentation)
/// ^^ this is just documentation :)
/// </summary>
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public int score = 0;
    private int scorePriv = 0;

    public float playerHealth = 100f;
    private float playerHealthPriv = 100f;
    public bool hasHammer = false;
    public bool hasRope = false;
    public bool hasKeyCard = false;
    public bool gameOver = false;

    public List<string> inventoryList = new List<string>(); // no need to change capacity (i dont have that many items lmao)
    private List<string> inventoryListPriv = new List<string>();

    public manageUI updateUI;

    public List<GameObject> collectibles = new List<GameObject>();

    // static globals

    public float DISTANCE_THRESHOLD = 2.0f;
    public float DISTANCE_THRESHOLD_ROPE = 5.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        print("GameManager Awake called on: " + gameObject.name);

        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // persists across scenes 

        
    }

    void Start()
    {
        updateUI = FindFirstObjectByType<manageUI>(); // i would put this in awake but im scared it doesnt work
    }

    public void Reset()
    {
        gameOver = false;
        score = 0;
        playerHealth = 100f;
        hasHammer = false;
        hasKeyCard = false;
        hasRope = false;
        inventoryList = new List<string>();

        foreach (GameObject item in collectibles)
        {
            item.SetActive(true);
        }

    }


    // Update is called once per frame
    void Update()
    {

        // check for changes in variable. update ui if there is a change
        if (inventoryList.Count != inventoryListPriv.Count)
        {
            updateUI.updateInventory();

            // deep copy (my own magic). not the most effecient but works
            inventoryListPriv = new List<string>();
            foreach(string item in inventoryList)
            {
                inventoryListPriv.Add(item);
            }
        }

        if (score != scorePriv)
        {
            updateUI.updateScore();

            scorePriv = score; // shallow copies only apply for lists right
        }

        if (playerHealth != playerHealthPriv)
        {
            updateUI.updateHealth();

            playerHealthPriv = playerHealth;
        }

        if (playerHealth <= 0)
        {
            gameOver = true;
            updateUI.gameOver();

        }
    }
}
