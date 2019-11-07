using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private float rotationSpeed = 100.0f;

    private float thrustForce = 3f;

    public AudioClip crash;

    public AudioClip shoot;

    public GameObject bullet;

    private GameController _gameController;

    private int _fireRatePlayerOne;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        _gameController = gameControllerObject.GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (name.Equals("PlayerOne"))
        {
            transform.Rotate(0, 0, -Input.GetAxis("HorizontalWASD") * rotationSpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("VerticalWASD") * thrustForce * transform.up);
            if (Input.GetKey(KeyCode.Space) && _fireRatePlayerOne > 10)
            {
                ShootBullet();
                _fireRatePlayerOne = 0;
            }

            _fireRatePlayerOne = _fireRatePlayerOne + 1;
        }

        if (name.Equals("PlayerTwo"))
        {
            transform.Rotate(0, 0, -Input.GetAxis("HorizontalArrows") * rotationSpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("VerticalArrows") * thrustForce * transform.up);
            if (Input.GetKey(KeyCode.Return) && _fireRatePlayerOne > 10)
            {
                ShootBullet();
                _fireRatePlayerOne = 0;
            }

            _fireRatePlayerOne = _fireRatePlayerOne + 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Ship"))
        {
            AudioSource.PlayClipAtPoint(
                crash, Camera.main.transform.position);
            transform.position = new Vector3(0,0,0);
            GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            _gameController.ReduceLives();
        }
    }

    void ShootBullet()
    {
        var transform1 = transform;
        var position = transform1.position;
        var rotation = transform1.rotation;
        Instantiate(bullet,
            new Vector3(position.x, position.y, 0),
            rotation);
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }
}
