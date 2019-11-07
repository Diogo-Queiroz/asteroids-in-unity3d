using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanTorusScript : MonoBehaviour
{
    Transform _transform1;
    void Start()
    {
        _transform1 = GetComponent<Transform>();
    }

    // Update method to see if we need to reposition our asteroids 
    void Update()
    {
        if (transform.position.x > 9)
        {
            _transform1.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            _transform1.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.y > 6)
        {
            _transform1.position = new Vector3(transform.position.x, -6, 0);
        }
        else if (transform.position.y < -6)
        {
            _transform1.position = new Vector3(transform.position.x, 6, 0);
        }
    }
}
