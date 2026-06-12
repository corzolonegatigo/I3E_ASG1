using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class pitFallBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 closedPos;
    private Vector3 openPos;
    private Renderer rend;
    private Bounds bounds;
    void Start()
    {
        // set closePos and openPos. determine using current position and edge of obj
        rend = gameObject.GetComponent<MeshRenderer>();
        bounds = rend.bounds;

        closedPos = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z); 

        openPos = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z  - bounds.extents.x * 2);


        
    }

    IEnumerator MoveInOut()
    {
        yield return new WaitForSeconds(4.0f);
        yield return StartCoroutine(meshMovement(openPos, 0.5f));
        yield return new WaitForSeconds(4.0f);
        yield return StartCoroutine(meshMovement(closedPos, 0.5f));


    }

    IEnumerator meshMovement(Vector3 endpoint, float duration)
    {
        Vector3 start = gameObject.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            gameObject.transform.position = Vector3.Lerp(start, endpoint, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;

        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveInOut());
    }
}
