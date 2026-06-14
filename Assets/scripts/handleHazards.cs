using System;
using UnityEngine;


/// <summary>
/// author: zac
/// date: 6/12
/// description: handles collision with hazard trigger zones, using RAYCASTING
/// </summary>
public class HandleHazards : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Renderer rend;
    private Bounds bounds;
    private Vector3 p1;
    private Vector3 p2;
    private float rad;
    private float height;

    private CapsuleCollider col;

    [SerializeField]
    private AudioClip lazerSound;
    
    [SerializeField]
    private AudioClip damageSound;
    


    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        bounds = rend.bounds;

        col = gameObject.GetComponent<CapsuleCollider>();
        height = col.height;
        rad = col.radius;

        InvokeRepeating(nameof(checkIfInHazardZone), 0f, 0.5f);
        
    }

    private void playOnDamage(AudioClip sound)
    {
        if(sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, transform.position);
            }
            else
            {
                print("no valid sound");
            }
    }

    void checkIfInHazardZone()
    {
        bounds = rend.bounds;
        p1 = new Vector3(bounds.center.x, bounds.center.y + height/2 , bounds.center.z);
        p2 = new Vector3(bounds.center.x, bounds.center.y - height/2, bounds.center.z);

        float damage = 0;

        Collider[] itemsTouching = Physics.OverlapCapsule(p1, p2, rad*3/4, 1<<7, QueryTriggerInteraction.Collide);
        foreach (Collider item in itemsTouching)
        {
            string hazardName = item.name;
            if (hazardName.Contains("Spikes"))
            {
                damage = 5.0f;
                playOnDamage(damageSound);
            }

            if (hazardName.Contains("lazer"))
            {
                damage = 10.0f;
                playOnDamage(lazerSound);
            }

            if (hazardName.Contains("Lava"))
            {
                damage = 2.0f;
                playOnDamage(damageSound);
            }

            GameManager.Instance.playerHealth = MathF.Max(GameManager.Instance.playerHealth - damage, 0.0f);
        }


        
       
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
