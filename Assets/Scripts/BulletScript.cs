using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Auto-destruction after 1 second
        Destroy(gameObject, 1.0f);
        
        // Movement of the bullet
        GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
    }
}
