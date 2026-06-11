using System.Collections;
using UnityEngine;

/// <summary>
/// taken from https://www.youtube.com/watch?v=91JruzwZTHQ
/// 
/// </summary>
public class glassBehaviour : MonoBehaviour
{
    public GameObject brokenGlass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void removeGlass()
    {
        Destroy(brokenGlass);
    }
    void OnCollisionEnter(Collision collision)
    {
        brokenGlass.SetActive(true);
        gameObject.SetActive(false);

        Invoke(nameof(removeGlass), 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
