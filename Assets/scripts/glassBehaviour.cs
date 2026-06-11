using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// taken from https://www.youtube.com/watch?v=91JruzwZTHQ
/// 
/// </summary>
public class glassBehaviour : MonoBehaviour
{
    public GameObject brokenGlass;
    public manageUI UpdateUI;

    private Material material1;
    [SerializeField] private Material material2;
    private MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        
    }

    void removeGlassObj()
    {
        Destroy(brokenGlass);
    }
    public void breakGlass()
    {
        brokenGlass.SetActive(true);
        gameObject.SetActive(false);

        Invoke(nameof(removeGlassObj), 5.0f);
    }

    public void breakGlassPrompt()
    {
        UpdateUI.showInteractiveOption("Press (E) to break glass");
        
    }



    // checks if a) player in range, b) glass in direct player view

    // Update is called once per frame
    void Update()
    {
        
        

    }
}
