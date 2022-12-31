using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    float angle=0;
    public float speed=50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(angle<100)
        //{
            Debug.Log(angle);
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z );
            angle += Time.deltaTime* speed;
        //}
       
    }
}
