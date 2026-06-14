using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class manageUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public Canvas UI;

    [SerializeField]
    private GameObject itemPrefab;
    
    [SerializeField]
    private Transform inventoryContainer;


    // for the hp components, easier to assign via inspector instead of code, due to the amount of nesting
    [SerializeField]
    private RectTransform healthBar;
    [SerializeField]
    private RectTransform healthBarContainer;
    [SerializeField]
    private TMP_Text healthText;


    private Transform itemPickedUpBox;
    



    // grab ui elements within ui
    private Transform collectdItemBox;
    private TMP_Text scoreText;
    private Transform interactiveBox;
    private Transform deathScreen;
    private Transform winContainer;
    

    void Start()
    {
        
        // disable vis of popups
        collectdItemBox = UI.transform.Find("CollectItemPromptBox");
        collectdItemBox.gameObject.SetActive(false);

        interactiveBox = UI.transform.Find("InteractivePromptBox");
        interactiveBox.gameObject.SetActive(false);

        deathScreen = UI.transform.Find("GameOver");

        itemPickedUpBox = UI.transform.Find("ItemPickedUpPrompt");
        itemPickedUpBox.gameObject.SetActive(false);

        winContainer = UI.transform.Find("WinScreen");
        winContainer.gameObject.SetActive(false);

        // maybe theres a better way to do this but it just to reduce amount of things you have to assign
        scoreText = (UI.transform.Find("ScoreBox")).transform.Find("score").GetComponent<TMP_Text>();

    }

    // dynamic inventory functions 
    public void updateInventory()
    {

        // reset inventory 
        foreach (Transform child in inventoryContainer.transform.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }


        List<string> inventory = GameManager.Instance.inventoryList; 
        int invLength = inventory.Count;
        print(invLength);

        // add inv header
        GameObject inventoryHeader = Instantiate(itemPrefab, inventoryContainer);
        inventoryHeader.GetComponent<TMP_Text>().text = "Inventory";
        
        for (int i = 0; i < invLength; i++)
        {
            print(i);
            
            GameObject slot = Instantiate(itemPrefab, inventoryContainer);
            slot.GetComponent<TMP_Text>().text = inventory[i];
        }
        
    }
    // this is so unnecessary
    IEnumerator hideElement(Transform element, float delay = 0.0f)
    {
        if (delay > 0.0f)
        {
            yield return new WaitForSeconds(delay);
        }

        element.gameObject.SetActive(false);
    }
    // prompts

    // for collecting items
    public void showCollectItem(string title, string description)
    {
        // get title and desc text boxes
        Transform titleTextTransform = collectdItemBox.transform.Find("Title");
        Transform descTextTransform = collectdItemBox.transform.Find("Description");

        TMP_Text titleText = titleTextTransform.GetComponent<TMP_Text>();
        TMP_Text descText = descTextTransform.GetComponent<TMP_Text>();

        // set the text
        titleText.text = title;
        descText.text = description;

        collectdItemBox.gameObject.SetActive(true);
        
        StartCoroutine(hideElement(collectdItemBox, 1.5f));
    }

    // to interact with something
    public void showInteractiveOption(string prompt)
    {
        Transform promptTransform = interactiveBox.transform.Find("Prompt");

        TMP_Text promptText = promptTransform.GetComponent<TMP_Text>();

        promptText.text = prompt;

        interactiveBox.gameObject.SetActive(true);

    }

    public void hideInteractiveOption()
    {
        interactiveBox.gameObject.SetActive(false);
    }

    public void updateScore()
    {
        int score = GameManager.Instance.score;
        scoreText.text = "Money Stolen: $" + score.ToString() + "K";
    }

    public void updateHealth()
    {
        float health = GameManager.Instance.playerHealth;
        float height = 80.0f; //its not changing anyways. setting a fixed value prevents any weirdness

        float maxWidth = healthBarContainer.rect.width;
        float width = MathF.Max(maxWidth * (health / 100.0f), 0f) ;

        

        print(width + "hp w");
        print(height);
        print(health/100.0f);

        healthBar.sizeDelta = new Vector2( width , height );

        // adjust health text

        string healthStr = health.ToString() + "/100";
        healthText.text = healthStr;

    }

    public void itemPickedUp(string itemname)
    {
        Transform itemPickedUp = itemPickedUpBox.transform.Find("ItemPickedUp");
        TMP_Text itemPickedUpText = itemPickedUp.GetComponent<TMP_Text>();

        itemPickedUpText.text = itemname;
        itemPickedUpBox.gameObject.SetActive(true);
    }

    public void hidePickItem()
    {
        itemPickedUpBox.gameObject.SetActive(false);
    }

    public void gameOver()
    {
        deathScreen.gameObject.SetActive(true);
    }

    public void gameRestart()
    {
        deathScreen.gameObject.SetActive(false);
    }
    
    public void winScreen()
    {
        Transform results = winContainer.transform.Find("MoneyStolen");
        TMP_Text resultText = results.GetComponent<TMP_Text>();

        resultText.text = "Money Stolen: $" + GameManager.Instance.score.ToString() + "/$" + GameManager.Instance.scoreMax.ToString();
        winContainer.gameObject.SetActive(true);
    }

    


    // Update is called once per frame
    void Update()
    {
       


    }
}
