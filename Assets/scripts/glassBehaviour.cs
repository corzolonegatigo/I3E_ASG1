using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// author: zac
/// date: 6/8
/// description: manages glass behaviour (a la breaking)
/// 
/// swap to broken glass mesh taken from https://www.youtube.com/watch?v=91JruzwZTHQ
/// 
/// </summary>
public class GlassBehaviour : MonoBehaviour
{
    public GameObject brokenGlass;
    [SerializeField]
    private AudioClip glassBreak;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.glasses.Add(gameObject);
    }

    void removeGlassObj()
    {
        brokenGlass.SetActive(false);
    }
    public void breakGlass()
    {
        brokenGlass.SetActive(true);
        gameObject.SetActive(false);

        if(glassBreak != null)
        {
            AudioSource.PlayClipAtPoint(glassBreak, transform.position);
        }
        else
        {
            print("no valid sound");
        }

        Invoke(nameof(removeGlassObj), 3.0f); // delay not in ref

        
    }

    public void resetGlass()
    {
        gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
        

    }
}
