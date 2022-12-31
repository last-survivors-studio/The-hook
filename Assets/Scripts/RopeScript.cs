using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x, Vector3.Distance(startPoint.transform.position, endPoint.transform.position) * 0.5f, transform.localScale.z);
        Vector3 relativePos = endPoint.transform.position - startPoint.transform.position;
        relativePos *= 0.5f;
        transform.position = startPoint.transform.position + relativePos;
        transform.LookAt(endPoint.transform.position);
        transform.Rotate( 90, 0 ,0);
    }
}
