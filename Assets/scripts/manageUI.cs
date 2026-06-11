using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
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



    // grab ui elements within ui
    private Transform collectdItemBox;
    private TMP_Text scoreText;
    private Transform interactiveBox;

    void Start()
    {
        
        // disable vis of popups
        collectdItemBox = UI.transform.Find("CollectItemPromptBox");
        collectdItemBox.gameObject.SetActive(false);

        interactiveBox = UI.transform.Find("InteractivePromptBox");
        interactiveBox.gameObject.SetActive(false);

        // maybe theres a better way to do this but it just to reduce amount of things you have to assign
        scoreText = (UI.transform.Find("box")).transform.Find("score").GetComponent<TMP_Text>();

    }

    // dynamic inventory functions 
    public void AddItem(string itemName)
    {
        GameObject slot = Instantiate(itemPrefab, inventoryContainer);
        slot.GetComponent<TMP_Text>().text = itemName;
    }
    public void RemoveItem(int index)
    {
        Destroy(inventoryContainer.GetChild(index).gameObject);
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
        
        StartCoroutine(hideElement(collectdItemBox, 2.5f));
    }

    // to interact with something
    public void showInteractiveOption(string prompt)
    {
        Transform promptTransform = interactiveBox.transform.Find("Prompt");

        TMP_Text promptText = promptTransform.GetComponent<TMP_Text>();

        promptText.text = prompt;

        interactiveBox.gameObject.SetActive(true);

    }

    


    // Update is called once per frame
    void Update()
    {

        /// update score
        int score = GameManager.Instance.score;
        scoreText.text = "Bars Collected: " + score.ToString();
    }
}
