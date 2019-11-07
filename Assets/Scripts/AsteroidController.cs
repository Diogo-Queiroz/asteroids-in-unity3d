using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public AudioClip destroy;

    public GameObject smallAsteroid;

    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        _gameController = gameControllerObject.GetComponent<GameController>();
        
        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-50.0f, 150.0f));
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject); // Destroy the bullet

            if (tag.Equals("Large Asteroid")) // This will destroy the large and create three small
            {
                var position = transform.position;
                Instantiate(smallAsteroid,
                    new Vector3(position.x - .5f, position.y - .5f, 0),
                    Quaternion.Euler(0, 0, 90));
                Instantiate(smallAsteroid,
                    new Vector3(position.x + .5f, position.y + .5f, 0),
                    Quaternion.Euler(0, 0, 0));
                Instantiate(smallAsteroid,
                    new Vector3(position.x + .5f, position.y - .5f, 0),
                    Quaternion.Euler(0, 0, 270));
                _gameController.SplitAsteroids();
            }
            else
            {
                _gameController.DecrementAsteroids(); // This case only the small will be destroyed
            }
            
            AudioSource.PlayClipAtPoint(
                destroy, Camera.main.transform.position);
            _gameController.IncrementScore();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
