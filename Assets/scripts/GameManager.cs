using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public int score = 0;
    public float playerHealth = 100f;
    public bool hasHammer = false;
    public int ropeAmt = 0;
    public bool hasKeyCard = false;

    // static globals

    public float DISTANCE_THRESHOLD = 0.005f;

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
