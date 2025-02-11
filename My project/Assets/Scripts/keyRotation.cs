using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector3(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0f);
        var euler = transform.eulerAngles + direction * speed; 
        transform.eulerAngles = euler;
    }
}
