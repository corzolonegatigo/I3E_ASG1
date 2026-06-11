using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// taken from https://www.youtube.com/watch?v=91JruzwZTHQ
/// 
/// </summary>
public class glassBehaviour : MonoBehaviour
{
    public GameObject brokenGlass;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform parentTransform;




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
        print(playerTransform.position);

        /// get distance between parent obj and player
        float d = (playerTransform.position - parentTransform.position).sqrMagnitude;
        print(d);

        /// check if distance below a certain threshold. run func if so
        while (d < Mathf.Pow(GameManager.Instance.DISTANCE_THRESHOLD, 2) )
        {
            print("in range");
        }

    }
}
