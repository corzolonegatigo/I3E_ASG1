using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manageUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public TMP_Text scoreText;

    [SerializeField]
    private GameObject itemPrefab;
    
    [SerializeField]
    private Transform inventoryContainer;
    void Start()
    {
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


    // Update is called once per frame
    void Update()
    {

        /// update score
        int score = GameManager.Instance.score;
        scoreText.text = "Bars Collected: " + score.ToString();
    }
}
