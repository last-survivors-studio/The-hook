using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animation_Boot : MonoBehaviour
{
    public float maxSize = 10.0f;
    private float curr_x = 0.0f;
    public float rotation_speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(curr_x)*maxSize);
        curr_x += Time.deltaTime * rotation_speed;
    }
}
