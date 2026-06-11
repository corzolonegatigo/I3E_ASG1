using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// swap to broken glass mesh taken from https://www.youtube.com/watch?v=91JruzwZTHQ
/// 
/// </summary>
public class glassBehaviour : MonoBehaviour
{
    public GameObject brokenGlass;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    void removeGlassObj()
    {
        Destroy(brokenGlass);
    }
    public void breakGlass()
    {
        brokenGlass.SetActive(true);
        gameObject.SetActive(false);

        Invoke(nameof(removeGlassObj), 3.0f); // delay not in ref
    }


    // Update is called once per frame
    void Update()
    {
        
        

    }
}
